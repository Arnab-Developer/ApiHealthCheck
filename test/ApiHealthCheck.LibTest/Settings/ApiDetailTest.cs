using ApiHealthCheck.Lib;
using ApiHealthCheck.Lib.Settings;
using Xunit;

namespace ApiHealthCheck.LibTest.Settings
{
    public class ApiDetailTest
    {
        [Fact]
        public void ApiDetailTrueTest()
        {
            ApiDetail apiDetail = new
            (
                "api1",
                "https://api1/hc",
                new ApiCredential("u1", "p1"),
                true
            );
            Assert.Equal("api1", apiDetail.Name);
            Assert.Equal("https://api1/hc", apiDetail.Url);
            Assert.Equal("u1", apiDetail.ApiCredential!.UserName);
            Assert.Equal("p1", apiDetail.ApiCredential.Password);
            Assert.True(apiDetail.IsEnable);
        }

        [Fact]
        public void ApiDetailFalseTest()
        {
            ApiDetail apiDetail = new
            (
                "api1",
                "https://api1/hc",
                new ApiCredential("u1", "p1"),
                false
            );
            Assert.Equal("api1", apiDetail.Name);
            Assert.Equal("https://api1/hc", apiDetail.Url);
            Assert.Equal("u1", apiDetail.ApiCredential!.UserName);
            Assert.Equal("p1", apiDetail.ApiCredential.Password);
            Assert.False(apiDetail.IsEnable);
        }
    }
}
