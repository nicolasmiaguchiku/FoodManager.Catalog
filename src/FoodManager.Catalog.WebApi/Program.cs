using FoodManager.Catalog.CrossCutting.Extentions;
using Mattioli.Configurations.Extensions.FluentValidations;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

builder.Configuration
    .AddJsonFile("appsettings.json", false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{enviroment}.json", true, reloadOnChange: true)
    .AddEnvironmentVariables();

var applicationSettings = builder.Configuration.GetApplicationSettings(builder.Environment);

builder.Services
    .AddMemoryCache()
    .AddMongo(applicationSettings.MongoSettings)
    .AddServices()
    .ConfigureValidationErrorResponses()
    .ConfigureLiteBus()
    .AddOpenApi("v1")
    .AddValidators()
    .AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Host.UseSerilog(enviroment!, applicationSettings.MltSettings.SeqUrl!);

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference(options => options.Servers = []);

app.UseRequestContextLogging()
    .UseHttpsRedirection()
    .UseAuthorization();

app.MapControllers();

await app.RunAsync();