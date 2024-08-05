using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CustomExchangeEndpointProxy.Model.Response
{
    public class BotExchangeResponse
    {
        public enum BotExchangeBranch
        {
            DoNotBegin,
            PromptAndCollectNextResponse,
            ReturnControlToScript,
            EndContact,
            AudioInputUntranscribeable,
            Error,
            DTMFBreakout,
            UserInputTimeout,
            UserInputNotUnderstood
        }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("branchName")]
        public BotExchangeBranch BranchName { get; set; }

        [JsonProperty("nextPromptSequence")]
        public PromptSequence? NextPromptSequence { get; set; }

        [JsonProperty("intentInfo")]
        public IntentInfo? IntentInfo { get; set; }

        [JsonProperty("nextPromptBehaviors")]
        public PromptBehaviors? NextPromptBehaviors { get; set; }

        [JsonProperty("customPayload")]
        public Dictionary<string, object>? CustomPayload { get; set; }

        [JsonProperty("errorDetails")]
        public BotErrorDetails? ErrorDetails { get; set; }

        [JsonProperty("botSessionState")]
        public object? BotSessionState { get; set; }

        [JsonProperty("nameValueParameters")]
        public Dictionary<string, object> NameValueParameters { get; set; }

        public BotExchangeResponse()
        {
            NameValueParameters = new Dictionary<string, object>();
        }
    }
}
