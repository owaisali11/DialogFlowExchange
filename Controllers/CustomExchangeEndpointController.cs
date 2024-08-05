using CustomExchangeEndpointProxy.Interface;
using CustomExchangeEndpointProxy.Manager;
using CustomExchangeEndpointProxy.Model;
using CustomExchangeEndpointProxy.Model.Request;
using CustomExchangeEndpointProxy.Model.Response;
using Google.Cloud.Dialogflow.V2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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
