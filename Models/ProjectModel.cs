using NLog;
using System.Text.Json.Serialization;

namespace TestRail.Models
{
    public class ProjectModel
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        [JsonPropertyName("id")] public int? Id { get; set; }
        [JsonPropertyName("name")] public string Name { get; }
        [JsonPropertyName("announcement")] public string? Announcement { get; set; }
        [JsonPropertyName("show_announcement")] public bool IsShowAnnouncement { get; set; } = false;
        [JsonPropertyName("suite_mode")] public int? ProjectTypeByValue { get; set; }
        [JsonPropertyName("case_statuses_enabled")] public bool IsEnableTestCase { get; set; } = false;

        public ProjectModel(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                string errorMessage = "The name of the project can't be empty.";
                _logger.Error(errorMessage);
                throw new ArgumentNullException(errorMessage);
            }
            else
            {
                Name = name;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Announcement)}: {Announcement}, " +
                   $"{nameof(IsShowAnnouncement)}: {IsShowAnnouncement}, {nameof(ProjectTypeByValue)}: {ProjectTypeByValue}" +
                   $"{nameof(IsEnableTestCase)}: {IsEnableTestCase}";
        }

        public bool IsEqual(ProjectModel projectModel)
        {
            return Name == projectModel.Name && 
                   Announcement == projectModel.Announcement &&
                   IsShowAnnouncement == projectModel.IsShowAnnouncement &&
                   ProjectTypeByValue == projectModel.ProjectTypeByValue &&
                   IsEnableTestCase == projectModel.IsEnableTestCase;
        }

        public int GetId()
        {
            if (Id == null)
            {
                string errorMessage = "Project Id is null.";
                _logger.Error(errorMessage);
                throw new InvalidOperationException(errorMessage);
            }
            return Id.Value;
        }
    }
}
