using CustomExchangeEndpointProxy.Interface;
using CustomExchangeEndpointProxy.Model.Request;
using Microsoft.AspNetCore.Mvc;

namespace CustomExchangeEndpointProxy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomExchangeEndpointController : ControllerBase
    {
        private readonly ICustomExchangeManager _customExchangeManager;
        public CustomExchangeEndpointController(ICustomExchangeManager customExchangeManager)
        {
            _customExchangeManager = customExchangeManager;
        }

        [HttpPost("testBotExchange")]
        public async Task<IActionResult> SendMessage([FromBody] ExternalIntegrationBotExchangeRequest request)
        {
            var response = await _customExchangeManager.InitiateBotExchangeAsync(request);
            return Ok(response);
        }
       
    }
}
