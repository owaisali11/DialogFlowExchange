using Newtonsoft.Json;

namespace CustomExchangeEndpointProxy.Model.Response
{
    public class PromptBargeConfiguration
    {
        [JsonProperty("enableSpeakerBarge")]
        public bool EnableSpeakerBarge { get; set; }
    }
}
