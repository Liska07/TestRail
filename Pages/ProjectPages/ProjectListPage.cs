using OpenQA.Selenium;
using System.Collections.ObjectModel;
using TestRail.BaseEntities;
using TestRail.Elements;

namespace TestRail.Pages.ProjectPages
{
    public class ProjectListPage : BasePage
    {
        private static readonly By _messageBy = By.CssSelector("[data-testid='messageSuccessDivBox']");
        private static readonly By _addProgectButtonBy = By.XPath("//a[contains(text(), 'Add Project')]");
        private const string _endPoint = "/index.php?/admin/projects/overview";
        public ProjectListPage(IWebDriver driver, bool openPageByUrl = false) : base(driver, openPageByUrl)
        {
        }
        public string GetMessageText() => new Message(driver, _messageBy).Text;
        public Button AddProjectButton() => new Button(driver, _addProgectButtonBy);

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
                logger.Error("'Add Project Button' on the 'Project List Page' is not  displayed! " + ex);
                return false;
            }
        }

        public ReadOnlyCollection<IWebElement> GetProgectListByPartialName(string projectName)
        {
            var projectList = driver.FindElements(By.XPath($"//a[contains(text(),'{projectName}')]"));
            return projectList;
        }

        public Button GetDeleteButtonByProjectName(string projectName)
        {
            return new Button(driver, By.XPath($"//a[contains(text(),'{projectName}')]/ancestor::tr//div[@data-testid='projectDeleteButton']"));
        }


    }
}
