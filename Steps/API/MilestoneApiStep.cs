using Allure.NUnit.Attributes;
using RestSharp;
using System.Text.Json;
using TestRail.BaseEntities;
using TestRail.Models;
using TestRail.Services.API;

namespace TestRail.Steps.API
{
    public class MilestoneApiStep : BaseApiStep
    {
        public MilestoneApiStep(RestClient client, ApiService apiServices) : base(client, apiServices)
        {
        }

        private List<MilestoneModel> GetMilestoneList(int project_id)
        {
            string endPoint = $"index.php?/api/v2/get_milestones/{project_id}";

            var response = apiService.CreateGetRequest(client, endPoint);
            var responseContent = apiService.GetContent(response);

            var milestonesResponse = JsonSerializer.Deserialize<MilestoneListResponse>(responseContent);

            if (milestonesResponse == null)
            {
                string errorMessage = "Deserialized milestone list response is null.";
                logger.Error(errorMessage);
                throw new InvalidOperationException(errorMessage);
            }

            if (milestonesResponse.Milestones == null)
            {
                string errorMessage = "Milestones in milestonesResponse is null.";
                logger.Error(errorMessage);
                throw new InvalidOperationException(errorMessage);
            }

            return milestonesResponse.Milestones.ToList();
        }

        public bool IsMilestoneInList(int project_id, string name)
        {
            List<MilestoneModel> milestones = GetMilestoneList(project_id);
            return milestones.Any(milestone => milestone.Name == name);
        }

        public bool IsMilestoneInList(int project_id, int milestone_id)
        {
            List<MilestoneModel> milestones = GetMilestoneList(project_id);
            return milestones.Any(milestone => milestone.Id == milestone_id);
        }

        [AllureStep("Add a milestone with a given milestone model by API and return response")]
        public RestResponse AddMilestone(MilestoneModel milestoneToCreate, int project_id)
        {
            string endPoint = $"index.php?/api/v2/add_milestone/{project_id}";

            var response = apiService.CreatePostRequest(client, endPoint, JsonSerializer.Serialize(milestoneToCreate));
            logger.Info($"Added a milestone: {response.Content}. StatusCode." + response.StatusCode);

            return response;
        }

        public MilestoneModel GetMilestoneModelFromResponse(RestResponse response)
        {
            var milestoneModel = JsonSerializer.Deserialize<MilestoneModel>(apiService.GetContent(response));

            if (milestoneModel == null)
            {
                string errorMessage = "Deserialized milestone model is null.";
                logger.Error(errorMessage);
                throw new InvalidOperationException(errorMessage);
            }

            return milestoneModel;
        }
    }
}
