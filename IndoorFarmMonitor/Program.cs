
using IndoorFarmMonitor.Configurations;
using IndoorFarmMonitor.Data;
using IndoorFarmMonitor.Repositories;
using IndoorFarmMonitor.Services;
using IndoorFarmMonitor.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IndoorFarmMonitor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

            builder.Host.UseSerilog();

            // Load Configuration Settings
            builder.Services.Configure<ThresholdOptions>(builder.Configuration.GetSection("ThresholdOptions"));
            var storageType = builder.Configuration["Storage:Type"];

            // Register Core Services
            builder.Services.AddScoped<IPlantSensorService, PlantSensorService>();
            builder.Services.AddHttpClient<ISensorReadingProvider, SensorReadingProvider>();
            builder.Services.AddHttpClient<IPlantConfigurationProvider, PlantConfigurationProvider>();

            // Register Data Storage (based on config)
            switch (storageType)
            {
                case "PostgreSql":
                    builder.Services.AddScoped<IPlantSensorRepository, PostgreSqlPlantSensorRepository>();
                    builder.Services.AddDbContext<IndoorFarmDbContext>(opt =>
                        opt.UseNpgsql(builder.Configuration.GetConnectionString("IndoorFarmDb")));
                    break;

                case "JsonFile":
                    builder.Services.AddScoped<IPlantSensorRepository, JsonFilePlantSensorRepository>();
                    break;

                case "InMemory":
                    builder.Services.AddSingleton<IPlantSensorRepository, InMemoryPlantSensorRepository>();
                    break;

                default:
                    throw new Exception("Invalid Storage:Type specified in configuration.");
            }

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
