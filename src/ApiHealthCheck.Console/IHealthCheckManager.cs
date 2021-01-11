using ApiHealthCheck.Console.Settings;
using ApiHealthCheck.Lib.Credentials;

namespace ApiHealthCheck.Console
{
    internal interface IHealthCheckManager
    {
        Urls Urls { get; }

        ProductApiCredential Credential { get; }

        void PrintHealthCheckResult();
    }
}