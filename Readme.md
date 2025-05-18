# Building Enterprise Grade Web APIs in ASP.NET Core

This repository is the sample code for my conference talk "Building Enterprise Grade Web APIs in ASP.NET Core".  This app and the corresponding conference talk were originally developed as a way to explore and document how to write APIs that went beyond the simple "hello World" examples that you often see and act as a reference app for developers needing to build real-world APIs that run in production.

The sample application was originally written in .NET Core 2.1 and has now been updated to .NET 8 to keep up with changes in the framework.  The slides for the original talk and the database project are also contained in this repo.  The demonstration API is based food truck data, what what food trucks exist, where they will be when and reviews for the truck.

All of the data in this application is fictional.

## Requirements

The following is a list of required tooling if you want to download and run the examples:

* .NET 8
* SQL Server 2016 or later

You download both of these from the Microsoft Website.  For SQL Server Express 2016, Scott Hanselman has a blog post with a direct link to the download that makes it much easier to find the actual download: [downloadsqlserverexpress.com](http://www.hanselman.com/blog/DownloadSqlServerExpress.aspx).  You can also run SQL Server in a container if you do not want to isntall it locally.

Note for SQL Server 2016 the database engine and management tools (SSMS) are separate downloads.  I strongly suggest you make sure you also have the 2016 or later version of SSMS installed as well.  

## Updates

The sample application was originally written in 2017 and updated frequently through 2020 to reflect changes in .NET Core (as it was called at the time).  I've recently updated the app to .NET 8, and I plan to continue to update the app for each LTS version of .NET that is released.  

All of the core principles of the talk remain relevant today.  I plan to add a docs section to better document these in this repo.  In addition, I plan on adding additional functionaility to the app in 2025 to keep it relevant as a reference app moving forward.

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


