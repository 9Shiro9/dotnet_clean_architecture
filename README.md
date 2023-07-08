# ASP.NET Core Clean Architecture Boilerplate

## Overview

## Technologies

* [ASP.NET Core 6](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core)
* [Entity Framework Core 6](https://docs.microsoft.com/en-us/ef/core/)
* [ASP.NET Core Identity with JWT](https://learn.microsoft.com/en-us/aspnet/core/security/?view=aspnetcore-6.0)
* [ASP.NET Core web API documentation with Swagger / OpenAPI](https://learn.microsoft.com/en-us/aspnet/core/security/?view=aspnetcore-6.0)
* [Localization (English/Myanmar)](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-6.0)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)
* [NUnit](https://nunit.org/), [FluentAssertions](https://fluentassertions.com/), [Moq](https://github.com/moq) & [Respawn](https://github.com/jbogard/Respawn)
* REST API Responses Consistent
* Tests Projects for All Layers
### Domain

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

### Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.

### Infrastructure

This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

### WebAPI


### Database Migrations
```
dotnet ef migrations add initmigration --context ApplicationDbContext --project .\Infrastructure\Infrastructure.csproj --startup-project .\WebAPI\WebAPI.csproj
```
```
dotnet ef database update --context ApplicationDbContext --project .\Infrastructure\Infrastructure.csproj --startup-project .\WebAPI\WebAPI.csproj
```
