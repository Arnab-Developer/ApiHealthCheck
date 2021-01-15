using ApiHealthCheck.Console.Settings;
using ApiHealthCheck.Lib.Credentials;

namespace ApiHealthCheck.Console
{
    /// <summary>
    /// Interface for api health check manager.
    /// </summary>
    internal interface IHealthCheckManager
    {
        /// <summary>
        /// Get api urls for health check.
        /// </summary>
        Urls Urls { get; }

        /// <summary>
        /// Get api credential to access api.
        /// </summary>
        ProductApiCredential Credential { get; }

        /// <summary>
        /// Log api health check result.
        /// </summary>
        /// <returns>Health check result log message.</returns>
        string LogHealthCheckResult();
    }
}