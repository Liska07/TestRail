using OpenQA.Selenium;
using TestRail.BaseEntities;
using TestRail.Elements;

namespace TestRail.Pages.MilestonePages
{
    public class MilestoneListPage : BasePage
    {
        private static readonly By _messageBy = By.CssSelector("[data-testid='messageSuccessDivBox']");
        private static readonly By _addMilestoneButtonBy = By.Id("navigation-milestones-add");

        public MilestoneListPage(IWebDriver driver, int projectId, bool openPageByUrl = false) : base(driver, projectId, openPageByUrl)
        {
        }

        public string GetMessageText() => new Message(driver, _messageBy).Text;
        public Button AddMilestoneButton() => new Button(driver, _addMilestoneButtonBy);
        public Button GetDeleteButtonByMilestoneName(string milestoneName)
        {
            return new Button(driver, By.XPath($"//a[contains(text(),'{milestoneName}')]/ancestor::div[contains(@class, 'row')]//div[@class='icon-small-delete ']"));
        }

        public override string GetEndpoint()
        {
            return $"/index.php?/milestones/overview/{projectId}";
        }

        protected override bool EvaluateLoadedStatus()
        {
            try
            {
                return AddMilestoneButton().Displayed;
            }
            catch (Exception ex)
            {
                logger.Error("'Add Milestone Button' on the 'Project List Page' is not  displayed! " + ex);
                return false;
            }
        }
    }
}
