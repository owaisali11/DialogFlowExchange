using Newtonsoft.Json;

namespace CustomExchangeEndpointProxy.Model.Response
{
    public class PromptDefinition
    {
        [JsonProperty("transcript")]
        public string? Transcript { get; set; }

        [JsonProperty("base64EncodedG711ulawWithWavHeader")]
        public string Base64EncodedG711ulawWithWavHeader { get; set; }

        [JsonProperty("audioFilePath")]
        public string AudioFilePath { get; set; }

        [JsonProperty("textToSpeech")]
        public string TextToSpeech { get; set; }

        [JsonProperty("mediaSpecificObject")]
        public object MediaSpecificObject { get; set; }
    }
}
