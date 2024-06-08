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

            return milestonesResponse.Milestones.ToList();
        }

        public bool IsMilestoneInListByName(int project_id, string name)
        {
            List<MilestoneModel> milestones = GetMilestoneList(project_id);
            return milestones.Any(milestone => milestone.Name == name);
        }
    }
}
