using Microsoft.Data.Sqlite;
using Dapper;

namespace CodingTracker.Ruksan12
{
    internal class DatabaseManager
    {
        private readonly string _connectionString;
        public DatabaseManager(string connectionString) 
        { 
            _connectionString = connectionString;
        }

        public void CreateDatabase()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS CodingSessions (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        StartTime TEXT NOT NULL,
                        EndTime TEXT NOT NULL,
                        Duration INTEGER NOT NULL
                    )";
                using (var command = new SqliteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
