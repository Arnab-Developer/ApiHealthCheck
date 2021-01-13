namespace ApiHealthCheck.Lib.Credentials
{
    public record TestApiCredential : IApiCredential
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public TestApiCredential()
        {
            UserName = string.Empty;
            Password = string.Empty;
        }
    }
}