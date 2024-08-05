using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using static CustomExchangeEndpointProxy.Model.Response.BotExchangeResponse;

namespace CustomExchangeEndpointProxy.Model.Response
{
    public class CustomExchangeResponse_V1
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("branchName", Required = Required.Always)]
        public BotExchangeBranch BranchName { get; set; }

        [JsonProperty("nextPromptSequence")]
        public PromptSequence? NextPromptSequence { get; set; }

        [JsonProperty("intentInfo")]
        public IntentInfo IntentInfo { get; set; }

        [JsonProperty("nextPromptBehaviors")]
        public PromptBehaviors NextPromptBehaviors { get; set; }

        [JsonProperty("customPayload")]
        public Dictionary<string, object>? CustomPayload { get; set; }

        [JsonProperty("errorDetails")]
        public BotErrorDetails? ErrorDetails { get; set; }

        [JsonProperty("botSessionState")]
        public object? BotSessionState { get; set; }
    }
}
