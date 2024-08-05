using CustomExchangeEndpointProxy.Model.Request;
using CustomExchangeEndpointProxy.Model.Response;

namespace CustomExchangeEndpointProxy.Interface
{
    public interface ICustomExchangeManager
    {
        Task<CustomExchangeResponse_V1> InitiateBotExchangeAsync(ExternalIntegrationBotExchangeRequest request);
    }
}
