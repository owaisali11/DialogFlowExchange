using CustomExchangeEndpointProxy.Interface;
using CustomExchangeEndpointProxy.Model.Request;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf.Collections;

namespace CustomExchangeEndpointProxy.Client
{
    public class CustomExchangeEndpointService : ICustomExchangeEndpointService
    {
        private readonly SessionsClient _sessionsClient;
        public CustomExchangeEndpointService(SessionsClient sessionsClient)
        {
            _sessionsClient = sessionsClient;
        }
        public async Task<DetectIntentResponse> DetectIntentAsync(DetectIntentRequest intentRequest)
        {
            if (intentRequest == null)
            {
                throw new ArgumentNullException(nameof(intentRequest), "The intent request cannot be null.");
            }

            var response = await _sessionsClient.DetectIntentAsync(intentRequest);

            return response;
        }


    }
    
}
