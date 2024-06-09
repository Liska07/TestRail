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
            var connectionString = $"Host={Configurator.GetServer()};" +
                                   $"Port={Configurator.GetPort()};" +
                                   $"Database={Configurator.GetName()};" +
                                   $"User Id={Configurator.GetUserName()};" +
                                   $"Password={Configurator.GetPassword()};";

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
