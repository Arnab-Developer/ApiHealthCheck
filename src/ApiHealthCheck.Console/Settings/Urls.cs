namespace ApiHealthCheck.Console.Settings
{
    internal record Urls
    {
        public string ProductApiUrl { get; set; }
        public string ResultApiUrl { get; set; }
        public string ContentApiUrl { get; set; }
        public string TestApiUrl { get; set; }
        public string TestPlayerApiUrl { get; set; }

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