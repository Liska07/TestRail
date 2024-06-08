using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestRail.Utils;

namespace TestRail.BaseEntities
{
    public abstract class BasePage : LoadableComponent<BasePage>
    {
        protected IWebDriver driver;
        protected int projectId;
        protected readonly Logger logger = LogManager.GetCurrentClassLogger();
        protected BasePage(IWebDriver driver, bool openPageByUrl = false)
        {
            this.driver = driver;
            if (openPageByUrl)
            {
                ExecuteLoad();
                Load();
            }
        }

        protected BasePage(IWebDriver driver, int projectId, bool openPageByUrl = false)
        {
            this.driver = driver;
            this.projectId = projectId;
            if (openPageByUrl)
            {
                ExecuteLoad();
                Load();
            }
        }

        public abstract string GetEndpoint();

        protected override void ExecuteLoad()
        {
            driver.Navigate().GoToUrl(Configurator.GetBaseURL() + GetEndpoint());
        }
    }
}
