using Allure.NUnit.Attributes;
using TestRail.BaseEntities;
using TestRail.Models;


namespace TestRail.Tests.API
{
    [Category("ProjectCleaner")]
    public class ProjectCleaner : BaseApiTest
    {
        //To uncomment if there is a need to delete all projects where the project name contains a specified partial name.
        //[Test]
        [NonParallelizable] //Runs after all tests.
        [AllureDescription("Deletes all projects where the project name contains a specified partial name")]
        public void DeleteAllProjectsByPartialName()
        {
            string projectName = "EAntonova";

            List<ProjectModel> projects = projectApiStep
                .GetProjectsListByPartialProjectName(projectName);

            foreach (ProjectModel project in projects)
            {
                projectApiStep.DeleteProject(project);
            }
        }
    }
}
