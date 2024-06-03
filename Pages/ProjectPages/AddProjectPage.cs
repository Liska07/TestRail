using OpenQA.Selenium;
using TestRail.BaseEntities;
using TestRail.Elements;

namespace TestRail.Pages.ProjectPages
{
    public class AddProjectPage : BasePage
    {
        private static readonly By _nameFieldBy = By.Id("name");
        private static readonly By _announcementFieldBy = By.Name("announcement");
        private static readonly By _isShowAnnouncementCheckboxBy = By.Id("show_announcement");
        private static readonly By _projectTypeRadioButtonBy = By.Name("suite_mode");
        private static readonly By _isEnableTestCaseCheckboxBy = By.Id("case_statuses_enabled");
        private static readonly By _addProjectButtonBy = By.Id("accept");
        private static readonly By _errorMessageBy = By.CssSelector(".message.message-error:not([class*='validationError'])");
        private const string _endPoint = "/index.php?/admin/projects/add";

        public AddProjectPage(IWebDriver driver) : base(driver)
        {
        }

        public UiElement NameField() => new UiElement(driver, _nameFieldBy);
        public UiElement AnnouncementField() => new UiElement(driver, _announcementFieldBy);
        public Checkbox IsShowAnnouncementCheckbox() => new Checkbox(driver, _isShowAnnouncementCheckboxBy);
        public RadioButton ProjectTypeRadioButton() => new RadioButton(driver, _projectTypeRadioButtonBy);
        public Checkbox IsEnableTestCaseCheckbox() => new Checkbox(driver, _isEnableTestCaseCheckboxBy);
        public Button AddProjectButton() => new Button(driver, _addProjectButtonBy);
        public string GetErrorMessageText() => new Message(driver, _errorMessageBy).Text;
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
                logger.Error("'Add Project Button' on the 'Add Project Page' is not  displayed! " + ex);
                return false;
            }
        }
    }
}
