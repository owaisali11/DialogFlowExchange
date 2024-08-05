namespace CustomExchangeEndpointProxy.Model.Response
{
    public class SilenceRules
    {
        public bool engageComfortSequence { get; set; }
        public int botResponseDelayTolerance { get; set; }
        public PromptSequence? comfortPromptSequence { get; set; }
        public int millisecondsToWaitForUserResponse { get; set; }
    }
}
