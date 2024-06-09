using Allure.NUnit.Attributes;
using TestRail.BaseEntities;
using TestRail.Models;


namespace TestRail.Tests.API
{
    [Category("ProjectCleaner")]
    public class ProjectCleaner : BaseApiTest
    {
        [Test]
        [NonParallelizable] //Runs after all tests
        [AllureDescription("Used to delete all my projects")]
        public void DeleteAllMyProjects()
        {
            string projectName = "EAntonova";

            List<ProjectModel> projects = projectApiStep.GetProjectsListByPartialProjectName(projectName);

            foreach (ProjectModel project in projects)
            {
                projectApiStep.DeleteProject(project);
            }
        }
    }
}
