using ApiHealthCheck.Lib.Settings;
using Xunit;

namespace ApiHealthCheck.LibTest.Settings
{
    public class ExecutionSettingsTest
    {
        [Fact]
        public void ExecutionSettingsSuccessTest()
        {
            ExecutionSettings executionSettings = new(int.Parse("122"));
            Assert.Equal(122, executionSettings.ExecutionFrequency);
        }
    }
}
