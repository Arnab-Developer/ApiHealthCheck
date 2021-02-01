using Xunit;

namespace ApiHealthCheck.Test.Settings
{
    public class UrlsTest
    {
        [Fact]
        public void UrlsSuccessTest()
        {
            Urls urls = new()
            {
                ProductApiUrl = "pu1",
                ResultApiUrl = "ra1",
                ContentApiUrl = "c1",
                TestApiUrl = "t1",
                TestPlayerApiUrl = "tp1"
            };
            Assert.Equal("pu1", urls.ProductApiUrl);
            Assert.Equal("ra1", urls.ResultApiUrl);
            Assert.Equal("c1", urls.ContentApiUrl);
            Assert.Equal("t1", urls.TestApiUrl);
            Assert.Equal("tp1", urls.TestPlayerApiUrl);
        }
    }
}
