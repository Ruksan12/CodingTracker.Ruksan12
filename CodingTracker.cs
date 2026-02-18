using CodingTracker.Ruksan12.Controller;
using CodingTracker.Ruksan12.Data;
using CodingTracker.Ruksan12.Menu;

namespace CodingTracker.Ruksan12
{
    internal class CodingTracker
    {
        public static void Main(string[] args)
        {
            string connectionString = "Data Source=coding-tracker.db";

            DatabaseManager dbManager = new DatabaseManager(connectionString);
            dbManager.CreateDatabase();

            CodingController controller = new CodingController(connectionString);
            UserInput userInput = new UserInput(controller);
            userInput.MainMenu();

        }
    }
}
