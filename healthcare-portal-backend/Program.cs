using Microsoft.EntityFrameworkCore;
using FluentValidation;
using MediatR;
using Healthcare_Patient_Portal.Models;
using Healthcare_Patient_Portal.Infrastructure.Middlewares;
using Healthcare_Patient_Portal.Infrastructure.Validations;


var builder = WebApplication.CreateBuilder(args);

// Load connection string from appsettings.json
 var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register DbContext with Dependency Injection (DI)
builder.Services.AddDbContext<HealthcarePortalContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(o => o.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.Decorate(typeof(IRequestHandler<,>), typeof(FluentValidationPipeline<,>));


var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.MapControllers();

app.Run();
