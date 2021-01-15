namespace ApiHealthCheck.Console.Settings
{
    internal record Urls
    {
        public string ProductApiUrl { get; init; }
        public string ResultApiUrl { get; init; }
        public string ContentApiUrl { get; init; }
        public string TestApiUrl { get; init; }
        public string TestPlayerApiUrl { get; init; }

        public Urls()
        {
            ProductApiUrl = string.Empty;
            ResultApiUrl = string.Empty;
            ContentApiUrl = string.Empty;
            TestApiUrl = string.Empty;
            TestPlayerApiUrl = string.Empty;
        }
    }
}