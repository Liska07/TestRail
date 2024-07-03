using OpenQA.Selenium;
using TestRail.BaseEntities;
using TestRail.Elements;

namespace TestRail.Pages
{
    public class HeaderPage : BasePage
    {
        private static readonly By _userNameButtonBy = By.ClassName("navigation-username");
        private static readonly By _logoutButtonBy = By.Id("navigation-user-logout");
        private const string _endPoint = "/index.php?/dashboard";

        public HeaderPage(IWebDriver driver) : base(driver)
        {
        }

        public Button UserNameButton() => new Button(driver, _userNameButtonBy);
        public Button LogoutButton() => new Button(driver, _logoutButtonBy);

        public override string GetEndpoint()
        {
            return _endPoint;
        }
        protected override bool EvaluateLoadedStatus()
        {
            try
            {
                return UserNameButton().Displayed;
            }
            catch (Exception ex)
            {
                logger.Error("'User Name Button' on the 'Header Page' is not  displayed! " + ex);
                return false;
            }
        }
    }
}
