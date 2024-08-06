using CustomExchangeEndpointProxy.Interface;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Dialogflow.V2;
using Grpc.Auth;


namespace CustomExchangeEndpointProxy.Client
{
    public class CustomExchangeEndpointService : ICustomExchangeEndpointService
    {
        private readonly IConfiguration _configuration;
        public CustomExchangeEndpointService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public SessionsClient InitializeSessionsClient(string jsonKey)
        {
            var credential = GoogleCredential.FromJson(jsonKey);

            var sessionsClient = new SessionsClientBuilder
            {
                ChannelCredentials = credential.ToChannelCredentials()
            }.Build();

            return sessionsClient;
        }
        public async Task<DetectIntentResponse> DetectIntentAsync(DetectIntentRequest intentRequest, string jsonKey)
        {
            if (intentRequest == null)
            {
                throw new ArgumentNullException(nameof(intentRequest), "The intent request cannot be null.");
            }
            var sessionClient = InitializeSessionsClient(jsonKey);
            var response = await sessionClient.DetectIntentAsync(intentRequest);

            return response;
        }

    }
    
}
