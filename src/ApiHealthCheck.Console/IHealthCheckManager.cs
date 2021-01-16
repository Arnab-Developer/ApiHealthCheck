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
        /// Get api credential to access product api.
        /// </summary>
        ProductApiCredential ProductApiCredential { get; }

        /// <summary>
        /// Get api credential to access result api.
        /// </summary>
        ResultApiCredential ResultApiCredential { get; }

        /// <summary>
        /// Get api credential to access content api.
        /// </summary>
        ContentApiCredential ContentApiCredential { get; }

        /// <summary>
        /// Get api credential to access test api.
        /// </summary>
        TestApiCredential TestApiCredential { get; }

        /// <summary>
        /// Get api credential to access test player api.
        /// </summary>
        TestPlayerApiCredential TestPlayerApiCredential { get; }

        /// <summary>
        /// Log api health check result.
        /// </summary>
        /// <returns>Health check result log message.</returns>
        string LogHealthCheckResult();
    }
}