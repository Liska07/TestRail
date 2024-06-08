using Allure.NUnit.Attributes;
using TestRail.BaseEntities;


namespace TestRail.Tests.API
{
    [Category("ProjectCleaner")]
    public class ProjectCleaner : BaseTest
    {
        [Test]
        [NonParallelizable] //Runs after all tests
        [AllureDescription("Used to delete all my projects")]
        public void DeleteAllMyProjects()
        {
            string projectName = "EAntonova";
            userStep.SuccessfulLogin();

            int quantityMyProjects = navigationStep.NavigateToProjectList().GetProgectListByPartialName(projectName).Count();
            logger.Info($"Need to delete {quantityMyProjects} my projects");

            for (int i = 0; i < quantityMyProjects; i++)
            {
                projectStep.DeleteProjectByName(projectName);
            }
        }
    }
}
