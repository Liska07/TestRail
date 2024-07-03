using OpenQA.Selenium;
using TestRail.BaseEntities;
using TestRail.Elements;

namespace TestRail.Pages
{
    public class DashboardPage : BasePage
    {
        private static readonly By _addProjectButtonBy = By.Id("sidebar-projects-add");
        private const string _endPoint = "/index.php?/dashboard";

        public DashboardPage(IWebDriver driver) : base(driver)
        {
        }
        
        public Button AddProjectButton() => new Button(driver, _addProjectButtonBy);
        public override string GetEndpoint()
        {
            return _endPoint;
        }
        protected override bool EvaluateLoadedStatus()
        {
            try
            {
                return AddProjectButton().Displayed;
            }
            catch (Exception ex)
            {
                logger.Error("'Add Project Button' on the 'Dashboard Page' is not  displayed! " + ex);
                return false;
            }
        }
    }
}