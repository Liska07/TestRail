using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using TestRail.BaseEntities;
using TestRail.Models;
using TestRail.Utils;

namespace TestRail.Tests.UI
{
    [Category("ProjectTests")]
    [Category("MilestoneTests")]
    [AllureFeature("MilestoneTests")]
    public class MilestoneTests : BaseTest
    {
        private ProjectModel _addedProject;
        private int _projectId;

        [SetUp]
        public void SetUp()
        {
            userStep.SuccessfulLogin();
            _addedProject = projectApiStep.AddProjectAndReturnIt(
                new ProjectModel(NameGenerator.CreateProjectName()));
            _projectId = _addedProject.GetId();
        }

        [Test]
        [Category("SmokeTests")]
        [AllureDescription("Verifying for adding a milestone with model data")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureStory("Add a milestone")]
        public void AddMilestone()
        {
            string expectedMessageText = "Successfully added the new milestone.";
            string milestoneName = NameGenerator.CreateMilestoneName();
            var mailstoneInfo = new MilestoneModel(milestoneName)
            {
                Description = "Test Milestone Description"
            };

            var milestoneListPage = milestoneStep.AddMilestoneWithModel(_projectId, mailstoneInfo);

            Assert.Multiple(() =>
            {
                Assert.That(milestoneListPage.GetMessageText(), Is.EqualTo(expectedMessageText));
                Assert.That(milestoneApiStep.IsMilestoneInList(_projectId, milestoneName));
            });
        }

        [Test]
        [Category("SmokeTests")]
        [AllureDescription("Verifying an added milestone has been deleted")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureStory("Delete a milestone")]
        public void DeleteAddedMilestone()
        {
            string expectedMessageText = "Successfully deleted the milestone (s).";
            string milestoneName = NameGenerator.CreateMilestoneName();
            var mailstoneInfo = new MilestoneModel(milestoneName);

            var milestoneListPage = milestoneStep.AddMilestoneWithModel(_projectId, mailstoneInfo);
            milestoneListPage = navigationStep.NavigateToMilestoneList(_projectId);
            milestoneListPage = milestoneStep.DeleteMilestoneByName(_projectId, milestoneName);

            Assert.Multiple(() =>
            {
                Assert.That(milestoneListPage.GetMessageText(), Is.EqualTo(expectedMessageText));
                Assert.That(!milestoneApiStep.IsMilestoneInList(_projectId, milestoneName));
            });
        }
    }
}
