using IndoorFarmMonitor.Data;
using IndoorFarmMonitor.Models.DTOs;
using IndoorFarmMonitor.Services.Interfaces;

namespace IndoorFarmMonitor.Repositories
{
    public class PostgreSqlPlantSensorRepository : IPlantSensorRepository
    {
        private readonly IndoorFarmDbContext _context;

        public PostgreSqlPlantSensorRepository(IndoorFarmDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(List<CombinedPlantSensorData> data)
        {
            var entities = data.Select(d => new CombinedSensorDataEntity
            {
                TrayId = d.TrayId,
                PlantType = d.PlantType,
                Temperature = d.Temperature,
                TargetTemperature = d.TargetTemperature,
                Humidity = d.Humidity,
                TargetHumidity = d.TargetHumidity,
                Light = d.Light,
                TargetLight = d.TargetLight,
                IsTemperatureOutOfRange = d.IsTemperatureOutOfRange,
                IsHumidityOutOfRange = d.IsHumidityOutOfRange,
                IsLightOutOfRange = d.IsLightOutOfRange
            });

            await _context.CombinedSensorData.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }
    }
}
