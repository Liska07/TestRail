using Allure.NUnit.Attributes;
using OpenQA.Selenium;
using TestRail.BaseEntities;
using TestRail.Models;
using TestRail.Pages.ProjectPages;


namespace TestRail.Steps.UI
{
    public class ProjectStep : BaseStep
    {
        public ProjectStep(IWebDriver driver) : base(driver)
        {
        }

        [AllureStep("Add a project with a given project model")]
        public ProjectListPage AddProjectWithModel(ProjectModel projectModel)
        {
            new NavigationStep(driver).NavigateToProjectList();
            projectListPage.AddProjectButton().Click();

            addProjectPage.NameField().SendKeys(projectModel.Name);

            if (!string.IsNullOrEmpty(projectModel.Announcement))
            {
                addProjectPage.AnnouncementField().SendKeys(projectModel.Announcement);
            }

            if (projectModel.IsShowAnnouncement == true)
            {
                addProjectPage.IsShowAnnouncementCheckbox().Select();
            }

            if (projectModel.ProjectTypeByValue != null)
            {
                addProjectPage.ProjectTypeRadioButton().SelectByValue(projectModel.ProjectTypeByValue);
            }

            if (projectModel.IsEnableTestCase == true)
            {
                addProjectPage.IsEnableTestCaseCheckbox().Select();
            }
            addProjectPage.AddProjectButton().Click();
            logger.Info($"Added {projectModel.Name} project");
            return projectListPage;
        }

        public ConfirmationProjectPage ClickDeleteButtonByProjectName(string projectName)
        {
            projectListPage.GetDeleteButtonByProjectName(projectName).Click();
            return confirmationPage;
        }

        [AllureStep("Delete a project with the specified name")]
        public ProjectListPage DeleteProjectByName(string projectName)
        {
            ClickDeleteButtonByProjectName(projectName);
            confirmationPage.IsDeleteProjectCheckbox().Select();
            confirmationPage.OkButton().Click();
            logger.Info($"Deleted {projectName} project");
            return projectListPage;
        }

        public bool IsProjectInList(string projectName)
        {
            var projectList = driver.FindElements(By.XPath($"//a[text()='{projectName}']"));
            if (projectList.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
