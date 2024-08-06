using Newtonsoft.Json;

namespace CustomExchangeEndpointProxy.Model
{
    public class ActionRequestResponse
    {
        [JsonProperty("branchName")]
        public string BranchName { get; set; }

        [JsonProperty("nameValueParameters")]
        public Dictionary<string, object> NameValueParameters { get; set; }

   
        public ActionRequestResponse()
        {
            NameValueParameters = new Dictionary<string, object>();
        }
    }
}