namespace CustomExchangeEndpointProxy.Model.Response
{
    public class CollectDtmfRules
    {
        public bool detectDtmf { get; set; }
        public bool clearDigits { get; set; }
        public string terminationCharacters { get; set; }
        public bool stripTerminator { get; set; }
        public int interDigitTimeoutMilliseconds { get; set; }
        public int maxDigits { get; set; }

        public static CollectDtmfRules SurveyResponseSettings()
        {
            return new CollectDtmfRules
            {
                clearDigits = true,
                detectDtmf = true,
                interDigitTimeoutMilliseconds = 3000,
                maxDigits = 10,
                stripTerminator = true,
                terminationCharacters = "#"
            };
        }
    }
}
