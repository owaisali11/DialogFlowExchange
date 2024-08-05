using Newtonsoft.Json;

namespace CustomExchangeEndpointProxy.Model.Response
{
    public class IntentInfo
    {
        [JsonProperty("intent")]
        public string Intent { get; set; }

        [JsonProperty("context")]
        public string Context { get; set; }

        [JsonProperty("intentConfidence")]
        public float IntentConfidence { get; set; }

        [JsonProperty("lastUserUtterance")]
        public string LastUserUtterance { get; set; }

        [JsonProperty("slots")]
        public Dictionary<string, object> Slots { get; set; }

        public IntentInfo()
        {
            Slots = new Dictionary<string, object>();
        }
    }
}
