namespace ApiHealthCheck.Lib
{
    /// <summary>
    /// Interface to check api health.
    /// </summary>
    public interface IHealthCheck
    {
        /// <summary>
        /// Check api health.
        /// </summary>
        /// <param name="url">Url of the api.</param>
        /// <param name="credential">Credential to access the api if any.</param>
        /// <returns>True if the api is healthy, false if not.</returns>
        bool IsApiHealthy(string url, ApiCredential? credential = null);
    }
}