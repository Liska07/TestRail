using NLog;

namespace TestRail.Utils
{
    public static class EnvironmentHelper
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public static string GetEnvironmentVariableOrThrow(string variable)
        {
            var value = Environment.GetEnvironmentVariable(variable);
            if (value == null)
            {
                _logger.Error($"Environment variable {variable} is not set.");
                throw new InvalidOperationException($"Environment variable {variable} is not set.");
            }
            return value;
        }
    }
}