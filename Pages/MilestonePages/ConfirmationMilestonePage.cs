using OpenQA.Selenium;
using TestRail.BaseEntities;
using TestRail.Elements;

namespace TestRail.Pages.MilestonePages
{
    public class ConfirmationMilestonePage : BasePage
    {
        private static readonly By _okButtonBy = By.CssSelector("[data-testid='caseFieldsTabDeleteDialogButtonOk']");
        private const string _endPoint = "";

        public ConfirmationMilestonePage(IWebDriver driver) : base(driver)
        {
        }
        public Button OkButton() => new Button(driver, _okButtonBy);

        public override string GetEndpoint()
        {
            return _endPoint;
        }

        protected override bool EvaluateLoadedStatus()
        {
            try
            {
                return OkButton().Displayed;
            }
            catch (Exception ex)
            {
                logger.Error("'OK Button' on the 'Conformation Milestone Page' is not  displayed! " + ex);
                return false;
            }
        }
    }
}
