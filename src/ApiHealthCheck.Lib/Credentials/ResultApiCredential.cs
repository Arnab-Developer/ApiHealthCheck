namespace ApiHealthCheck.Lib.Credentials
{
    public record ResultApiCredential : IApiCredential
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public ResultApiCredential()
        {
            UserName = string.Empty;
            Password = string.Empty;
        }
    }
}