namespace ApiHealthCheck.Lib
{
    public interface IHealthCheck
    {
        bool IsApiHealthy(string url, ApiCredential? credential = null);
    }
}