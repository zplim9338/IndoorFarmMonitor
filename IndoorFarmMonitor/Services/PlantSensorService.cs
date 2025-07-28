using IndoorFarmMonitor.Configurations;
using IndoorFarmMonitor.Models.DTOs;
using IndoorFarmMonitor.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace IndoorFarmMonitor.Services
{
    public class PlantSensorService : IPlantSensorService
    {
        private readonly ISensorReadingProvider _sensorProvider;
        private readonly IPlantConfigurationProvider _configProvider;
        private readonly IPlantSensorRepository _repository;
        private readonly ILogger<PlantSensorService> _logger;
        private readonly ThresholdOptions _thresholds;

        public PlantSensorService(
            ISensorReadingProvider sensorProvider,
            IPlantConfigurationProvider configProvider,
            IPlantSensorRepository repository,
            IOptions<ThresholdOptions> options,
            ILogger<PlantSensorService> logger)
        {
            _sensorProvider = sensorProvider;
            _configProvider = configProvider;
            _repository = repository;
            _thresholds = options.Value;
            _logger = logger;
        }

        public async Task<List<CombinedPlantSensorData>> GetCombinedSensorDataAsync()
        {
            try
            {
                var sensors = await _sensorProvider.GetSensorReadingsAsync();
                var configs = await _configProvider.GetPlantConfigurationsAsync();

                var combined = CombineSensorData(sensors, configs);

                await _repository.SaveAsync(combined);
                return combined;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetCombinedSensorDataAsync");
                throw;
            }
        }

        private List<CombinedPlantSensorData> CombineSensorData(List<SensorReading> sensors, List<PlantConfiguration> configs)
        {
            return sensors
                .Join(configs,
                      sensor => sensor.TrayId,
                      config => config.TrayId,
                      (sensor, config) => new CombinedPlantSensorData
                      {
                          TrayId = sensor.TrayId,
                          PlantType = config.PlantType,
                          Temperature = sensor.Temperature,
                          TargetTemperature = config.TargetTemperature,
                          Humidity = sensor.Humidity,
                          TargetHumidity = config.TargetHumidity,
                          Light = sensor.Light,
                          TargetLight = config.TargetLight,
                          IsTemperatureOutOfRange = Math.Abs(sensor.Temperature - config.TargetTemperature) > _thresholds.Temperature,
                          IsHumidityOutOfRange = Math.Abs(sensor.Humidity - config.TargetHumidity) > _thresholds.Humidity,
                          IsLightOutOfRange = Math.Abs(sensor.Light - config.TargetLight) > _thresholds.Light
                      })
                .ToList();
        }
    }
}
