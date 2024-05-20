using Npgsql;

namespace FleetMSLogic.Repository
{
    class DatabaseConnection
    {
        private readonly string ConnectionString = "Host=localhost;Username=postgres;Password=12345;Database=asmajehad_fms";
        public readonly NpgsqlConnection Connection;

        public DatabaseConnection()
        {
            Connection = new NpgsqlConnection(ConnectionString);
        }

        /// <summary>
        /// Opens a connection to the database.
        /// </summary>
        public void OpenConnection()
        {
            try
            {
                Connection.Open();
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Failed to connect to the database: {ex.Message}");
            }
        }

        /// <summary>
        /// Closes the connection to the database.
        /// </summary>
        public void CloseConnection()
        {
            try
            {
                Connection.Close();
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Failed to close to the database: {ex.Message}");
            }
        }
    }
}
