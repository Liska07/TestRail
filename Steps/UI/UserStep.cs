using Allure.NUnit.Attributes;
using OpenQA.Selenium;
using TestRail.BaseEntities;
using TestRail.Pages;
using TestRail.Utils;

namespace TestRail.Steps.UI
{
    public class UserStep : BaseStep
    {
        public UserStep(IWebDriver driver) : base(driver)
        {
        }

        [AllureStep("Successful login")]
        public DashboardPage SuccessfulLogin()
        {
            Login(EnvironmentHelper.GetEnvironmentVariableOrThrow("TESTRAIL_USERNAME"),
                EnvironmentHelper.GetEnvironmentVariableOrThrow("TESTRAIL_PASSWORD"));
            logger.Info("Successful login");
            return dashboardPage;
        }

        public LoginPage UnsuccessfulLogin(string userName = "", string password = "")
        {
            Login(userName, password);
            return loginPage;
        }
        public void Login(string userName, string password)
        {
            loginPage.UserNameField().SendKeys(userName);
            loginPage.PasswordFeld().SendKeys(password);
            loginPage.LoginButton().Click();
        }
    }
}
