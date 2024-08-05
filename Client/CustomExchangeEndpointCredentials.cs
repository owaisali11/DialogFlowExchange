using CustomExchangeEndpointProxy.Interface;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Dialogflow.V2;
using Grpc.Auth;

namespace CustomExchangeEndpointProxy.Client
{
    public class CustomExchangeEndpointCredentials : ICustomExchangeEndpointCredentials
    {
        private SessionsClient _sessionsClient;
        public SessionsClient InitializeCredentials(string jsonKeyPath)
        {
            var credential = GoogleCredential.FromJson(jsonKeyPath);

            var sessionsClient = new SessionsClientBuilder
            {
                ChannelCredentials = credential.ToChannelCredentials()
            }.Build();

            return sessionsClient;
        }
    }
}
