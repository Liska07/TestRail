using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using System.Net;
using System.Text.Json;
using TestRail.BaseEntities;
using TestRail.Models;

namespace TestRail.Tests.API
{
    [Category("ProjectTests")]
    public class ApiProjectTests : BaseApiTest
    {
        [Test]
        [Category("SmokeTests")]
        [AllureDescription("Verifying the addition of a project with test data from the file")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureStory("Add a project")]
        [TestCaseSource(nameof(ProjectPositiveTestCases))]
        public void AddProjectWithFileTestCaseAPI(ProjectModel expectedProject)
        {
            var response = projectApiStep.AddProject(expectedProject);
            var actualProject = projectApiStep.GetProjectModelFromResponse(response);

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode == HttpStatusCode.OK);
                Assert.That(actualProject.IsEqual(expectedProject));
                Assert.That(projectApiStep.IsProjectInList(actualProject.GetId()));
            });
        }

        private static List<ProjectModel> ProjectPositiveTestCases()
        {
            using (StreamReader r = new StreamReader("Resources/ProjectPositiveTestCases.json"))
            {
                string json = r.ReadToEnd();
                var items = JsonSerializer.Deserialize<List<ProjectModel>>(json);
                return items ?? new List<ProjectModel>();
            }
        }

        [Test]
        [AllureStory("Add a project")]
        [Category("SmokeTests")]
        [AllureDescription("Verifying a response with invalid data")]
        [AllureSeverity(SeverityLevel.critical)]
        [TestCaseSource(nameof(ProjectNegativeTestCases))]
        public void NegativeAddProjectTestAPI(Dictionary<string, object> project, string expectedResponseContent)
        {
            const string endPoint = "/index.php?/api/v2/add_project";

            var response = apiService.CreatePostRequest(client, endPoint, JsonSerializer.Serialize(project));
            string actualResponseContent = apiService.GetContent(response);

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode == HttpStatusCode.BadRequest);
                Assert.That(actualResponseContent, Is.EqualTo(expectedResponseContent));
            });
        }

        private static readonly object[] ProjectNegativeTestCases =
        {
             new object[]
             {
                new Dictionary<string, object>
                {
                    {"announcement", "test announcement" },
                    {"show_announcement", true },
                    {"suite_mode", 1 }
                },
                "{\"error\":\"Field :name is a required field.\"}"
             },
             new object[]
             {
                new Dictionary<string, object>
                {
                    {"name", $"EAntonova + {Guid.NewGuid()}" },
                    {"announcement", "test announcement" },
                    {"show_announcement", true },
                    {"suite_mode", 4 }
                },
                "{\"error\":\"Field :suite_mode is not a supported enum value (\\\"4\\\").\"}"
             },
        };
    }
}
