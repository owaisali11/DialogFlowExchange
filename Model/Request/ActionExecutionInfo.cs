using Newtonsoft.Json;

namespace CustomExchangeEndpointProxy.Model.Request
{
    public class ActionExecutionInfo
    {
        [JsonProperty("contactId")]
        public long ContactId { get; set; }

        [JsonProperty("busNo")]
        public int BusNo { get; set; }

        [JsonProperty("requestId")]
        public int RequestId { get; set; }

        [JsonProperty("actionType")]
        public string? ActionType { get; set; }

        [JsonProperty("actionId")]
        public int ActionId { get; set; }

        [JsonProperty("scriptName")]
        public string? ScriptName { get; set; }
    }
}
