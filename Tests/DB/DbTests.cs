using System.Net;
using TestRail.BaseEntities;
using TestRail.Connector;
using TestRail.Models;
using TestRail.Services.DB;
using TestRail.Steps.UI;

namespace TestRail.Tests.DB
{
    public class DbTests : BaseApiTest
    {
        private DbConnector _connector;
        private DbService _projectService;

        [SetUp]
        public void ConnectToDatabase()
        {
            _connector = new DbConnector();
            _projectService = new DbService(_connector.Connection);
        }

        [Test]
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
        public void DisconnectFromDatabase()
        {
            if (_connector != null)
            {
                _connector.CloseConnection();
            }
        }
    }
}
