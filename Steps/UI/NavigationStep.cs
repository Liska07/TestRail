using OpenQA.Selenium;
using TestRail.BaseEntities;
using TestRail.Pages.MilestonePages;
using TestRail.Pages.ProjectPages;

namespace TestRail.Steps.UI
{
    public class NavigationStep : BaseStep
    {
        public NavigationStep(IWebDriver driver) : base(driver)
        {
        }

        public ProjectListPage NavigateToProjectList()
        {
            return new ProjectListPage(driver, true);
        }

        public MilestoneListPage NavigateToMilestone(int projectId)
        {
            return new MilestoneListPage(driver, projectId, true);
        }
    }
}
