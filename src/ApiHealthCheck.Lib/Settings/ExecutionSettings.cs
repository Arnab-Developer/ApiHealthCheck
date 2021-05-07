namespace ApiHealthCheck.Lib.Settings
{
    public record ExecutionSettings
    {
        public int ExecutionFrequency { get; init; }

        public ExecutionSettings()
        {
            ExecutionFrequency = 0;
        }

        public ExecutionSettings(int executionFrequency)
        {
            ExecutionFrequency = executionFrequency;
        }
    }
}
