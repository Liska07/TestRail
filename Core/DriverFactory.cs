using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace TestRail.Core
{
    public class DriverFactory
    {
        public IWebDriver GetChromeDriver()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--incognito");
            chromeOptions.AddArgument("--headless");
            chromeOptions.AddArgument("--disable-gpu");
            chromeOptions.AddArgument("--disable-extensions");
            chromeOptions.AddArgument("--remote-debugging-pipe");

            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            return new ChromeDriver(chromeOptions);
        }
        public IWebDriver GetFireFoxDriver()
        {
            var firefoxOptions = new FirefoxOptions();
            firefoxOptions.AddArgument("--private");
            firefoxOptions.AddArgument("--headless");
            //firefoxOptions.Profile = new FirefoxProfileManager().GetProfile("default");
            //firefoxOptions.Profile.SetPreference("extensions.enabledScopes", 0);
            //firefoxOptions.Profile.SetPreference("layers.acceleration.disabled", true);

            new DriverManager().SetUpDriver(new FirefoxConfig());
            return new FirefoxDriver(firefoxOptions);
        }

        public IWebDriver GetEdgeDriver()
        {
            var edgeOptions = new EdgeOptions();
            edgeOptions.AddArgument("--inprivate");
            //edgeOptions.AddArgument("--headless");
            edgeOptions.AddArgument("--disable-gpu");
            edgeOptions.AddArgument("--disable-extensions");

            new DriverManager().SetUpDriver(new EdgeConfig(), VersionResolveStrategy.MatchingBrowser);
            return new EdgeDriver(edgeOptions);
        }
    }
}