# BookStore

Single Page React Application (SPA) written in Typescript with a C# MVC RESTful API using ASP.NET Core that connects to an MS SQL Server database with Entity Framework.

A demo version of the application is available at https://juchertb.github.io/BookStore

## Single Page React Application

**Note:** The application requires Node.js to be installed.

After cloning the Github repository you need to run the following:

1. To install the npm modules referenced in package.json run: npm install
2. Starting the application in development mode: npm run dev

At this point the Bookstore Single Page Application should load in the browser.

## RESTFul API

To create and update the data model in the database, after compiling the project, run the following from the Terminal in VS Code:

1. dotnet ef migrations add init
2. dotnet ef database update

Swagger UI is used for API testing.

<img src="bookstore-landing.png" width=100% height=100% style="margin-top:10px;margin-bottom:10px">
<img src="bookstore-books.png" width=100% height=100% style="margin-top:10px;margin-bottom:10px">
<img src="bookstore-cart.png" width=100% height=100% style="margin-top:10px;margin-bottom:10px">
<img src="bookstore-order.png" width=100% height=100% style="margin-top:10px;margin-bottom:10px">

## Publishing the Web API to an IIS web site

[Required for IIS] Download and install ASP.NET Core 8.0 Runtime (v8.0.7) - Windows Hosting Bundle Installer
from https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-8.0.7-windows-hosting-bundle-installer

I was getting an error of type "HTTP Error 500.19 - Internal Server Error" when trying to access the route http://localhost/api/book.
https://stackoverflow.com/questions/67187661/http-500-19-error-for-dot-net-5-0-webapi-application
Likely need to install .NET core hosting bundle: https://learn.microsoft.com/en-us/aspnet/core/tutorials/publish-to-iis?view=aspnetcore-5.0&tabs=visual-studio

## Build release Web API package

1. dotnet build --configuration Release
2. Run VS Code as administrator to publish api: dotnet publish -c Release -o c:\inetpub\wwwroot\restapi

## Setup IIS web site

1. Create a new application pool "NETTESTAPI" and set the identity to "[machine name]\[user id]]".
   Otherwise I would get a cannot login error from SQLExpress in the application event log.
2. Right click sites and select "Add Website"
3. Set port to whatever port is setup for the JWT in the appsetting.json (5246). I think it was working with other ports too.
4. Create new application pool and use it for the new web site.
5. Point to the folder c:\prj\bookstore\api\publish

## Looks like SQLExpress is required

I was getting this error in the application event log when trying to connect to MSSQLLocalDB:
Microsoft.Data.SqlClient.SqlException (0x80131904): A network-related or instance-specific error occurred while establishing a connection to SQL Server.
The server was not found or was not accessible. Verify that the instance name is correct and that SQL Server is configured to allow remote connections.
(provider: SQL Network Interfaces, error: 50 - Local Database Runtime error occurred. Error occurred during LocalDB instance startup: SQL Server process
failed to start.)

I had to install SQL Server Express. This one was then showing as a Windows service. I attached the Bookstore DB to the SQL Express and changed the
connection string to: "DefaultConnection": "Server=[Machine name]\\SQLEXPRESS;Database=Bookstore;Trusted_Connection=True;TrustServerCertificate=true".
Then the Web API was then able to connect to the Bookstore database and retrieve the data.

Unfortunately there is no way to change the default paths for the DB files in MSSQLLocalDB or SQLExpress (under database properties\database settings).
So entity framework will create the DB under the default SQLExpress path of "C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\".
I then have to detach the database, copy the files to "C:\Prj\SQL Server Databases", then re-attach the database.

Note: For database files created with MSSQLLocalDB in the default location of "C:\Users\[user id]", SQL Express wasn't seeing the files, I had to move them
and I had to give full control to the mdf and ldf files to the admin user.
https://stackoverflow.com/questions/13778034/how-do-i-change-database-default-locations-for-localdb-in-sql-server-managemen

https://www.youtube.com/watch?v=kMmZ9SbPBQA
publishing the web api from vs code: dotnet publish -c Release -o c:\inetpub\wwwroot\restapi

https://blog.medhat.ca/2020/08/deploying-aspnet-core-application-that.html
