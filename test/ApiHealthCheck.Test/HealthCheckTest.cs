using ApiHealthCheck.Lib;
using System;
using Xunit;

namespace ApiHealthCheck.Test
{
    public class HealthCheckTest
    {
        [Fact]
        public void IsApiHealthySuccessTest()
        {
            IHealthCheck healthCheck = new HealthCheck();
            bool isHealthy = healthCheck.IsApiHealthy("https://google.com", new ApiCredential("jon", "pwd1"));
            Assert.True(isHealthy);
        }

        [Fact]
        public void IsApiHealthyFailTest()
        {
            IHealthCheck healthCheck = new HealthCheck();
            Assert.Throws<AggregateException>(() =>
            {
                healthCheck.IsApiHealthy("http://google.com1", new ApiCredential("jon", "pwd1"));
            });
        }

        [Fact]
        public void IsApiHealthyNullCredentialTest()
        {
            IHealthCheck healthCheck = new HealthCheck();
            bool isHealthy = healthCheck.IsApiHealthy("https://google.com");
            Assert.True(isHealthy);
        }
    }
}
