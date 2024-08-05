using Newtonsoft.Json;

namespace CustomExchangeEndpointProxy.Model.Response
{
    public class PromptBehaviors
    {
        [JsonProperty("silenceRules")]
        public SilenceRules SilenceRules { get; set; }

        [JsonProperty("audioCollectionRules")]
        public AudioCollectionRules AudioCollectionRules { get; set; }

        public PromptBehaviors()
        {
            SilenceRules = new SilenceRules();
        }
    }
}
