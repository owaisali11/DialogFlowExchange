
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CustomExchangeEndpointProxy.Model.Response
{
    public class AudioCollectionRules
    {
        public enum UserInputCollectType
        {
            DO_NOT_COLLECT_USER_RESPONSE,
            SEND_UTTERANCE_AUDIO,
            SEND_DTMF_ONLY_AS_TEXT
        }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("collectionType")]
        public UserInputCollectType CollectionType { get; set; }
        [JsonProperty("dtmfRules")]
        public CollectDtmfRules DtmfRules { get; set; }
        [JsonProperty("bargeConfiguration")]
        public PromptBargeConfiguration BargeConfiguration { get; set; }
        [JsonProperty("audioTranscriptionConfig")]
        public AudioTranscriptionConfig AudioTranscriptionConfig { get; set; }
    }
}
