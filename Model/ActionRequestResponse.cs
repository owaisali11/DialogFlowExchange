namespace CustomExchangeEndpointProxy.Model
{
    public class ActionRequestResponse
    {
        public string branchName { get; set; }
        public Dictionary<string, object> nameValueParameters { get; set; }

   
        public ActionRequestResponse()
        {
            nameValueParameters = new Dictionary<string, object>();
        }
    }
}