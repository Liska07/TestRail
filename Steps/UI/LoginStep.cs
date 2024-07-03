using Allure.NUnit.Attributes;
using OpenQA.Selenium;
using TestRail.BaseEntities;
using TestRail.Utils;

namespace TestRail.Steps.UI
{
    public class LoginStep : BaseStep
    {
        public LoginStep(IWebDriver driver) : base(driver)
        {
        }

        [AllureStep("Successful login")]
        public void SuccessfulLogin()
        {
            Login(EnvironmentHelper.GetEnvironmentVariableOrThrow("TESTRAIL_USERNAME"),
                EnvironmentHelper.GetEnvironmentVariableOrThrow("TESTRAIL_PASSWORD"));
            logger.Info("Successful login");
        }

        public void UnsuccessfulLogin(string userName = "", string password = "")
        {
            Login(userName, password);
        }
        private void Login(string userName, string password)
        {
            loginPage.UserNameField().SendKeys(userName);
            loginPage.PasswordFeld().SendKeys(password);
            loginPage.LoginButton().Click();
        }
    }
}
