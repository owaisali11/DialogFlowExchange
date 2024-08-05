namespace CustomExchangeEndpointProxy.Model.Request
{
    public class SystemTelemetryData
    {
        public string? consumerProcessHost { get; set; }
        public string? consumerProcessName { get; set; }
        public string? consumerProcessVersion { get; set; }
        public string? inContactClusterAlias { get; set; }
        public string? inContactScriptEngineHost { get; set; }
        public Dictionary<string, string>? consumerMetaData { get; set; }

    }
}
