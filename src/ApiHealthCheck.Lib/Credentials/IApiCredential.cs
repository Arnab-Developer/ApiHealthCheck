namespace ApiHealthCheck.Lib.Credentials
{
    /// <summary>
    /// Interface for api credential.
    /// </summary>
    public interface IApiCredential
    {
        /// <summary>
        /// User name to access api.
        /// </summary>
        public string UserName { get; init; }

        /// <summary>
        /// Password to access api.
        /// </summary>
        public string Password { get; init; }
    }
}
