namespace ApiHealthCheck.Lib.Credentials
{
    public record TestPlayerApiCredential : IApiCredential
    {
        public string UserName { get; init; }
        public string Password { get; init; }

        public TestPlayerApiCredential()
        {
            UserName = string.Empty;
            Password = string.Empty;
        }
    }
}