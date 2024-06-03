using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using TestRail.BaseEntities;
using TestRail.Pages;
using TestRail.Utils;
using TestRail.Steps.UI;

namespace TestRail.Tests.UI
{
    [Category("TestRail")]
    [AllureEpic("TestRail")]
    [AllureFeature("Basic Functionality")]
    [AllureStory("Login")]
    public class LoginTests : BaseTest
    {
        [Test]
        [AllureDescription("Check login with the correct username and password")]
        [AllureSeverity(SeverityLevel.blocker)]
        public void PositiveLogin()
        {
            Assert.That(userStep.SuccessfulLogin().AddProjectButton().Displayed);
        }

        [Test]
        [AllureDescription("Check the error messages if username and password are not entered")]
        [AllureSeverity(SeverityLevel.normal)]
        public void LoginWithoutUserNameAndPassword()
        {
            string expectedUserNameErrorText = "Email/Login is required.";
            string expectedPasswordErrorText = "Password is required.";

            LoginPage loginPage = userStep.UnsuccessfulLogin();

            string actualUserNameErrorText = loginPage.GetUserNameErrorMessageText();
            string actualPasswordErrorText = loginPage.GetPasswordErrorMessageText();

            Assert.Multiple(() =>
            {
                Assert.That(actualUserNameErrorText, Is.EqualTo(expectedUserNameErrorText));
                Assert.That(actualPasswordErrorText, Is.EqualTo(expectedPasswordErrorText));
            });
        }

        [Test]
        [AllureDescription("Check the error message if password is less than 5 characters")]
        [AllureSeverity(SeverityLevel.normal)]
        public void LoginWithShortPassword()
        {
            string userName = "WrongUserName";
            string password = "1234";
            string expectedPasswordErrorText = "Password is too short (5 characters required).";

            LoginPage loginPage = userStep.UnsuccessfulLogin(userName, password);

            string actualPasswordErrorText = loginPage.GetPasswordErrorMessageText();

            Assert.That(actualPasswordErrorText, Is.EqualTo(expectedPasswordErrorText));
        }

        [Test]
        [AllureDescription("Check the error messages if username or password are incorrect")]
        [AllureSeverity(SeverityLevel.critical)]
        [TestCaseSource(nameof(TestCases))]
        public void LoginWithWrongUserNameOrPassword(string userName, string password)
        {
            string expectedTopErrorText = "Sorry, there was a problem.";
            string expectedLoginErrorText = "Email/Login or Password is incorrect. Please try again.";

            LoginPage loginPage = userStep.UnsuccessfulLogin(userName, password);

            string actualTopErrorText = loginPage.GetTopErrorMessageText();
            string actualLoginErrorText = loginPage.GetLoginErrorMessageText();

            Assert.Multiple(() =>
            {
                Assert.That(actualTopErrorText, Is.EqualTo(expectedTopErrorText));
                Assert.That(actualLoginErrorText, Is.EqualTo(expectedLoginErrorText));
            });
        }

        private static readonly object[] TestCases =
        {
            new string[] { EnvironmentHelper.GetEnvironmentVariableOrThrow("TESTRAIL_USERNAME"),  "12345"},
            new string[] { "WrongUserName", EnvironmentHelper.GetEnvironmentVariableOrThrow("TESTRAIL_PASSWORD")},
            new string[] { "' OR '1'='1", "' OR '1'='1" },
            new string[] { "<script>alert(1)</script>", "<script>alert(1)</script>" }
        };

        [Test]
        [AllureDescription("Check the error messages if username is empty and password is less than 5 characters")]
        [AllureSeverity(SeverityLevel.normal)]
        public void LoginWithEmptyUserNameAndShortPassword()
        {
            string password = "1234";
            string expectedUserNameErrorText = "Email/Login is required.";
            string expectedPasswordErrorText = "Password is too short (5 characters required).";

            LoginPage loginPage = userStep.UnsuccessfulLogin(password: password);

            string actualUserNameErrorText = loginPage.GetUserNameErrorMessageText();
            string actualPasswordErrorText = loginPage.GetPasswordErrorMessageText();

            Assert.Multiple(() =>
            {
                Assert.That(actualUserNameErrorText, Is.EqualTo(expectedUserNameErrorText));
                Assert.That(actualPasswordErrorText, Is.EqualTo(expectedPasswordErrorText));
            });
        }
    }
}
