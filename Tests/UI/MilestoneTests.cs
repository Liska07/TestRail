using TestRail.BaseEntities;
using TestRail.Models;

namespace TestRail.Tests.UI
{
    public class MilestoneTests : BaseTest
    {
        private ProjectModel _addedProject;
        private int _projectId;

        [SetUp]
        public void SetUp()
        {
            userStep.SuccessfulLogin();
            _addedProject = projectApiStep.AddProjectAndReturnIt(new ProjectModel($"EAntonova {Guid.NewGuid()}"));
            _projectId = _addedProject.GetId();
        }

        [Test]
        public void AddMilestone()
        {
            string expectedMessageText = "Successfully added the new milestone.";
            string milestoneName = "Release " + Guid.NewGuid();
            var mailstoneInfo = new MilestoneModel(milestoneName)
            {
                Description = "Test Milestone Description"
            };

            var milestoneListPage = milestoneStep.AddMilestoneWithModel(_projectId, mailstoneInfo);

            Assert.Multiple(() =>
            {
                Assert.That(milestoneListPage.GetMessageText(), Is.EqualTo(expectedMessageText));
                Assert.That(milestoneApiStep.IsMilestoneInListByName(_projectId, milestoneName));
            });
        }

        [Test]
        public void DeleteAddedMilestone()
        {
            string expectedMessageText = "Successfully deleted the milestone (s).";
            string milestoneName = "Release " + Guid.NewGuid();
            var mailstoneInfo = new MilestoneModel(milestoneName)
            {
                Description = "Test Milestone Description"
            };

            var milestoneListPage = milestoneStep.AddMilestoneWithModel(_projectId, mailstoneInfo);
            milestoneListPage = navigationStep.NavigateToMilestone(_projectId);
            milestoneListPage = milestoneStep.DeleteMilestoneByName(_projectId, milestoneName);

            Assert.Multiple(() =>
            {
                Assert.That(milestoneListPage.GetMessageText(), Is.EqualTo(expectedMessageText));
                Assert.That(!milestoneApiStep.IsMilestoneInListByName(_projectId, milestoneName));
            });
        }
    }
}
