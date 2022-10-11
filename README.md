# .NET API boilerplate 🚀

An awesome boilerplate for your next .NET 6.0 based API. Its only goal is to simply **kick-start your API development** and provide you with some of the best practices when building amazing and scalable REST APIs 🔥

## Features 🍭

### REST API Best Practices
We baked in all the best REST API practices in terms of structuring your API, naming conversations, HTTP methods, responses, and optimizations
	
### Concrete examples
We all like to learn by examples and that's why  the boilerplate comes with a concrete example that includes everything from folder structure, and routes to naming controllers

### Built-in versioning
Building a versioning system that scales is always hard and that's why we included a robust versioning system that is easy to use and can grow with the project</dd>

### Minimal APIs
The boilerplate uses the new .NET 6.0 Minimal APIs architecture.

### Clean Architecture
The boilerplate is loosely based on Clean Architecture patterns without being too strict. The endpoints are kept 'thin' by using the Mediator Pattern with [Mediatr](https://github.com/jbogard/MediatR).

### Entity Framework
Entity Framework Core is already included and we built two tables Users and Posts and defined everything you might need on a database and model level. You can easily expand the database by using the Entity Framework Migration system.

### .NET Core Identity
Built-in .NET Core Identity authentication using JWT Bearer tokens included in the boilerplate.

### Validation
The boilerplate uses [FluentValidation](https://github.com/FluentValidation/FluentValidation) for consistent and scalable validation.

### Easy exception handling
The boilerplate includes an example of a global exception handler which makes sure that the API always returns a user-friendly error message.

### Treblle built-in
We added an awesome NuGet package called Treblle. Out of the box, Treblle gives you: real-time API monitoring, automatically generated and updated documentation, error tracking and logging, API analytics, quality scoring, and much more. To get started with Treblle visit [treblle.com](https://treblle.com).

## Project Structure

#### **Core**
The Core project contains the domain and business logic. This is where the entity models and everything needed to interact with them is defined. The interfaces for repositories are defined here but it's left to the Infrastructure layer to implement them.

#### **Infrastructure**
The Infrastructure project handles the communication with the database. It contains the implementations of the repositories defined in the Core project.

#### **Presentation**
The Presentation project implements the Minimal API and contains everything that's needed to facilitate communication with the consumer. API endpoints are defined here and user actions are mapped to queries and commands which are sent to the Core layer using the Mediator Pattern.


## Requirements
* [.NET 6.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

## Dependencies

- [Entity Framework Core](https://github.com/dotnet/efcore)
- [ASP.NET Core Identity](https://github.com/dotnet/aspnetcore/tree/main/src/Identity)
- [Treblle for .NET Core](https://github.com/Treblle/treblle-net-core)
- [Seriog](https://github.com/serilog/serilog)
- [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
- [Mediatr](https://github.com/jbogard/MediatR)
- [AutoMapper](https://github.com/AutoMapper/AutoMapper)

## Getting started 🚀

You can install this template using the dotnet new CLI. To install the latest version of the template run the following command:
```bash
dotnet new --install TreblleDotNetApiBoilerplate.Template
```

To create a new solution based on this template use this command:
```bash
dotnet new treblle_dotnet_api_boilerplate --name {YOUR_SOLUTION_NAMESPACE}
```

Optionally, you can also specify these parameters:
```bash
-c  | --connectionString        (Your database connection string)
-j  | --jwtKey                  (Your JWT secret key)
-t  | --treblleApiKey           (Your Treblle API Key)
-tr | --treblleProjectId        (Your Treblle Project Id)
```

### Treblle

Create a FREE account on https://treblle.com to get an API key and Project ID.
Once you have your Treble API Key and Project ID you'll have to copy them into ```app.config```. Alternatively, you can pass ```--treblleApiKey``` and ```--treblleProjectId``` parameters when creating a solution from the template.

### Database

The template was built for use with the Microsoft SQL Server database, but it can easily be modified for any other database provider.
Make sure to edit the connection string in ```appsettings.json```.

### JWT Bearer Token

**Warning**: Change the default secret key for JWT token generation, as well as Issuer and Audience in ```appsettings.json``` before using this boilerplate for production apps.

## Support

If you have problems of any kind feel free to reach out via <https://treblle.com> or email vedran@treblle.com, and we'll
do our best to help you out.

## License

Copyright 2022, Treblle Limited. Licensed under the MIT license:
https://opensource.org/licenses/MIT