using Newtonsoft.Json;

namespace CustomExchangeEndpointProxy.Model.Response
{
    public class AudioTranscriptionConfig
    {
        [JsonProperty("transcriptionProfileId")]
        public string TranscriptionProfileId { get; set; }

        [JsonProperty("hintPhrases")]
        public List<string> HintPhrases { get; set; }
    }
}
