# Coding Tracker

A console application to track your coding sessions. Built with .NET 10.

## Features

- **View All Records** - Display all coding sessions in a table
- **Insert Record** - Add a new coding session with start/end times
- **Update Record** - Modify existing sessions
- **Delete Record** - Remove sessions from the database

## Technologies

- .NET 10
- SQLite (via Microsoft.Data.Sqlite)
- Dapper (ORM)
- Spectre.Console (UI)

## How to Build and Run
 Prerequisites :-
- .NET 10 SDK installed on your machine

Steps :-
- Clone the repository
   git clone https://github.com/Ruksan12/CodeReviews.Console.CodingTracker.git 
    cd CodeReviews.Console.CodingTracker/CodingTracker.Ruksan12

- Restore dependencies
   dotnet restore

- Build the project
   dotnet build

- Run the application
   dotnet run --project CodingTracker
## Thoughts
  Setting up the SQLite database and basic CRUD operations came together quickly. Dapper made mapping query results to C# objects very straightforward, removing a lot of the boilerplate that would come with raw ADO.NET. Spectre.Console was also easy to pick up and made the terminal output look polished with minimal effort.
  The trickiest part was handling date and time input validation — making sure the user couldn't enter an end time before a start time, and gracefully re-prompting on invalid input without crashing. Getting the update flow to feel intuitive (fetching the existing record, showing it, then allowing edits) also took a few iterations to get right.

## What I learned

 - How to use Dapper as a lightweight ORM with SQLite in a .NET console app
 - Better understanding of separation of concerns — keeping database logic, models, and UI in distinct layers makes the code much easier to maintain and extend
 - How to use Spectre.Console to build readable, user-friendly console interfaces
