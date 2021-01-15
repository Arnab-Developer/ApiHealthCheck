namespace ApiHealthCheck.Lib.Credentials
{
    /// <summary>
    /// Credential to access test api.
    /// </summary>
    public record TestApiCredential : IApiCredential
    {
        public string UserName { get; init; }
        public string Password { get; init; }

        public TestApiCredential()
        {
            UserName = string.Empty;
            Password = string.Empty;
        }
    }
}