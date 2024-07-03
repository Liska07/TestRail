using NLog;
using OpenQA.Selenium;
using TestRail.Pages;
using TestRail.Pages.MilestonePages;
using TestRail.Pages.ProjectPages;

namespace TestRail.BaseEntities
{
    public class BaseStep
    {
        protected IWebDriver driver;
        protected LoginPage loginPage;
        protected DashboardPage dashboardPage;
        protected ProjectPage projectPage;
        protected ProjectListPage projectListPage;
        protected ConfirmationProjectPage confirmationProjectPage;
        protected ConfirmationMilestonePage confirmationMilestonePage;
        protected readonly Logger logger = LogManager.GetCurrentClassLogger();
        public BaseStep(IWebDriver driver)
        {
            this.driver = driver;
            loginPage = new LoginPage(driver);
            dashboardPage = new DashboardPage(driver);
            projectPage = new ProjectPage(driver);
            projectListPage = new ProjectListPage(driver);
            confirmationProjectPage = new ConfirmationProjectPage(driver);
            confirmationMilestonePage = new ConfirmationMilestonePage(driver);
        }
    }
}
