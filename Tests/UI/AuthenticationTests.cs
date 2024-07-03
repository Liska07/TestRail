using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using TestRail.BaseEntities;

namespace TestRail.Tests.UI
{
    [Category("AuthenticationTests")]
    [AllureFeature("Authentication Tests")]
    public class AuthenticationTests : BaseTest
    {
        [Test]
        [Category("SmokeTests")]
        [AllureDescription("Check login with the correct username and password")]
        [AllureSeverity(SeverityLevel.blocker)]
        [AllureStory("Login")]
        public void PositiveLogin()
        {
            authenticationStep.SuccessfulLogin();
            Assert.That(dashboardPage.AddProjectButton().Displayed);
        }

        [Test]
        [AllureDescription("Check the error messages if username and password are not entered")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureStory("Login")]
        public void LoginWithoutUserNameAndPassword()
        {
            string expectedUserNameErrorText = "Email/Login is required.";
            string expectedPasswordErrorText = "Password is required.";

            authenticationStep.UnsuccessfulLogin();

            Assert.Multiple(() =>
            {
                Assert.That(loginPage.GetUserNameErrorMessageText(), Is.EqualTo(expectedUserNameErrorText));
                Assert.That(loginPage.GetPasswordErrorMessageText(), Is.EqualTo(expectedPasswordErrorText));
            });
        }

        [Test]
        [AllureDescription("Check the error message if password is less than 5 characters")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureStory("Login")]
        public void LoginWithShortPassword()
        {
            string userName = "UserName";
            string password = "1234";
            string expectedPasswordErrorText = "Password is too short (5 characters required).";

            authenticationStep.UnsuccessfulLogin(userName, password);

            Assert.That(loginPage.GetPasswordErrorMessageText(), Is.EqualTo(expectedPasswordErrorText));
        }

        [Test]
        [Category("SmokeTests")]
        [AllureDescription("Check the error messages if username or password are incorrect")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureStory("Login")]
        [TestCase("UserName", "Password")]
        [TestCase("' OR '1'='1", "' OR '1'='1")]
        [TestCase("<script>alert(1)</script>", "<script>alert(1)</script>")]
        public void LoginWithWrongUserNameOrPassword(string userName, string password)
        {
            string expectedTopErrorText = "Sorry, there was a problem.";
            string expectedLoginErrorText = "Email/Login or Password is incorrect. Please try again.";

            authenticationStep.UnsuccessfulLogin(userName, password);

            Assert.Multiple(() =>
            {
                Assert.That(loginPage.GetTopErrorMessageText(), Is.EqualTo(expectedTopErrorText));
                Assert.That(loginPage.GetLoginErrorMessageText(), Is.EqualTo(expectedLoginErrorText));
            });
        }

        [Test]
        [Category("ToFail")]
        [AllureDescription("Check capturing and attaching a screenshot of the screen upon test failure")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureStory("Login")]
        public void LoginWithEmptyUserNameAndShortPassword()
        {
            string password = "1234";
            string expectedUserNameErrorText = "Email/Login is required.";
            string expectedPasswordErrorText = "Password is too short (5 characters required).";

            authenticationStep.UnsuccessfulLogin(password: password);

            Assert.Multiple(() =>
            {
                Assert.That(loginPage.GetUserNameErrorMessageText(), Is.EqualTo(expectedUserNameErrorText));
                Assert.That(loginPage.GetPasswordErrorMessageText(), Is.EqualTo(expectedPasswordErrorText));
            });
        }

        [Test]
        [Category("SmokeTests")]
        [AllureDescription("Check logout")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureStory("Logout")]
        public void Logout()
        {
            authenticationStep.SuccessfulLogin();
            authenticationStep.Logout();

            Assert.That(loginPage.LoginButton().Displayed);
        }
    }
}
