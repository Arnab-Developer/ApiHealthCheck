namespace ApiHealthCheck.Lib.Credentials
{
    public record TestPlayerApiCredential : IApiCredential
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public TestPlayerApiCredential()
        {
            UserName = string.Empty;
            Password = string.Empty;
        }
    }
}