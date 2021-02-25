using ApiHealthCheck.Console.Settings;
using Xunit;

namespace ApiHealthCheck.ConsoleTest.Settings
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
