using OpenQA.Selenium;
using TestRail.BaseEntities;
using TestRail.Elements;

namespace TestRail.Pages.MilestonePages
{
    public class AddMilestonePage : BasePage
    {
        private static readonly By _nameFieldBy = By.Id("name");
        private static readonly By _descriptionFieldBy = By.Id("description_display");
        private static readonly By _attachButtonBy = By.Id("entityAttachmentListEmptyIcon");
        private static readonly By _addMilestoneButtonBy = By.Id("accept");

        public AddMilestonePage(IWebDriver driver, int projectId) : base(driver, projectId)
        {
        }
        //public AddMilestonePage(IWebDriver driver, int projectId, bool openPageByUrl = false) : base(driver, projectId, openPageByUrl)
        //{
        //}
        public Field NameField() => new Field(driver, _nameFieldBy);
        public Field DescriptionField() => new Field(driver, _descriptionFieldBy);
        public Button AttachButton() => new Button(driver, _attachButtonBy);
        public Button AddMilestoneButton() => new Button(driver, _addMilestoneButtonBy);

        public override string GetEndpoint()
        {
            return $"/index.php?/milestones/add/{projectId}";
        }

        protected override bool EvaluateLoadedStatus()
        {
            try
            {
                return NameField().Enabled;
            }
            catch (Exception ex)
            {
                logger.Error("'Name Field' on the 'Add Milestone Page' is not enabled! " + ex);
                return false;
            }
        }
    }
}
