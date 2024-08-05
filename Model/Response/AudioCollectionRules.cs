
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
        public UserInputCollectType collectionType { get; set; }
        public CollectDtmfRules dtmfRules { get; set; }
        public PromptBargeConfiguration bargeConfiguration { get; set; }
        public AudioTranscriptionConfig audioTranscriptionConfig { get; set; }
    }
}
