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
                _logger.Info(project);
                projectList.Add(project);
            }
            return projectList;
        }
    }
}
