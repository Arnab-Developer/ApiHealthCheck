namespace ApiHealthCheck.Lib.Credentials
{
    public interface IApiCredential
    {
        public string UserName { get; init; }
        public string Password { get; init; }
    }
}
