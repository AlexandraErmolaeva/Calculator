using Calculator.Api.Extensions;
using Calculator.Api.Filters;
using Calculator.Api.Middleware;
using Serilog;
using System.Reflection;

var assemblyName = Assembly.GetExecutingAssembly().GetName();

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

builder.Host.UseSerilog();

LogInformation("- Adding Services...");
services.AddServices();

LogInformation("- Adding CORS...");
services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost8080", builder =>
    {
        builder.WithOrigins("http://localhost:8080")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

LogInformation("- Adding Controllers...");
services.AddControllers(options =>
{
    LogInformation($"- Adding {nameof(LoggingFilter)}...");
    options.Filters.Add<LoggingFilter>();

    LogInformation($"- Adding {nameof(ExpressionConversionFilter)}...");
    options.Filters.Add<ExpressionConversionFilter>();

    LogInformation($"- Adding {nameof(ValidationFilter)}...");
    options.Filters.Add<ValidationFilter>();
});

PrintApplicationInfo(assemblyName);

LogInformation("- Adding Swagger...");
services.AddCustomSwagger(assemblyName.Name);

LogInformation("- Building App...");
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    LogInformation("- Configuruing Swagger...");
    app.UseSwagger();
    app.UseSwaggerUI();
}

LogInformation($"- Using {nameof(LoggingMiddleware)}...");
app.UseMiddleware<LoggingMiddleware>();

LogInformation($"- Using {nameof(ExceptionHandlingMiddleware)}...");
app.UseMiddleware<ExceptionHandlingMiddleware>();

LogInformation("- Using CORS...");
app.UseCors("AllowLocalhost8080");

LogInformation("- Using Routing...");
app.UseRouting();

LogInformation("- Using Https Redirection...");
app.UseHttpsRedirection();

LogInformation("- Using Static Files...");
app.UseStaticFiles();

LogInformation("- Using Https Endpoints...");
app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllers();

    _ = endpoints.MapFallbackToFile("index.html");
});

try
{
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly.");
}
finally
{
    Log.CloseAndFlush();
}

#region [Print AppInfo]
static void PrintApplicationInfo(AssemblyName assemblyName)
{
    var separator = new string('-', 30);

    var applicationName = assemblyName.Name;
    var applicationVersion = assemblyName.Version?.ToString() ?? "unknown";

    var machineName = Environment.MachineName;
    var operatingSystem = Environment.OSVersion.ToString();
    var dotNetVersion = Environment.Version.ToString();
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
    var currentDirectory = Environment.CurrentDirectory;
    var processorCount = Environment.ProcessorCount;
    var startTime = DateTime.UtcNow;

    LogHeader("Application Info", separator);
    LogInformation("Application Name    : {ApplicationName}", applicationName);
    LogInformation("Application Version : {ApplicationVersion}", applicationVersion);
    LogInformation("Machine Name        : {MachineName}", machineName);
    LogInformation("Operating System    : {OperatingSystem}", operatingSystem);
    LogInformation("Environment         : {Environment}", environment);
    LogInformation("DotNet Version      : {DotNetVersion}", dotNetVersion);
    LogInformation("Processor Count     : {ProcessorCount}", processorCount);
    LogInformation("Current Directory   : {CurrentDirectory}", currentDirectory);
    LogInformation("Start Time (UTC)    : {StartTime}", startTime);
}

static void LogHeader(string title, string separator)
{
    LogInformation(separator);
    LogInformation($"  {title}");
    LogInformation(separator);
}

static void LogInformation(string message, params object[] propertyValues)
{
    Log.Information(message, propertyValues);
}
#endregion