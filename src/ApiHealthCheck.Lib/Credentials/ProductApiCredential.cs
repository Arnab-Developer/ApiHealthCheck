namespace ApiHealthCheck.Lib.Credentials
{
    /// <summary>
    /// Credential to access product api.
    /// </summary>
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