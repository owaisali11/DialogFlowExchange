using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CustomExchangeEndpointProxy.Model.Response
{
    public class BotErrorDetails
    {
        public enum BotLoopErrorBehavior
        {
            ReturnControlToScriptThroughErrorBranch,
            EndContact
        }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("errorBehavior")]
        public BotLoopErrorBehavior ErrorBehavior { get; set; }

        [JsonProperty("errorPromptSequence")]
        public PromptSequence? ErrorPromptSequence { get; set; }

        [JsonProperty("systemErrorMessage")]
        public string? SystemErrorMessage { get; set; }
    }
}
