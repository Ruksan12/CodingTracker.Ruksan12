using CodingTracker.Ruksan12;
using Spectre.Console;

namespace CodingTracker
{
    internal class UserInput
    {
        private readonly CodingController _codingController;
        private readonly Validation _validation;

        public UserInput(CodingController codingController)
        {
            _codingController = codingController;
            _validation = new Validation();
        }

        public void MainMenu()
        {
            bool closeApp = false;
            while (!closeApp)
            {
                AnsiConsole.Clear();
                var rule = new Rule("[blue]Coding Tracker[/]");
                AnsiConsole.Write(rule);

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("What would you like to do?")
                    .AddChoices(new[] {
                        "View All Records",
                        "Insert Record",
                        "Delete Record",
                        "Update Record",
                        "Close Application"
                    }));

                switch (choice)
                {
                    case "View All Records":
                        ViewAllRecords();
                        break;
                    case "Insert Record":
                        InsertRecord();
                        break;
                    case "Delete Record":
                        DeleteRecord();
                        break;
                    case "Update Record":
                        UpdateRecord();
                        break;
                    case "Close Application":
                        closeApp = true;
                        break;
                }
            }
        }

        private void ViewAllRecords()
        {
            var sessions = _codingController.GetAllSessions();
            var table = new Table();
            table.AddColumn("Id");
            table.AddColumn("Start Time");
            table.AddColumn("End Time");
            table.AddColumn("Duration");

            foreach (var session in sessions)
            {
                table.AddRow(session.Id.ToString(), session.StartTime.ToString("dd-MM-yyyy HH:mm"), session.EndTime.ToString("dd-MM-yyyy HH:mm"), session.Duration.ToString());
            }

            AnsiConsole.Write(table);
            AnsiConsole.MarkupLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void InsertRecord()
        {
            var startTime = GetDateInput("Please insert the date and time of start: (Format: dd-MM-yyyy HH:mm). Type 0 to return to main menu");
            if (startTime == "0") return;

            var endTime = GetDateInput("Please insert the date and time of end: (Format: dd-MM-yyyy HH:mm). Type 0 to return to main menu", startTime);
            if (endTime == "0") return;

            // Calculate duration
            DateTime start = DateTime.ParseExact(startTime, "dd-MM-yyyy HH:mm", null);
            DateTime end = DateTime.ParseExact(endTime, "dd-MM-yyyy HH:mm", null);
            TimeSpan duration = end - start;

            var session = new CodingSession
            {
                StartTime = start,
                EndTime = end,
                Duration = duration.ToString()
            };

            _codingController.InsertSession(session);
            AnsiConsole.MarkupLine("[green]Record inserted successfully![/]");
            Thread.Sleep(2000);
        }

        private void DeleteRecord()
        {
            ViewAllRecords();
            var idInput = AnsiConsole.Ask<string>("Please type the Id of the record you want to delete or type 0 to return to main menu");

            if (idInput == "0") return;

            if (int.TryParse(idInput, out int id))
            {
                _codingController.DeleteSession(id);
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Invalid Id[/]");
                Thread.Sleep(2000);
            }
        }

        private void UpdateRecord()
        {
            ViewAllRecords();
            var idInput = AnsiConsole.Ask<string>("Please type the Id of the record you want to update or type 0 to return to main menu");

            if (idInput == "0") return;

            if (int.TryParse(idInput, out int id))
            {
                var session = _codingController.GetSessionById(id);
                if (session == null)
                {
                    AnsiConsole.MarkupLine("[red]Record with that Id doesn't exist[/]");
                    Thread.Sleep(2000);
                    return;
                }

                var startTime = GetDateInput("Please insert the new date and time of start: (Format: dd-MM-yyyy HH:mm). Type 0 to return to main menu");
                if (startTime == "0") return;

                var endTime = GetDateInput("Please insert the new date and time of end: (Format: dd-MM-yyyy HH:mm). Type 0 to return to main menu", startTime);
                if (endTime == "0") return;

                DateTime start = DateTime.ParseExact(startTime, "dd-MM-yyyy HH:mm", null);
                DateTime end = DateTime.ParseExact(endTime, "dd-MM-yyyy HH:mm", null);
                TimeSpan duration = end - start;

                session.StartTime = start;
                session.EndTime = end;
                session.Duration = duration.ToString();

                _codingController.UpdateSession(session);
                AnsiConsole.MarkupLine("[green]Record updated successfully![/]");
                Thread.Sleep(2000);

            }
            else
            {
                AnsiConsole.MarkupLine("[red]Invalid Id[/]");
                Thread.Sleep(2000);
            }
        }

        private string GetDateInput(string message, string startDate = null)
        {
            while (true)
            {
                var dateInput = AnsiConsole.Ask<string>(message);

                if (dateInput == "0") return "0";

                if (!_validation.ValidateDate(dateInput, "dd-MM-yyyy HH:mm"))
                {
                    AnsiConsole.MarkupLine("[red]Invalid date format. Use dd-MM-yyyy HH:mm[/]");
                    continue;
                }

                if (startDate != null)
                {
                    if (!_validation.ValidateDateRange(startDate, dateInput, "dd-MM-yyyy HH:mm"))
                    {
                        AnsiConsole.MarkupLine("[red]End time must be after start time[/]");
                        continue;
                    }
                }

                return dateInput;
            }
        }
    }
}
