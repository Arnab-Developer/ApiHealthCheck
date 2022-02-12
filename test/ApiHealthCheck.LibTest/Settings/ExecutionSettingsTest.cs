using ApiHealthCheck.Lib.Settings;
using Tynamix.ObjectFiller;
using Xunit;

namespace ApiHealthCheck.LibTest.Settings;

public class ExecutionSettingsTest
{
    [Fact]
    public void ExecutionSettingsSuccessTest()
    {
        int expectedExecutionFrequency = Randomizer<int>.Create();
        ExecutionSettings executionSettings = new(expectedExecutionFrequency);
        Assert.Equal(expectedExecutionFrequency, executionSettings.ExecutionFrequency);
    }
}
