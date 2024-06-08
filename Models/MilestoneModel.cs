using NLog;
using System.Text.Json.Serialization;

namespace TestRail.Models
{
    public class MilestoneModel
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();
        [JsonPropertyName("id")] public int? Id { get; set; }
        [JsonPropertyName("name")] public string Name { get; }
        [JsonPropertyName("description")] public string? Description { get; set; }

        public MilestoneModel(string name)
        {
            if (string.IsNullOrEmpty(name))
            {

                string errorMessage = "The name of the milestone can't be empty.";
                _logger.Error(errorMessage);
                throw new ArgumentNullException(errorMessage);
            }
            else
            {
                Name = name;
            }
        }
    }
}
