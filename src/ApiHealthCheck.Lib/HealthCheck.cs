using System.Net.Http.Headers;
using System.Text;

namespace ApiHealthCheck.Lib
{
    public class HealthCheck : IHealthCheck
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HealthCheck(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        bool IHealthCheck.IsApiHealthy(string url, ApiCredential? credential)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            if (credential != null)
            {
                var byteArray = Encoding.ASCII.GetBytes($"{credential.UserName}:{credential.Password}");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Basic", Convert.ToBase64String(byteArray));
            }
            HttpResponseMessage consultationApiResponseMessage = httpClient.GetAsync(url).Result;
            return consultationApiResponseMessage.IsSuccessStatusCode;
        }
    }
}
