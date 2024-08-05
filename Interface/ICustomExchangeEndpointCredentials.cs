using Google.Cloud.Dialogflow.V2;

namespace CustomExchangeEndpointProxy.Interface
{
    public interface ICustomExchangeEndpointCredentials
    {
        public SessionsClient InitializeCredentials(string jsonKeyPath);
    }
}
