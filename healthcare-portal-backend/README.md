# Entity Framework Core (SQL Server) 
dotnet add package Microsoft.EntityFrameworkCore.SqlServer				# SQL Server support for EF Core
dotnet add package Microsoft.EntityFrameworkCore.Tools					# Tools to scaffold DbContext from database

# MediatR (CQRS/Handlers)
dotnet add package MediatR												# Core CQRS handler interface
dotnet add package MediatR.Extensions.Microsoft.DependencyInjection		# Dependency Injection support for MediatR

# FluentValidation
dotnet add package FluentValidation										# Fluent validation rules
dotnet add package FluentValidation.AspNetCore							# ASP.NET Core integration

# Scrutor (Decorator Pattern)
dotnet add package Scrutor												# Enables .Decorate() for services

# Newtonsoft.Json
dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson				# For custom JSON formatting and exception handling

# Swagger/API Documentation
dotnet add package Swashbuckle.AspNetCore								# Swagger UI and docs for your API

# Update DBContext/Models 
dotnet ef dbcontext scaffold "Server=.;Database=Healthcare Portal;User Id=sa;Password=Software!123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -f --project "Healthcare Patient Portal.csproj" --startup-project "Healthcare Patient Portal.csproj"
