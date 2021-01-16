namespace ApiHealthCheck.Console.Settings
{
    internal record UrlsIsEnable
    {
        public bool IsCheckProductApi { get; set; }
        public bool IsCheckResultApi { get; set; }
        public bool IsCheckContentApi { get; set; }
        public bool IsCheckTestApi { get; set; }
        public bool IsCheckTestPlayerApi { get; set; }
    }
}
