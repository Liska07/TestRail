﻿using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using TestRail.BaseEntities;
using TestRail.Models;
using TestRail.Utils;

namespace TestRail.Tests.UI
{
    [Category("ProjectTests")]
    [AllureFeature("ProjectTests")]
    public class ProjectTests : BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            loginStep.SuccessfulLogin();
        }

        [Test]
        [Category("SmokeTests")]
        [AllureDescription("Verifying for adding a project with model data")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureStory("Add a project")]
        public void AddProject()
        {
            string projectName = NameGenerator.CreateProjectName();
            ProjectModel projectInfo = new ProjectModel(projectName)
            {
                Announcement = "Test Project Announcement",
                IsShowAnnouncement = true,
                ProjectTypeByValue = 1,
                IsEnableTestCase = true,
            };

            projectStep.AddProjectWithModel(projectInfo);
            navigationStep.NavigateToProjectList();
            ProjectModel addedProject = projectApiStep.GetProjectByItsName(projectName);

            Assert.Multiple(() =>
            {
                Assert.That(projectStep.IsProjectInList(projectName));
                Assert.That(projectApiStep.IsProjectInList(projectName));
                Assert.That(addedProject.IsEqual(projectInfo));
            });
        }

        [Test]
        [Category("SmokeTests")]
        [AllureDescription("Verifying the error message when trying to create a project with an empty name")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureStory("Add a project")]
        public void CheckMessageAddingProjectWithEmptyName()
        {
            string expectedErrorMessageText = "Field Name is a required field.";

            dashboardPage.AddProjectButton().Click();
            addProjectPage.AddProjectButton().Click();

            Assert.That(addProjectPage.GetErrorMessageText(), Is.EqualTo(expectedErrorMessageText));
        }

        [Test]
        [Category("SmokeTests")]
        [AllureDescription("Verifying an added project has been deleted")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureStory("Delete a project")]
        public void DeleteAddedProject()
        {
            string projectName = NameGenerator.CreateProjectName();
            string expectedMessageText = "Successfully deleted the project.";

            projectStep.AddProjectWithModel(new ProjectModel(projectName));
            navigationStep.NavigateToProjectList();
            projectStep.DeleteProjectByName(projectName);

            Assert.Multiple(() =>
            {
                Assert.That(projectListPage.GetMessageText(), Is.EqualTo(expectedMessageText));
                Assert.That(!projectApiStep.IsProjectInList(projectName));
            });
        }

        [Test]
        [AllureDescription("Verifying the message text in the project deletion confirmation window")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureStory("Delete a project")]
        public void CheckMessageInProjectDelConformationWindow()
        {
            string projectName = NameGenerator.CreateProjectName();
            string expectedConformationTitleText = "Confirmation";
            string expectedTopMessageText = $"Really delete project {projectName}? This also deletes all test cases and results and everything else that is part of this project. This cannot be undone.";
            string expectedExtraMessageText = "Deleting a project is a high impact and irrevocable action. " +
                "You can alternatively also just set the project to completed or hide it from the dashboard via project permissions instead.";
            string expectedConfirmMessageText = "Yes, delete this project (cannot be undone)";

            projectStep.AddProjectWithModel(new ProjectModel(projectName));
            navigationStep.NavigateToProjectList();
            projectStep.ClickDeleteButtonByProjectName(projectName);

            Assert.Multiple(() =>
            {
                Assert.That(confirmationProjectPage.GetTitleText(), Is.EqualTo(expectedConformationTitleText));
                Assert.That(confirmationProjectPage.GetTopMessageText(), Is.EqualTo(expectedTopMessageText));
                Assert.That(confirmationProjectPage.GetExtraMessageText(), Is.EqualTo(expectedExtraMessageText));
                Assert.That(confirmationProjectPage.GetConfirmMessageText(), Is.EqualTo(expectedConfirmMessageText));
            });
        }

        [Test]
        [Category("SmokeTests")]
        [AllureDescription("Verifying if the project is in the list if you refused to delete it")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureStory("Delete a project")]
        public void CheckExistenceProjectAfterRefusalDeleteIt()
        {
            string projectName = NameGenerator.CreateProjectName();

            projectStep.AddProjectWithModel(new ProjectModel(projectName));
            navigationStep.NavigateToProjectList();
            projectStep.ClickDeleteButtonByProjectName(projectName);
            confirmationProjectPage.CancelButton().Click();


            Assert.Multiple(() =>
            {
                Assert.That(projectStep.IsProjectInList(projectName));
                Assert.That(projectApiStep.IsProjectInList(projectName));
            });
        }
    }
}
