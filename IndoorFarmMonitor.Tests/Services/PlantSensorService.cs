using IndoorFarmMonitor.Configurations;
using IndoorFarmMonitor.Models.DTOs;
using IndoorFarmMonitor.Services;
using IndoorFarmMonitor.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace IndoorFarmMonitor.Tests.Services
{
    public class PlantSensorServiceTests
    {
        private PlantSensorService _plantSensorService;
        private Mock<ISensorReadingProvider> _mockSensorProvider;
        private Mock<IPlantConfigurationProvider> _mockConfigProvider;
        private Mock<IPlantSensorRepository> _mockRepository;
        private Mock<ILogger<PlantSensorService>> _mockLogger;
        private IOptions<ThresholdOptions> _options;

        [SetUp]
        public void SetUp()
        {
            _mockSensorProvider = new Mock<ISensorReadingProvider>();
            _mockConfigProvider = new Mock<IPlantConfigurationProvider>();
            _mockRepository = new Mock<IPlantSensorRepository>();
            _mockLogger = new Mock<ILogger<PlantSensorService>>();

            _options = Options.Create(new ThresholdOptions
            {
                Temperature = 2,
                Humidity = 4,
                Light = 10
            });

            _plantSensorService = new PlantSensorService(
                _mockSensorProvider.Object,
                _mockConfigProvider.Object,
                _mockRepository.Object,
                _options,
                _mockLogger.Object
            );
        }

        [Test]
        public async Task GetMergedSensorDataAsync_ReturnsMergedResult()
        {
            // Arrange
            var sensorData = new List<SensorReading>
            {
                new SensorReading { TrayId = 1,  Temperature = 25.0F, Humidity = 60.0F, Light=1200.0F },
                new SensorReading { TrayId = 2, Temperature = 22.5F, Humidity = 65.0F, Light=1100.0F }
            };

            var configData = new List<PlantConfiguration>
            {
                new PlantConfiguration { TrayId = 1, PlantType= "Lettuce",TargetTemperature = 24.0F, TargetHumidity = 55.0F, TargetLight = 1110.0F },
                new PlantConfiguration { TrayId = 2, PlantType= "Spinach",TargetTemperature = 23.0F, TargetHumidity = 60.0F, TargetLight = 1110.0F }
            };

            _mockSensorProvider.Setup(p => p.GetSensorReadingsAsync()).ReturnsAsync(sensorData);
            _mockConfigProvider.Setup(p => p.GetPlantConfigurationsAsync()).ReturnsAsync(configData);

            // Act
            var result = await _plantSensorService.GetCombinedSensorDataAsync();

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));

            var tray1 = result.Find(x => x.TrayId == 1);
            Assert.That(tray1, Is.Not.Null);
            Assert.That(tray1.Temperature, Is.EqualTo(25.0));
            Assert.That(tray1.TargetTemperature, Is.EqualTo(24.0));
            Assert.That(tray1.IsTemperatureOutOfRange, Is.False);
            Assert.That(tray1.Humidity, Is.EqualTo(60.0));
            Assert.That(tray1.TargetHumidity, Is.EqualTo(55.0));
            Assert.That(tray1.IsHumidityOutOfRange, Is.True);
            Assert.That(tray1.Light, Is.EqualTo(1200.0));
            Assert.That(tray1.TargetLight, Is.EqualTo(1110.0));
            Assert.That(tray1.IsLightOutOfRange, Is.True);
        }
    }
}