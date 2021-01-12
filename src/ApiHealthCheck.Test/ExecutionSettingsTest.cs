using ApiHealthCheck.Console.Settings;
using Xunit;

namespace ApiHealthCheck.Test
{
    public class ExecutionSettingsTest
    {
        [Fact]
        public void ExecutionSettingsSuccessTest()
        {
            ExecutionSettings executionSettings = new()
            {
                ExecutionFrequency = int.Parse("122")
            };
            Assert.Equal(122, executionSettings.ExecutionFrequency);
        }
    }
}
