using OpenQA.Selenium;
using TestRail.BaseEntities;
using TestRail.Elements;

namespace TestRail.Pages.ProjectPages
{
    public class ConfirmationProjectPage : BasePage
    {
        private static readonly By _titleBy = By.Id("ui-dialog-title-deleteDialog");
        private static readonly By _topMessageBy = By.XPath("//*[@id='deleteDialog']//p[contains(@class, 'top')]");
        private static readonly By _extraMessageBy = By.XPath("//*[@id='deleteDialog']//p[contains(@class, 'dialog-extra')]");
        private static readonly By _confirmMessageBy = By.XPath("//*[@id='deleteDialog']//span/strong");
        private static readonly By _isDeleteProgectCheckboxBy = By.XPath("//*[@data-testid='caseFieldsTabDeleteDialogCheckbox']//input");
        private static readonly By _okButtonBy = By.CssSelector("[data-testid='caseFieldsTabDeleteDialogButtonOk']");
        private static readonly By _cancelButtonBy = By.XPath("//*[@id='deleteDialog']//*[contains(@class, 'button-cancel')]");
        private const string _endPoint = "";

        public ConfirmationProjectPage(IWebDriver driver) : base(driver)
        {
        }
        public string GetTitleText() => new Message(driver, _titleBy).Text;
        public string GetTopMessageText() => new Message(driver, _topMessageBy).Text;
        public string GetExtraMessageText() => new Message(driver, _extraMessageBy).Text;
        public string GetConfirmMessageText() => new Message(driver, _confirmMessageBy).Text;
        public Checkbox IsDeleteProjectCheckbox() => new Checkbox(driver, _isDeleteProgectCheckboxBy);
        public Button OkButton() => new Button(driver, _okButtonBy);
        public Button CancelButton() => new Button(driver, _cancelButtonBy);

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
                logger.Error("'OK Button' on the 'Conformation Page' is not  displayed! " + ex);
                return false;
            }
        }
    }
}
