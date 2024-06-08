using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using NLog;
using OpenQA.Selenium;
using TestRail.Core;
using TestRail.Pages;
using TestRail.Pages.ProjectPages;
using TestRail.Steps.UI;

namespace TestRail.BaseEntities
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [AllureNUnit]
    [AllureOwner("EAntonova")]
    [AllureEpic("TestRail")]
    public class BaseTest
    {
        protected IWebDriver driver;
        protected readonly Logger logger = LogManager.GetCurrentClassLogger();
        //Steps
        protected UserStep userStep;
        protected ProjectStep projectStep;
        protected NavigationStep navigationStep;
        //Pages
        protected LoginPage loginPage;
        protected DashboardPage dashboardPage;
        protected AddProjectPage addProjectPage;
        protected ProjectListPage projectListPage;
        protected ConfirmationPage confirmationPage;

        [OneTimeSetUp]
        [AllureBefore("Clear allure-results directory")]
        public static void OneTimeSetUp()
        {
            AllureLifecycle.Instance.CleanupResultDirectory();
        }

        [SetUp]
        [AllureBefore("Set up driver")]
        public void Setup()
        {
            driver = new Browser().Driver;
            new LoginPage(driver, true);
            //Steps
            userStep = new UserStep(driver);
            projectStep = new ProjectStep(driver);
            navigationStep = new NavigationStep(driver);
            //Pages
            loginPage = new LoginPage(driver);
            dashboardPage = new DashboardPage(driver);
            addProjectPage = new AddProjectPage(driver);
            projectListPage = new ProjectListPage(driver);
            confirmationPage = new ConfirmationPage(driver);
        }

        [TearDown]
        [AllureAfter("Driver quite")]
        public void TearDown()
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
