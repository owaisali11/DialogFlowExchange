using Google.Cloud.Dialogflow.V2;

namespace CustomExchangeEndpointProxy.Interface
{
    public interface ICustomExchangeEndpointService
    {
        Task<DetectIntentResponse> DetectIntentAsync(DetectIntentRequest intentRequest);
    }
}
