using NLog;
using Npgsql;
using TestRail.Models;

namespace TestRail.Services.DB
{
    public class DbService
    {
        private NpgsqlConnection _connection;
        private Logger _logger = LogManager.GetCurrentClassLogger();

        public DbService(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public List<ProjectModel> GetAllProjects()
        {
            var projectList = new List<ProjectModel>();

            var cmd = new NpgsqlCommand("SELECT * FROM public.\"Projects\";", _connection);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var project = new ProjectModel(reader.GetString(reader.GetOrdinal("name")))
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    Announcement = reader.IsDBNull(reader.GetOrdinal("announcement")) ? null : reader.GetString(reader.GetOrdinal("announcement")),
                    IsShowAnnouncement = reader.IsDBNull(reader.GetOrdinal("announcement")) ? false : reader.GetBoolean(reader.GetOrdinal("is_show_announcement")),
                    ProjectTypeByValue = reader.GetInt32(reader.GetOrdinal("project_type")),
                    IsEnableTestCase = reader.IsDBNull(reader.GetOrdinal("announcement")) ? false : reader.GetBoolean(reader.GetOrdinal("is_enable_test_case"))
                };

                projectList.Add(project);
                _logger.Info($"Project from DB: {project}");
            }
            return projectList;
        }

        public List<MilestoneModel> GetAllMilestones()
        {
            var milestoneList = new List<MilestoneModel>();

            var cmd = new NpgsqlCommand("SELECT * FROM public.\"Milestones\";", _connection);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var milestone = new MilestoneModel(reader.GetString(reader.GetOrdinal("name")))
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    Description = reader.IsDBNull(reader.GetOrdinal("description")) ? null : reader.GetString(reader.GetOrdinal("description"))
                };
                
                milestoneList.Add(milestone);
                _logger.Info($"Milestone from DB: {milestone}");
            }
            return milestoneList;
        }
    }
}
