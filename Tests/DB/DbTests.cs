using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using System.Net;
using TestRail.BaseEntities;
using TestRail.Connector;
using TestRail.Models;
using TestRail.Services.DB;
using TestRail.Utils;

namespace TestRail.Tests.DB
{
    [Category("DatabaseTests")]
    public class DbTests : BaseApiTest
    {
        private DbConnector _connector;
        private DbService _dbService;

        [SetUp]
        [AllureBefore("Setup connection")]
        public void ConnectToDatabase()
        {
            _connector = new DbConnector();
            _dbService = new DbService(_connector.Connection);
        }

        [Test]
        [AllureDescription("Verifying for adding a project by API with data from database")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureStory("Add a project")]
        [AllureFeature("ProjectTests")]
        public void AddProjectDB()
        {
            List<ProjectModel> projectsFromDB = _dbService.GetAllProjects();

            foreach (ProjectModel projectInfo in projectsFromDB)
            {
                var response = projectApiStep.AddProject(projectInfo);
                var actualProject = projectApiStep.GetProjectModelFromResponse(response);

                Assert.Multiple(() =>
                {
                    Assert.That(response.StatusCode == HttpStatusCode.OK);
                    Assert.That(actualProject.IsEqual(projectInfo));
                    Assert.That(projectApiStep.IsProjectInList(actualProject.GetId()));
                });
            }
        }

        [Test]
        [AllureDescription("Verifying for adding a milestone by API with data from database")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureStory("Add a milestone")]
        [AllureFeature("MilestoneTests")]
        public void AddMilestoneDB()
        {
            string projectName = NameGenerator.CreateProjectName();
            ProjectModel addedProject = projectApiStep.AddProjectAndReturnIt(new ProjectModel(projectName));

            List<MilestoneModel> milestonesFromDB = _dbService.GetAllMilestones();

            foreach (MilestoneModel milestoneInfo in milestonesFromDB)
            {
                var response = milestoneApiStep.AddMilestone(milestoneInfo, addedProject.GetId());
                var actualMilestone = milestoneApiStep.GetMilestoneModelFromResponse(response);

                Assert.Multiple(() =>
                {
                    Assert.That(response.StatusCode == HttpStatusCode.OK);
                    Assert.That(actualMilestone.IsEqual(milestoneInfo));
                    Assert.That(milestoneApiStep.IsMilestoneInList(addedProject.GetId(), actualMilestone.GetId()));
                });
            }
        }

        [TearDown]
        [AllureAfter("Close connection")]
        public void DisconnectFromDatabase()
        {
            if (_connector != null)
            {
                _connector.CloseConnection();
            }
        }
    }
}
