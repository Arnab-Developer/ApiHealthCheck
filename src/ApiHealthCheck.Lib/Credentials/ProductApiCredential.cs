namespace ApiHealthCheck.Lib.Credentials
{
    public record ProductApiCredential : IApiCredential
    {
        public string UserName { get; init; }
        public string Password { get; init; }

        public ProductApiCredential()
        {
            UserName = string.Empty;
            Password = string.Empty;
        }
    }
}