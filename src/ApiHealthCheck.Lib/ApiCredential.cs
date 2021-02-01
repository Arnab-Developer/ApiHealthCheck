namespace ApiHealthCheck.Lib
{
    public record ApiCredential
    {
        public string UserName { get; init; }
        public string Password { get; init; }

        public ApiCredential()
        {
            UserName = string.Empty;
            Password = string.Empty;
        }
    }
}
