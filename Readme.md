# Building Enterprise Grade Web APIs in ASP.NET Core

This repository is the sample code for my conference talk "Building Enterprise Grade Web APIs in ASP.NET Core".  This app and the corresponding conference talk were originally developed as a way to explore and document how to write APIs that went beyond the simple "Hello World" examples that you often see and act as a reference app for developers needing to build real-world APIs that run in production.

The API is based food truck data.  This includes what food trucks are active, managing the schedules of food trucks, and leaving reviews of food trucks.  All of the data in this application is fictional.

## Cloning the Repository

This repository uses submodules to access my Framework projects.  This is done so you can debug through all of the Framework code as weill as the API code itself.

To clone the repository, use the command:

```bash
git clone --recurse-submodules https://github.com/DavidCBerry13/FoodTruckNationApi.git
```

## Requirements

The following is a list of required tooling if you want to download and run the examples:

* [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* [SQL Server 2019](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

In addition, you will want to have an IDE like [Visual Studio](https://visualstudio.microsoft.com/downloads/) or [VS Code](https://code.visualstudio.com) in order to review the code.

## Database Configuration

The database schema and some initial configuration data are available as a [SQL Server Database Project](https://learn.microsoft.com/en-us/sql/tools/sql-database-projects/get-started) in the [database](./database) folder of this repo.  SQL Server database projects can be opened and published to a database using Visual Studio.  This will create all of the tables and some initial data in the database.

The following connection string is in the `appsettings.json` file of the API project.

`Server=localhost\\sqlexpress;Database=FoodTruckNation;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True`

If you choose to host the database on a different server, instance, or use a different database name, you will need to update this connection string.

## Updates

The sample application was originally written in 2017 using .NET Core 2.1.  It has been updated since then and now uses .NET 8, the current LTS version of .NET.

All of the core principles of the talk remain relevant today.  I plan to add a docs section to better document these in this repo.  In addition, I plan on adding additional functionality to the app to keep it relevant as a reference app moving forward.

## Talk History

I've given this talk at the following events:

* .NET Developer Days (Warsaw, Poland) - October 2019
* Techorama Belgium - May 2019
* Chicago C# Web Develovers Meetup - August 2018
* Madison .NET Users Group - March 2018
* Chicago .NET Users Group - March 2018
* Wisconsin .NET Users Group (Milwaukee) - January 2018
* New York City Code Camp - October 2017

The talk runs about an hour and 15 minutes.  If you are interested in having me present at your User Group or Event, [reach out to me on LinkedIn](https://www.linkedin.com/in/david-berry-a488596/)
