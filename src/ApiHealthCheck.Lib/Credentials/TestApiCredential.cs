namespace ApiHealthCheck.Lib.Credentials
{
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