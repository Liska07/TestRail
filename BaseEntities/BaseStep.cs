using NLog;
using OpenQA.Selenium;
using TestRail.Pages;
using TestRail.Pages.ProjectPages;

namespace TestRail.BaseEntities
{
    public class BaseStep
    {
        protected IWebDriver driver;
        protected LoginPage loginPage;
        protected DashboardPage dashboardPage;
        protected AddProjectPage addProjectPage;
        protected ProjectListPage projectListPage;
        protected ConfirmationPage confirmationPage;
        protected readonly Logger logger = LogManager.GetCurrentClassLogger();
        public BaseStep(IWebDriver driver)
        {
            this.driver = driver;
            loginPage = new LoginPage(driver);
            dashboardPage = new DashboardPage(driver);
            addProjectPage = new AddProjectPage(driver);
            projectListPage = new ProjectListPage(driver);
            confirmationPage = new ConfirmationPage(driver);
        }
    }
}
