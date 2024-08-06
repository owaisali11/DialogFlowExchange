using Newtonsoft.Json;

namespace CustomExchangeEndpointProxy.Model.Response
{
    public class SilenceRules
    {
        [JsonProperty("engageComfortSequence")]
        public bool engageComfortSequence { get; set; }

        [JsonProperty("botResponseDelayTolerance")]
        public int BotResponseDelayTolerance { get; set; }

        [JsonProperty("comfortPromptSequence")]
        public PromptSequence? ComfortPromptSequence { get; set; }

        [JsonProperty("millisecondsToWaitForUserResponse")]
        public int MillisecondsToWaitForUserResponse { get; set; }
    }
}
