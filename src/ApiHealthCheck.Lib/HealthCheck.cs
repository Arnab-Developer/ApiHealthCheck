using ApiHealthCheck.Lib.Credentials;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ApiHealthCheck.Lib
{
    public class HealthCheck : IHealthCheck
    {
        bool IHealthCheck.IsApiHealthy(string url, ProductApiCredential? credential)
        {
            HttpClient httpClient = new();
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
