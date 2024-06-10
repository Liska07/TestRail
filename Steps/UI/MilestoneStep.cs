using Allure.NUnit.Attributes;
using OpenQA.Selenium;
using TestRail.BaseEntities;
using TestRail.Models;
using TestRail.Pages.MilestonePages;

namespace TestRail.Steps.UI
{
    public class MilestoneStep : BaseStep
    {
        public MilestoneStep(IWebDriver driver) : base(driver)
        {
        }

        [AllureStep("Add a milestone with a given milestone model")]
        public MilestoneListPage AddMilestoneWithModel(int projectId, MilestoneModel milestoneModel)
        {
            var milestoneListPage = new NavigationStep(driver).NavigateToMilestone(projectId);
            milestoneListPage.AddMilestoneButton().Click();
            //var addMilestonePage = new AddMilestonePage(driver, projectId, true);
            var addMilestonePage = new AddMilestonePage(driver, projectId);
            addMilestonePage.NameField().SendKeys(milestoneModel.Name);

            if (!string.IsNullOrEmpty(milestoneModel.Description))
            {
                addMilestonePage.DescriptionField().SendKeys(milestoneModel.Description);
            }

            addMilestonePage.AddMilestoneButton().Click();
            logger.Info($"Added '{milestoneModel.Name}' milestone");
            return milestoneListPage;
        }

        public ConfirmationMilestonePage ClickDeleteButtonByMilestoneName(int projectId, string milestoneName)
        {
            var milestoneListPage = new MilestoneListPage(driver, projectId);
            milestoneListPage.GetDeleteButtonByMilestoneName(milestoneName).Click();
            return confirmationMilestonePage;
        }

        [AllureStep("Delete a milestone with the specified name")]
        public MilestoneListPage DeleteMilestoneByName(int projectId, string milestoneName)
        {
            ClickDeleteButtonByMilestoneName(projectId, milestoneName);
            confirmationMilestonePage.OkButton().Click();
            logger.Info($"Deleted {milestoneName} milestone");
            return new MilestoneListPage(driver, projectId);
        }
    }
}
