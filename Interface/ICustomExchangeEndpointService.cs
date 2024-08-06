using Google.Cloud.Dialogflow.V2;

namespace CustomExchangeEndpointProxy.Interface
{
    public interface ICustomExchangeEndpointService
    {
        public SessionsClient InitializeSessionsClient(string jsonKey);
        Task<DetectIntentResponse> DetectIntentAsync(DetectIntentRequest intentRequest, string jsonKey);

    }
}
