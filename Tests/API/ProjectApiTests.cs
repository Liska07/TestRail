﻿using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using System.Net;
using System.Text.Json;
using TestRail.BaseEntities;
using TestRail.Models;
using TestRail.Utils;

namespace TestRail.Tests.API
{
    [Category("ProjectTests")]
    [AllureFeature("Project Tests")]
    public class ProjectApiTests : BaseApiTest
    {
        [Test]
        [Category("SmokeTests")]
        [AllureDescription("Verifying the addition of a project with test data from the .json file")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureStory("Add a project")]
        [TestCaseSource(nameof(ProjectPositiveTestCases))]
        public void AddProjectAPI(ProjectModel projectInfo)
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

        private static List<ProjectModel> ProjectPositiveTestCases()
        {
            using (StreamReader r = new StreamReader("Resources/ProjectPositiveTestCases.json"))
            {
                string json = r.ReadToEnd();
                var projects = JsonSerializer.Deserialize<List<ProjectModel>>(json);
                return projects ?? new List<ProjectModel>();
            }
        }

        [Test]
        [Category("SmokeTests")]
        [AllureDescription("Verifying a response with invalid data")]
        [AllureSeverity(SeverityLevel.critical)]
        [TestCaseSource(nameof(ProjectNegativeTestCases))]
        [AllureStory("Add a project")]
        public void AddProjectNegativeAPI(Dictionary<string, object> project, string expectedResponseContent)
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
                    {"name", NameGenerator.CreateProjectName()},
                    {"announcement", "test announcement" },
                    {"show_announcement", true },
                    {"suite_mode", 4 }
                },
                "{\"error\":\"Field :suite_mode is not a supported enum value (\\\"4\\\").\"}"
             },
        };

        [Test]
        [Category("SmokeTests")]
        [AllureDescription("Verifying an added project has been deleted")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureStory("Delete a project")]
        public void DeleteAddedProjectAPI()
        {
            ProjectModel addedProject = projectApiStep.AddProjectAndReturnIt(new ProjectModel(NameGenerator.CreateProjectName()));

            var response = projectApiStep.DeleteProject(addedProject);

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode == HttpStatusCode.OK);
                Assert.That(!projectApiStep.IsProjectInList(addedProject.GetId()));
            });
        }

        [Test]
        [Category("SmokeTests")]
        [AllureDescription("Verifying an added project has been updated")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureStory("Update a project")]
        public void UpdateAddedProjectAPI()
        {
            ProjectModel baseProjectInfo = new ProjectModel("EAntonova Base Project Name")
            {
                Announcement = "Base Project Announcement",
                IsShowAnnouncement = false,
                ProjectTypeByValue = 1,
                IsEnableTestCase = false,
            };

            ProjectModel baseProject = projectApiStep.AddProjectAndReturnIt(baseProjectInfo);

            ProjectModel updatedProjectInfo = new ProjectModel("EAntonova Updated Project Name")
            {
                Announcement = "Updated Project Announcement",
                IsShowAnnouncement = true,
                ProjectTypeByValue = 1,
                IsEnableTestCase = true,
            };

            var response = projectApiStep.UpdateProject(baseProject, updatedProjectInfo);

            ProjectModel updatedProject = projectApiStep.GetProjectModelFromResponse(response);

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode == HttpStatusCode.OK);
                Assert.That(updatedProject.IsEqual(updatedProjectInfo));
                Assert.That(baseProject.GetId(), Is.EqualTo(updatedProject.GetId()));
                Assert.That(projectApiStep.IsProjectInList(updatedProject.GetId()));
            });
        }
    }
}
