namespace ApiHealthCheck.Lib.Credentials
{
    public record ProductApiCredential
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public ProductApiCredential()
        {
            UserName = string.Empty;
            Password = string.Empty;
        }
    }
}