using System.Text.Json.Serialization;

namespace TestRail.Models
{
    public class ProjectListResponse
    {
        [JsonPropertyName("projects")] public List<ProjectModel>? Projects { get; set; }
    }
}
