using OpenQA.Selenium;
using TestRail.BaseEntities;
using TestRail.Elements;

namespace TestRail.Pages.ProjectPages
{
    public class ProjectPage : BasePage
    {
        private static readonly By _nameFieldBy = By.Id("name");
        private static readonly By _announcementFieldBy = By.Name("announcement");
        private static readonly By _isShowAnnouncementCheckboxBy = By.Id("show_announcement");
        private static readonly By _projectTypeRadioButtonBy = By.Name("suite_mode");
        private static readonly By _isEnableTestCaseCheckboxBy = By.Id("case_statuses_enabled");
        private static readonly By _acceptProjectButtonBy = By.Id("accept");
        private static readonly By _errorMessageBy = By.CssSelector(".message.message-error:not([class*='validationError'])");
        private const string _endPoint = "/index.php?/admin/projects/add";

        public ProjectPage(IWebDriver driver) : base(driver)
        {
        }

        public Field NameField() => new Field(driver, _nameFieldBy);
        public Field AnnouncementField() => new Field(driver, _announcementFieldBy);
        public Checkbox IsShowAnnouncementCheckbox() => new Checkbox(driver, _isShowAnnouncementCheckboxBy);
        public RadioButton ProjectTypeRadioButton() => new RadioButton(driver, _projectTypeRadioButtonBy);
        public Checkbox IsEnableTestCaseCheckbox() => new Checkbox(driver, _isEnableTestCaseCheckboxBy);
        public Button AcceptProjectButton() => new Button(driver, _acceptProjectButtonBy);
        public string GetErrorMessageText() => new Message(driver, _errorMessageBy).Text;
        public override string GetEndpoint()
        {
            return _endPoint;
        }
        protected override bool EvaluateLoadedStatus()
        {
            try
            {
                return AcceptProjectButton().Displayed;
            }
            catch (Exception ex)
            {
                logger.Error("'Add Project Button' on the 'Add Project Page' is not  displayed! " + ex);
                return false;
            }
        }
    }
}
