using DemoCICD.API.Middleware;
using DemoCICD.Application.DependencyInjection.Extensions;
using DemoCICD.Persistence.DependencyInjection.Extensions;
using DemoCICD.Presentation.DependencyInjection.Extension;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
Log.Logger = new LoggerConfiguration()
    .ReadFrom
    .Configuration(configuration)
    .CreateLogger();

builder
    .Logging
    .ClearProviders()
    .AddSerilog();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

// Api Configuration
builder
    .Services
    .AddControllers()
    .AddApplicationPart(DemoCICD.Presentation.AssemblyReference.Assembly);

// Add configuration to retry
builder
    .Services
    .ConfigurationServiceRetryOptions(builder.Configuration.GetSection("SqlServerRetryOptions"));

// Add appliccation, infrastructure, persistence
builder.Services.AddPersistence();
builder.Services.AddApplication();
builder.Services.AddPresentation();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (builder.Environment.IsProduction())
{
    app.UseHttpsRedirection();
    app.UseHsts();
}

app.UseAuthorization();
app.MapControllers();

if (builder.Environment.IsDevelopment() || builder.Environment.IsStaging())
{
    app.ConfigureSwagger();
}

try
{
    await app.RunAsync();
    Log.Information("Stopped cleanly!");
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhanled exeption occured during boostrapping");
    await app.StopAsync();
}
finally
{
    Log.CloseAndFlush();
    await app.DisposeAsync();
}
