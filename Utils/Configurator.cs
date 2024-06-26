﻿using System.Reflection;
using System.Text.Json;
using NLog;

namespace TestRail.Utils
{
    public class Configurator
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private static AppSettings ReadConfiguration()
        {
            string? directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (directory == null)
            {
                string errorMessage = "Failed to determine the directory of the executing assembly.";
                _logger.Error(errorMessage);
                throw new InvalidOperationException(errorMessage);
            }

            var appSettingsPath = Path.Combine(directory, "appsettings.json");

            if (!File.Exists(appSettingsPath))
            {
                string errorMessage = $"The configuration file 'appsettings.json' was not found at path: {appSettingsPath}";
                _logger.Error(errorMessage);
                throw new FileNotFoundException(errorMessage);
            }

            var appSettingsText = File.ReadAllText(appSettingsPath);

            var appSettings = JsonSerializer.Deserialize<AppSettings>(appSettingsText);

            if (appSettings == null)
            {
                string errorMessage = "Failed to deserialize the application settings from 'appsettings.json'.";
                _logger.Error(errorMessage);
                throw new InvalidOperationException(errorMessage);
            }

            return appSettings;
        }

        public static string GetBaseURL()
        {
            string? baseUrl = ReadConfiguration().TestRailBaseURL;

            if (string.IsNullOrEmpty(baseUrl))
            {
                string errorMessage = "TestRailBaseURL in AppSettings is not configured or is null.";
                _logger.Error(errorMessage);
                throw new InvalidOperationException(errorMessage);
            }
            return baseUrl;
        }

        public static double GetTimeOut()
        {
            double? timeout = ReadConfiguration().TimeOut;

            if (timeout == null)
            {
                string errorMessage = "Timeout in AppSettings is null.";
                _logger.Error(errorMessage);
                throw new InvalidOperationException(errorMessage);
            }
            return timeout.Value;
        }

        public static string GetBrowserType()
        {
            string? browserType = ReadConfiguration().BrowserType;

            if (browserType == null)
            {
                string errorMessage = "BrowserType in AppSettings is null.";
                _logger.Error(errorMessage);
                throw new InvalidOperationException(errorMessage);
            }
            return browserType.ToLower();
        }

        private static DbSettings GetDbSettings()
        {
            DbSettings? dbSettings = ReadConfiguration().DbSettings;

            if (dbSettings == null)
            {
                string errorMessage = "DbSettings in AppSettings is null.";
                _logger.Error(errorMessage);
                throw new InvalidOperationException(errorMessage);
            }
            return dbSettings;
        }

        public static string GetServer()
        {
            string? server = GetDbSettings().Server;

            if (server == null)
            {
                string errorMessage = "Server (DbSettings) in AppSettings is null.";
                _logger.Error(errorMessage);
                throw new InvalidOperationException(errorMessage);
            }
            return server;
        }

        public static string GetPort()
        {
            string? port = GetDbSettings().Port;

            if (port == null)
            {
                string errorMessage = "Port (DbSettings) in AppSettings is null.";
                _logger.Error(errorMessage);
                throw new InvalidOperationException(errorMessage);
            }
            return port;
        }

        public static string GetName()
        {
            string? name = GetDbSettings().Name;

            if (name == null)
            {
                string errorMessage = "Name (DbSettings) in AppSettings is null.";
                _logger.Error(errorMessage);
                throw new InvalidOperationException(errorMessage);
            }
            return name;
        }

        public static string GetUserName()
        {
            string? userName = GetDbSettings().UserName;

            if (userName == null)
            {
                string errorMessage = "UserName (DbSettings) in AppSettings is null.";
                _logger.Error(errorMessage);
                throw new InvalidOperationException(errorMessage);
            }
            return userName;
        }

        public static string GetPassword()
        {
            string? password = GetDbSettings().Password;

            if (password == null)
            {
                string errorMessage = "Password (DbSettings) in AppSettings is null.";
                _logger.Error(errorMessage);
                throw new InvalidOperationException(errorMessage);
            }
            return password;
        }
    }
}
