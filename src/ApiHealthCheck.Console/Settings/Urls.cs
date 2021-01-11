namespace ApiHealthCheck.Console.Settings
{
    internal record Urls
    {
        public string ProductApiUrl { get; set; }

        public Urls()
        {
            ProductApiUrl = string.Empty;
        }
    }
}