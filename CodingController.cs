using Dapper;
using Microsoft.Data.Sqlite;

namespace CodingTracker.Ruksan12
{
    internal class CodingController
    {
        private readonly string _connectionString;
        public CodingController(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<CodingSession> GetAllSessions()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM CodingSessions";
                var sessions = connection.Query<CodingSession>(query).AsList();
                return sessions;
            }
        }

        public void InsertSession(CodingSession session)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO CodingSessions (StartTime, EndTime, Duration) VALUES (@StartTime, @EndTime, @Duration)";
                connection.Execute(insertQuery, session);
            }
        }

        public void DeleteSession(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM CodingSessions WHERE Id = @Id";
                connection.Execute(deleteQuery, new { Id = id });
            }
        }

        public void UpdateSession(CodingSession session)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                string updateQuery = "UPDATE CodingSessions SET StartTime = @StartTime, EndTime = @EndTime, Duration = @Duration WHERE Id = @Id";
                connection.Execute(updateQuery, session);
            }
        }

        public CodingSession GetSessionById(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM CodingSessions WHERE Id = @Id";
                var session = connection.QuerySingleOrDefault<CodingSession>(query, new { Id = id });
                return session;
            }
        }
    }
}
