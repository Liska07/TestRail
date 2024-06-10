using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using TestRail.BaseEntities;
using TestRail.Utils;

namespace TestRail.Tests.UI
{
    [Category("LoginTests")]
    [AllureFeature("LoginTests")]
    public class LoginTests : BaseTest
    {
        [Test]
        [Category("SmokeTests")]
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

            userStep.UnsuccessfulLogin();

            Assert.Multiple(() =>
            {
                Assert.That(loginPage.GetUserNameErrorMessageText(), Is.EqualTo(expectedUserNameErrorText));
                Assert.That(loginPage.GetPasswordErrorMessageText(), Is.EqualTo(expectedPasswordErrorText));
            });
        }

        [Test]
        [AllureDescription("Check the error message if password is less than 5 characters")]
        [AllureSeverity(SeverityLevel.normal)]
        public void LoginWithShortPassword()
        {
            string userName = "UserName";
            string password = "1234";
            string expectedPasswordErrorText = "Password is too short (5 characters required).";

            userStep.UnsuccessfulLogin(userName, password);

            Assert.That(loginPage.GetPasswordErrorMessageText(), Is.EqualTo(expectedPasswordErrorText));
        }

        [Test]
        [Category("SmokeTests")]
        [AllureDescription("Check the error messages if username or password are incorrect")]
        [AllureSeverity(SeverityLevel.critical)]
        [TestCaseSource(nameof(TestCasesForLoginWithWrongData))]
        public void LoginWithWrongUserNameOrPassword(string userName, string password)
        {
            string expectedTopErrorText = "Sorry, there was a problem.";
            string expectedLoginErrorText = "Email/Login or Password is incorrect. Please try again.";

            userStep.UnsuccessfulLogin(userName, password);

            Assert.Multiple(() =>
            {
                Assert.That(loginPage.GetTopErrorMessageText(), Is.EqualTo(expectedTopErrorText));
                Assert.That(loginPage.GetLoginErrorMessageText(), Is.EqualTo(expectedLoginErrorText));
            });
        }

        private static readonly string[][] TestCasesForLoginWithWrongData =
        [
            [EnvironmentHelper.GetEnvironmentVariableOrThrow("TESTRAIL_USERNAME"), "12345"],
            ["WrongUserName", EnvironmentHelper.GetEnvironmentVariableOrThrow("TESTRAIL_PASSWORD")],
            ["' OR '1'='1", "' OR '1'='1"],
            ["<script>alert(1)</script>", "<script>alert(1)</script>"]
        ];

        [Test]
        [AllureDescription("Check the error messages if username is empty and password is less than 5 characters")]
        [AllureSeverity(SeverityLevel.normal)]
        public void LoginWithEmptyUserNameAndShortPassword()
        {
            string password = "1234";
            string expectedUserNameErrorText = "Email/Login is required.";
            string expectedPasswordErrorText = "Password is too short (5 characters required).";

            userStep.UnsuccessfulLogin(password: password);

            Assert.Multiple(() =>
            {
                Assert.That(loginPage.GetUserNameErrorMessageText(), Is.EqualTo(expectedUserNameErrorText));
                Assert.That(loginPage.GetPasswordErrorMessageText(), Is.EqualTo(expectedPasswordErrorText));
            });
        }
    }
}
