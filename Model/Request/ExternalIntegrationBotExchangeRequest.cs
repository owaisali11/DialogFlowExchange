using Newtonsoft.Json;

namespace CustomExchangeEndpointProxy.Model.Request
{
    public class ExternalIntegrationBotExchangeRequest
    {
        [JsonProperty("virtualAgentId")]
        public string VirtualAgentId { get; set; }

        [JsonProperty("botConfig")]
        public dynamic BotConfig { get; set; }

        [JsonProperty("userInput")]
        public string? UserInput { get; set; }

        [JsonProperty("userInputType")]
        public UserInputType UserInputType { get; set; }

        [JsonProperty("executionInfo")]
        public ActionExecutionInfo? ExecutionInfo { get; set; }

        [JsonProperty("base64wavFile")]
        public string? Base64WavFile { get; set; }

        [JsonProperty("botSessionState")]
        public object? BotSessionState { get; set; }

        [JsonProperty("customPayload")]
        public object? CustomPayload { get; set; }

        [JsonProperty("mediaType")]
        public string MediaType { get; set; }
    }
}
