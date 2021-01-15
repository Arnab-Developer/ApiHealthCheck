namespace ApiHealthCheck.Lib.Credentials
{
    public record ContentApiCredential : IApiCredential
    {
        public string UserName { get; init; }
        public string Password { get; init; }

        public ContentApiCredential()
        {
            UserName = string.Empty;
            Password = string.Empty;
        }
    }
}