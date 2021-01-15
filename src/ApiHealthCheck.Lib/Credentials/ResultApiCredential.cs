namespace ApiHealthCheck.Lib.Credentials
{
    /// <summary>
    /// Credential to access result api.
    /// </summary>
    public record ResultApiCredential : IApiCredential
    {
        public string UserName { get; init; }
        public string Password { get; init; }

        public ResultApiCredential()
        {
            UserName = string.Empty;
            Password = string.Empty;
        }
    }
}