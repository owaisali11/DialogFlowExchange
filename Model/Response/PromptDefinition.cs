namespace CustomExchangeEndpointProxy.Model.Response
{
    public class PromptDefinition
    {
        public string? transcript { get; set; }
        public string base64EncodedG711ulawWithWavHeader { get; set; }
        public string audioFilePath { get; set; }
        public string textToSpeech { get; set; }
        public object mediaSpecificObject { get; set; }
    }
}
