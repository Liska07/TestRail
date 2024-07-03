using Allure.NUnit.Attributes;
using OpenQA.Selenium;
using TestRail.BaseEntities;
using TestRail.Models;


namespace TestRail.Steps.UI
{
    public class ProjectStep : BaseStep
    {
        public ProjectStep(IWebDriver driver) : base(driver)
        {
        }

        [AllureStep("Added a project")]
        public void AddProject(ProjectModel projectModel)
        {
            new NavigationStep(driver).NavigateToProjectList();
            projectListPage.AddProjectButton().Click();
            FillProjectData(projectModel);
            projectPage.AcceptProjectButton().Click();
            logger.Info($"Added '{projectModel.Name}' project");
        }

        private void FillProjectData(ProjectModel projectModel)
        {
            projectPage.NameField().SendKeys(projectModel.Name);

            if (!string.IsNullOrEmpty(projectModel.Announcement))
            {
                projectPage.AnnouncementField().SendKeys(projectModel.Announcement);
            }

            if (projectModel.IsShowAnnouncement == true)
            {
                projectPage.IsShowAnnouncementCheckbox().Select();
            }
            else
            {
                projectPage.IsShowAnnouncementCheckbox().Deselect();
            }

            if (projectModel.ProjectTypeByValue != null)
            {
                projectPage.ProjectTypeRadioButton().SelectByValue(projectModel.ProjectTypeByValue);
            }

            if (projectModel.IsEnableTestCase == true)
            {
                projectPage.IsEnableTestCaseCheckbox().Select();
            }
            else
            {
                projectPage.IsEnableTestCaseCheckbox().Deselect();
            }
        }

        public void ClickDeleteButtonByProjectName(string projectName)
        {
            projectListPage.GetDeleteButtonByProjectName(projectName).Click();
        }

        [AllureStep("Deleted a project with the specified name")]
        public void DeleteProjectByName(string projectName)
        {
            ClickDeleteButtonByProjectName(projectName);
            confirmationProjectPage.IsDeleteProjectCheckbox().Select();
            confirmationProjectPage.OkButton().Click();
            logger.Info($"Deleted '{projectName}' project");
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

        public void OpenProjectPageByName(string projectName)
        {
            projectListPage.GetProjectLinkByProjectName(projectName).Click();
        }

        [AllureStep("Updated a project with the specified name")]
        public void UpdateProject(string baseProjectName, ProjectModel updatedProjectInfo)
        {
            OpenProjectPageByName(baseProjectName);
            FillProjectData(updatedProjectInfo);
            projectPage.AcceptProjectButton().Click();
            logger.Info($"Updated '{updatedProjectInfo.Name}' project (previously '{baseProjectName}')");
        }
    }
}
