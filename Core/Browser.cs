﻿using NLog;
using OpenQA.Selenium;
using TestRail.Utils;

namespace TestRail.Core
{
    public class Browser
    {
        public IWebDriver Driver { get; set; }
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public Browser()
        {
            string browserType = Configurator.GetBrowserType();
            switch (browserType)
            {
                case "chrome":
                    Driver = new DriverFactory().GetChromeDriver();
                    break;
                case "firefox":
                    Driver = new DriverFactory().GetFireFoxDriver();
                    break;
                case "edge":
                    Driver = new DriverFactory().GetEdgeDriver();
                    break;
                default:
                    _logger.Error($"There is no implementation for '{browserType}'!");
                    throw new NotImplementedException($"There is no implementation for '{browserType}' browser!");
            }

            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Configurator.GetTimeOut());
            Driver.Manage().Window.Maximize();
        }
    }
}