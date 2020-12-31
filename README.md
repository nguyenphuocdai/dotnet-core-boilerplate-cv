## Releases
v1.1 - Beta Release - Clone the Repository to get the latest codebase

### Changelog
#### v1.1
* added fluent validation
* added product images
* UI improvement
* activity log integration
* added toast notification
* Superadmins can now create users directly from Dashboard! Client / Server side Validations enabled
* Custom Error Pages
* Removed Hangfire Caching
* Complete Account Management on Public API (Register / Forgot Password / Confirm Email)
* UI Improvements for Register / Locked / Error Pages
* Active Page Razor Helper to Detect Current Active UI Page
* Added Product Category with Complete CRUD / CQRS
* Image Modal Improvement (Open Product Images & Profile Picture)
* Integrated SelectJs
* Added a sample Self Hosted API at Web Project - /api/productcategory/search?term=b



## Problem It will Solve

The Basic Idea is to save hours of development time. The users should be able to start off from the point all the technical aspects are already covered. The only thhing we need to worry is about implementing the Business Logic. I am planning this Project to have 2 realms - A Fluid UI Based ASP.NET Core 3.1 Razor Project and a WebAPI Project that provided data to public via valid JWT.

### Tech Stack
- ASP.NET Core 3.1 Razor Pages / Controllers / Web API 
- Entity Framework Core
- MSSQL (Supports other RDBMS too)
- Javascript / JQuery
- Bootstrap 4 / AdminLTE

# Getting Started with ASP.NET Core Hero - Boilerplate Template
0. Make sure you have EF CLI Tools installed. Open up Powershell and run the following command
`dotnet tool install --global dotnet-ef`
1. Clone this Repository and Extract it to a Folder.
3. Change the Connection Strings for the Application and Identity in the PublicAPI/appsettings.json and Web/appsettings.json
2. Run the following commands on Powershell in the Web Project's Directory.
- `dotnet restore`
- `dotnet ef database update -c ApplicationContext`
- `dotnet ef database update -c IdentityContext`
- `dotnet run` (OR) Run the Solution using Visual Studio 2019

PS - If the above code doesnt work for some reason, try using -c instead of -context or vice versa.
   
### Default Roles & Credentials
As soon you build and run your application, default users and roles get added to the database.

Default Roles are as follows.
- SuperAdmin
- Admin
- Moderator
- Basic

Here are the credentials for the default users.
- Email - superadmin@gmail.com  / Password - 123Pa$$word!
- Email - basic@gmail.com  / Password - 123Pa$$word!

### Project Structure
- ASP.NET Core 3.1 Razor Project with Identity
- ASP.NET Core 3.1 WebAPI Public API Project with JWT Auth
- Application Layer
- Domain Layer
- Infrastructure.Shared Layer
- Infrastructure.Persistence Layer

### Architecture
Check out a [Diagramatic Representation of the Architecture here](https://www.codewithmukesh.com/wp-content/uploads/2020/10/ASP.NET-Core-Hero-Boilerplate-template.png)

### Feature Overview
- Onion / Hexagonal Architecture
- Clean Code Practices
- CQRS with MediatR
- Cached Repository with In-Memory Caching and Redis Caching
- Generic Repository with Unit Of Work Pattern
- Complete User Management Module*
- Role Management* (Add / Edit / Delete Roles)
- Add Roles to Users
- Automapper
- Validation
- Auditable Entity (Track Changes on any Entity based on User and DateTime)
- Policy Based Permission Management*
- Mail Service
- Project Wise
- CRUD on Product Entity Implemented for Reference

#### ASP.NET Core Razor Page
- DARK Mode
- MultiLingual
- Fluid UI - Blazing Fast
- AdminLTE
- Responsive & Clean Design
- RTL Support for Arabic Scripts
- Cookie Authentication
- Default User / Roles / Claims Seeding
- Serilog Logging
- Super Quick CRUD with Razor Page / Partial Views and JQuery
- jQuery Datatable
- Bootstrap Modal

### ASP.NET Core WebAPI
- JWT Authentication
- Doesnot depend on the UI Project - Should run Individually
- CQRS Approach to communicate with Application Layer
- Response Wrappers
- Swagger UI with Bearer Auth
- API Versioning

