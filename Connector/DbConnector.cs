using NLog;
using Npgsql;
using TestRail.Utils;

namespace TestRail.Connector
{
    public class DbConnector
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();
        public NpgsqlConnection Connection { get; init; }
        public DbConnector()
        {
            var connectionString = $"Host={Configurator.ReadConfiguration().DbSettings.Db_Server};" +
                                   $"Port={Configurator.ReadConfiguration().DbSettings.Db_Port};" +
                                   $"Database={Configurator.ReadConfiguration().DbSettings.Db_Name};" +
                                   $"User Id={Configurator.ReadConfiguration().DbSettings.Db_UserName};" +
                                   $"Password={Configurator.ReadConfiguration().DbSettings.Db_Password};";

            try
            {
                Connection = new NpgsqlConnection(connectionString);
                Connection.Open();
                _logger.Info("Connected to database");
            }
            catch (Exception ex)
            {
                _logger.Error("Faild to connect to database", ex);
                throw; 
            }
        }

        public void CloseConnection() 
        { 
            Connection.Close(); 
        }
    }
}
