using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using OpenQA.Selenium;
using TestRail.Core;
using TestRail.Pages;
using TestRail.Pages.ProjectPages;
using TestRail.Steps.UI;

namespace TestRail.BaseEntities
{
    public class BaseTest : BaseApiTest
    {
        protected IWebDriver driver;
        //Steps
        protected LoginStep loginStep;
        protected ProjectStep projectStep;
        protected NavigationStep navigationStep;
        protected MilestoneStep milestoneStep;
        //Pages
        protected LoginPage loginPage;
        protected DashboardPage dashboardPage;
        protected ProjectPage projectPage;
        protected ProjectListPage projectListPage;
        protected ConfirmationProjectPage confirmationProjectPage;

        [SetUp]
        [AllureBefore("Setup driver")]
        public void SetupBaseTest()
        {
            driver = new Browser().Driver;
            new LoginPage(driver, true);
            //Steps
            loginStep = new LoginStep(driver);
            projectStep = new ProjectStep(driver);
            navigationStep = new NavigationStep(driver);
            milestoneStep = new MilestoneStep(driver);
            //Pages
            loginPage = new LoginPage(driver);
            dashboardPage = new DashboardPage(driver);
            projectPage = new ProjectPage(driver);
            projectListPage = new ProjectListPage(driver);
            confirmationProjectPage = new ConfirmationProjectPage(driver);
        }

        [TearDown]
        [AllureAfter("Driver dispose")]
        public void TearDownBaseTest()
        {
            try
            {
                if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
                {
                    AttachScreenshotToAllure((ITakesScreenshot)driver, "Screenshot");
                    logger.Info("Attached a screenshot to Allure");
                }
            }
            catch (Exception ex)
            {
                logger.Error("Screenshot cannot be attached! " + ex);
                throw;
            }
            finally
            {
                driver.Dispose();
            }
        }

        private void AttachScreenshotToAllure(ITakesScreenshot takesscreenshot, string name)
        {
            Screenshot screenshot = takesscreenshot.GetScreenshot();
            byte[] screenshotByte = screenshot.AsByteArray;
            AllureApi.AddAttachment(name, "image/png", screenshotByte);
        }
    }
}
