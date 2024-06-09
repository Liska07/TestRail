using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using System.Net;
using TestRail.BaseEntities;
using TestRail.Connector;
using TestRail.Models;
using TestRail.Services.DB;

namespace TestRail.Tests.DB
{
    [Category("DatabaseTests")]
    [AllureFeature("ProjectTests")]
    public class DbTests : BaseApiTest
    {
        private DbConnector _connector;
        private DbService _projectService;

        [SetUp]
        [AllureBefore("Setup connection")]
        public void ConnectToDatabase()
        {
            _connector = new DbConnector();
            _projectService = new DbService(_connector.Connection);
        }

        [Test]
        [AllureDescription("Verifying for adding a project by API with data from database")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureStory("Add a project")]
        public void AddProjectDB()
        {
            List<ProjectModel> projectsFromDB = _projectService.GetAllProjects();

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
