using Xunit;

namespace ApiHealthCheck.Test.Settings
{
    public class UrlsIsEnableTest
    {
        [Fact]
        public void UrlsIsEnableTrueTest()
        {
            UrlsIsEnable urlsIsEnable = new()
            {
                IsCheckProductApi = true,
                IsCheckResultApi = true,
                IsCheckContentApi = true,
                IsCheckTestApi = true,
                IsCheckTestPlayerApi = true
            };
            Assert.True(urlsIsEnable.IsCheckProductApi);
            Assert.True(urlsIsEnable.IsCheckResultApi);
            Assert.True(urlsIsEnable.IsCheckContentApi);
            Assert.True(urlsIsEnable.IsCheckTestApi);
            Assert.True(urlsIsEnable.IsCheckTestPlayerApi);
        }

        [Fact]
        public void UrlsIsEnableFalseTest()
        {
            UrlsIsEnable urlsIsEnable = new()
            {
                IsCheckProductApi = false,
                IsCheckResultApi = false,
                IsCheckContentApi = false,
                IsCheckTestApi = false,
                IsCheckTestPlayerApi = false
            };
            Assert.False(urlsIsEnable.IsCheckProductApi);
            Assert.False(urlsIsEnable.IsCheckResultApi);
            Assert.False(urlsIsEnable.IsCheckContentApi);
            Assert.False(urlsIsEnable.IsCheckTestApi);
            Assert.False(urlsIsEnable.IsCheckTestPlayerApi);
        }
    }
}
