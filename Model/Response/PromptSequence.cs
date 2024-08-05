using Newtonsoft.Json;

namespace CustomExchangeEndpointProxy.Model.Response
{
    public class PromptSequence
    {
        [JsonProperty("prompts")]
        public List<PromptDefinition> Prompts { get; set; }

        public PromptSequence()
        {
            Prompts = new List<PromptDefinition>();
        }
    }
}
