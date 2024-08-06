using CustomExchangeEndpointProxy.Client;
using CustomExchangeEndpointProxy.Interface;
using CustomExchangeEndpointProxy.Model;
using CustomExchangeEndpointProxy.Model.Request;
using CustomExchangeEndpointProxy.Model.Response;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace CustomExchangeEndpointProxy.Manager
{
    public class CustomExchangeManager : ICustomExchangeManager
    {            
        private readonly ILogger<CustomExchangeManager> _logger;
        private readonly ICustomExchangeEndpointService _service;
        /// <summary>
        /// Constructorr
        /// </summary>
        public CustomExchangeManager(ILogger<CustomExchangeManager> logger, ICustomExchangeEndpointService service)
        {
            _logger = logger;
            _service = service;
        }
        /// <summary>
        /// This method handles json key and project id from endpoint parameters.
        /// </summary>
        /// <param name="botExchangeRequest"></param>
        /// <returns></returns>
        private (string jsonKeyPath, string projectid) InitializeParameters(ExternalIntegrationBotExchangeRequest botExchangeRequest)
        {            
            string jsonKeyPath = null;
            string projectid = null;
            var botConfig = JObject.Parse(botExchangeRequest.BotConfig);

            var endPointParameters = botConfig["endpointParameters"] as JArray;
            if (endPointParameters != null)
            {
                foreach (var param in endPointParameters)
                {
                    if (param["name"]?.ToString() == "jsonKey")
                    {
                        jsonKeyPath = param["value"]?.ToString();                       
                    }
                    if (param["name"]?.ToString() == "projectid")
                    {
                        projectid = param["value"]?.ToString();
                    }                  
                }
            }
            string wrappedJsonkey = "{" + jsonKeyPath + "}";
            _logger.LogInformation("Parameters initialized successfully.");
            return (wrappedJsonkey, projectid);
        }
        /// <summary>
        /// This asynchronous method interacts with a Dialogflow service to
        /// process a bot exchange request 
        /// and generates a response based on the conversation with the bot.
        /// </summary>
        /// <param name="botrequest"></param>
        /// <returns></returns>
        public async Task<CustomExchangeResponse_V1> InitiateBotExchangeAsync(ExternalIntegrationBotExchangeRequest botrequest)
        {
            CustomExchangeResponse_V1 actionResponse = new();
            PromptDefinition promptResponse = new();
            PromptSequence promptSequence = new();
            BotSessionState botSessionState = new();
            var (jsonKey, projectid) = InitializeParameters(botrequest);
            string sessionId;
                             
            if (botrequest.BotSessionState != null)
            {
                try
                {
                    botSessionState = JsonConvert.DeserializeObject<BotSessionState>(botrequest.BotSessionState.ToString());
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Deserialization error: {ex.Message}");
                }
            }
            else
            {
                sessionId = Guid.NewGuid().ToString();
                botSessionState.SessionID = sessionId;
                _logger.LogInformation("Generated new session ID: {SessionId}", sessionId);
            }

            try
            {             
                var customPayloadString = JsonConvert.SerializeObject(botrequest.CustomPayload);

                var detectIntentRequest = HandleRequest(projectid, botSessionState.SessionID, botrequest, customPayloadString);

                var response = await _service.DetectIntentAsync(detectIntentRequest, jsonKey);
                _logger.LogInformation("Received response from Dialogflow: {response}", response);

                var fulfillmentTexts = GetMessageAsync(response.QueryResult.FulfillmentMessages);
                _logger.LogInformation("FulfillmentText: {fulfillmentTexts}", fulfillmentTexts);
                var prompts = new List<PromptDefinition>();
                var customPayload = new Dictionary<string, object>();

                foreach (var message in fulfillmentTexts)
                {
                    actionResponse.BranchName = BotExchangeResponse.BotExchangeBranch.PromptAndCollectNextResponse;
                    actionResponse.IntentInfo = new IntentInfo
                    {
                        Intent = response.QueryResult.Intent.DisplayName,
                    };
                    actionResponse.BotSessionState = botSessionState;
                    actionResponse.ErrorDetails = new BotErrorDetails
                    {
                        ErrorBehavior = BotErrorDetails.BotLoopErrorBehavior.ReturnControlToScriptThroughErrorBranch,
                        ErrorPromptSequence = null,
                        SystemErrorMessage = null,      
                    };
                    if (message.StartsWith("{"))
                    {
                        ProcessMessageWithPayload(message, actionResponse, prompts, customPayload,response);
                        _logger.LogInformation("Custom Payload : {Custom Payload} " , customPayload);
                        break;
                    }
                    else
                    {
                        prompts.Add(new PromptDefinition
                        {
                            Transcript = message,
                        });
                    }
                }
                actionResponse.NextPromptSequence = new PromptSequence
                {
                    Prompts = prompts
                };

                if (response.QueryResult.Intent.DisplayName == "StandardBotEscalation" || response.QueryResult.Intent.DisplayName == "StandardBotEndConversation")
                {
                    actionResponse.BranchName = BotExchangeResponse.BotExchangeBranch.ReturnControlToScript;                                                                
                }
                else if (botrequest.UserInputType == UserInputType.NO_INPUT)
                {
                    actionResponse.BranchName = BotExchangeResponse.BotExchangeBranch.UserInputTimeout;                                 
                }
                _logger.LogInformation("Bot exchange initiated successfully.");
                return actionResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during bot exchange: {ex.Message}");
                return ErrorResponse(ex);
            }
        }
        /// <summary>
        /// This method process message which contains custom payload in response.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="actionResponse"></param>
        /// <param name="prompts"></param>
        /// <param name="customPayload"></param>
        /// <param name="intentResponse"></param>
        private void ProcessMessageWithPayload(string message, CustomExchangeResponse_V1 actionResponse, List<PromptDefinition> prompts, Dictionary<string, object> customPayload, DetectIntentResponse intentResponse)
        {
            var mediaSpecificObject = JObject.Parse(message);
            if (mediaSpecificObject.ToString().Contains("dfoMessage"))
            {
                prompts.Add(new PromptDefinition
                {
                    MediaSpecificObject = mediaSpecificObject,
                });
            }
            else
            {
                var customPayloadObject = JObject.Parse(message);
                prompts.Add(new PromptDefinition
                {
                    Transcript = intentResponse.QueryResult.FulfillmentText,
                });

                customPayload = JsonConvert.DeserializeObject<Dictionary<string, object>>(customPayloadObject.ToString());
                actionResponse.CustomPayload = customPayload;

                ProcessExchangeResultOverride(customPayload, actionResponse);
            }
        }
        /// <summary>
        /// Overrides the response branch or the intent in VAH with the branch specified or the
        ///intent specified (or both) in the custom payload returned from the bot
        /// </summary>
        /// <param name="customPayload"></param>
        /// <param name="actionResponse"></param>
        /// <returns></returns>
        private bool ProcessExchangeResultOverride(Dictionary<string, object> customPayload, CustomExchangeResponse_V1 actionResponse)
        {
            if (!customPayload.ContainsKey("contentType") || customPayload["contentType"].ToString() != "ExchangeResultOverride")
            {
                return false;
            }
            var content = (JObject)customPayload["content"];
            if (content == null)
            {
                return false;
            }
            var vahExchangeResultBranchString = content["vahExchangeResultBranch"]?.ToString();
            var intent = content["intent"]?.ToString();

            if (string.IsNullOrEmpty(vahExchangeResultBranchString) && string.IsNullOrEmpty(intent))
            {
                return false;
            }
            if (Enum.TryParse(vahExchangeResultBranchString, out BotExchangeResponse.BotExchangeBranch vahExchangeResultBranch))
            {
                actionResponse.BranchName = vahExchangeResultBranch;

                actionResponse.IntentInfo = new IntentInfo
                {
                    Intent = intent,
                };
                actionResponse.CustomPayload = customPayload;
                return true;
            }
            return false;
        }
        /// <summary>
        /// This method handle the request from user
        /// check if its event or text input.
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="sessionId"></param>
        /// <param name="botExchangeRequest"></param>
        /// <param name="customPayload"></param>
        /// <param name="languageCode"></param>
        /// <returns></returns>
        private DetectIntentRequest HandleRequest(string projectId, string sessionId, ExternalIntegrationBotExchangeRequest botExchangeRequest, string customPayload, string languageCode = "en-US")
        {
            var session = new SessionName(projectId, sessionId);
            DetectIntentRequest intentRequest = new();

            if (botExchangeRequest.UserInputType == UserInputType.AUTOMATED_TEXT)
            {
                intentRequest = new DetectIntentRequest
                {
                    SessionAsSessionName = session,

                    QueryInput = new QueryInput
                    {
                        Event = new EventInput
                        {
                            Name = botExchangeRequest.UserInput,
                            LanguageCode = languageCode,
                        }
                    }
                };
            }
            else
            {
                intentRequest = new DetectIntentRequest
                {
                    SessionAsSessionName = session,

                    QueryInput = new QueryInput
                    {
                        Text = new TextInput
                        {
                            Text = botExchangeRequest.UserInput,
                            LanguageCode = languageCode,
                        }
                    }
                };
            }
            if (!string.IsNullOrEmpty(customPayload))
            {
                try
                {
                    var payloadStruct = Google.Protobuf.WellKnownTypes.Struct.Parser.ParseJson(customPayload);
                    intentRequest.QueryParams = new QueryParameters
                    {
                        Payload = payloadStruct
                    };
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Error parsing custom payload: {ex.Message}");
                }
            }
            return intentRequest;   
        }
        /// <summary>
        /// This method handles the message from response 
        /// handle condition for both payload and text.
        /// </summary>
        /// <param name="fulfillmentMessages"></param>
        /// <returns></returns>
        private List<string> GetMessageAsync(RepeatedField<Intent.Types.Message> fulfillmentMessages)
        {
            var responses = new List<string>();
            foreach (var message in fulfillmentMessages)
            {
                if (message.Text != null && message.Text.Text_.Count > 0)
                {
                    responses.AddRange(message.Text.Text_);
                }
                else
                {
                    responses.Add(message.Payload.ToString());
                }
            }
            return responses;
        }

        /// <summary>
        /// This method handles the error  response.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private CustomExchangeResponse_V1 ErrorResponse(Exception ex)
        {
            BotSessionState botSessionState = new();
            var errorResponse = new CustomExchangeResponse_V1
            {
                BranchName = BotExchangeResponse.BotExchangeBranch.Error,
                NextPromptSequence = new PromptSequence
                {
                    Prompts = new List<PromptDefinition>
                    {
                        new PromptDefinition
                        {
                            Transcript = "The webhook response does not conform to spec.",
                            Base64EncodedG711ulawWithWavHeader = null,
                            AudioFilePath = null,
                            TextToSpeech = null,
                            MediaSpecificObject = null
                        }
                    }
                },
                ErrorDetails = new BotErrorDetails
                {
                    ErrorBehavior = BotErrorDetails.BotLoopErrorBehavior.ReturnControlToScriptThroughErrorBranch,
                    SystemErrorMessage = $"External webhook response does not conform to spec. Exception caught in REST Post: {ex.Message}"
                },
                BotSessionState = botSessionState
            };

            return errorResponse;
        }
    }
}

