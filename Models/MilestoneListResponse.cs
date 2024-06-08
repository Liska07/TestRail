using System.Text.Json.Serialization;

namespace TestRail.Models
{
    public class MilestoneListResponse
    {
        [JsonPropertyName("milestones")] public List<MilestoneModel> Milestones { get; set; }
    }
}
