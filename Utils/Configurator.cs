using System.Reflection;
using System.Text.Json;
using NLog;

namespace TestRail.Utils
{
    public class Configurator
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private static AppSettings ReadConfiguration()
        {
            var appSettingsPath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                ?? throw new InvalidOperationException("Failed to determine the directory of the executing assembly."),
                "appsettings.json");

            if (!File.Exists(appSettingsPath))
            {
                throw new FileNotFoundException($"The configuration file 'appsettings.json' was not found at path: {appSettingsPath}");
            }

            var appSettingsText = File.ReadAllText(appSettingsPath);

            return JsonSerializer.Deserialize<AppSettings>(appSettingsText) 
                ?? throw new InvalidOperationException("Failed to deserialize the application settings from 'appsettings.json'.");
        }

        public static string GetBaseURL()
        {
            string? baseUrl = ReadConfiguration().TestRailBaseURL;

            if (string.IsNullOrEmpty(baseUrl))
            {
                _logger.Error("TestRailBaseURL in AppSettings is not configured or is null.");
                throw new InvalidOperationException("TestRailBaseURL in AppSettings is not configured or is null.");
            }
            return baseUrl;
        }

        public static double GetTimeOut()
        {
            double? timeout = ReadConfiguration().TimeOut;

            if (timeout == null)
            {
                _logger.Error("Timeout in AppSettings is null.");
                throw new Exception("Timeout in AppSettings is null.");
            }
            return timeout.Value;
        }

        public static string GetBrowserType()
        {
            string? browserType = ReadConfiguration().BrowserType;

            if (browserType == null)
            {
                _logger.Error("BrowserType in AppSettings is null.");
                throw new Exception("BrowserType in AppSettings is null.");
            }
            return browserType.ToLower();
        }
    }
}
