namespace ApiHealthCheck.Console.Settings
{
    internal record Urls
    {
        public string ProductApiUrl { get; set; }
        public string ResultApiUrl { get; set; }

        public Urls()
        {
            ProductApiUrl = string.Empty;
            ResultApiUrl = string.Empty;
        }
    }
}