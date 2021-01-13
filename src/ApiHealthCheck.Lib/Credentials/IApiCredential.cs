namespace ApiHealthCheck.Lib.Credentials
{
    public interface IApiCredential
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
