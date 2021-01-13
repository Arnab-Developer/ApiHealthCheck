namespace ApiHealthCheck.Lib.Credentials
{
    public record ContentApiCredential : IApiCredential
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public ContentApiCredential()
        {
            UserName = string.Empty;
            Password = string.Empty;
        }
    }
}