using Allure.NUnit.Attributes;
using Allure.NUnit;
using Allure.Net.Commons;
using NLog;
using RestSharp;
using TestRail.Services.API;
using TestRail.Steps.API;
using TestRail.Utils;

namespace TestRail.BaseEntities
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [AllureNUnit]
    [AllureOwner("EAntonova")]
    [AllureEpic("TestRail")]
    public class BaseApiTest
    {
        protected RestClient client;
        protected ApiService apiService;
        protected ProjectApiStep projectApiStep;
        protected MilestoneApiStep milestoneApiStep;
        protected readonly Logger logger = LogManager.GetCurrentClassLogger();

        [OneTimeSetUp]
        public static void OneTimeSetUp()
        {
            AllureLifecycle.Instance.CleanupResultDirectory();
        }

        [SetUp]
        [AllureBefore("Set up API client")]
        public void Setup()
        {
            apiService = new ApiService();
            client = apiService.SetUpClientWithOptions(
                Configurator.GetBaseURL(),
                EnvironmentHelper.GetEnvironmentVariableOrThrow("TESTRAIL_USERNAME"),
                EnvironmentHelper.GetEnvironmentVariableOrThrow("TESTRAIL_PASSWORD"));

            projectApiStep = new ProjectApiStep(client, apiService);
            milestoneApiStep = new MilestoneApiStep(client, apiService);
        }

        [TearDown]
        [AllureAfter("API client dispose")]
        public void TearDown()
        {
            client.Dispose();
        }
    }
}
