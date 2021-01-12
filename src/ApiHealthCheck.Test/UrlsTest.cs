using ApiHealthCheck.Console.Settings;
using Xunit;

namespace ApiHealthCheck.Test
{
    public class UrlsTest
    {
        [Fact]
        public void UrlsSuccessTest()
        {
            Urls urls = new()
            {
                ProductApiUrl = "pu1"
            };
            Assert.Equal("pu1", urls.ProductApiUrl);
        }
    }
}
