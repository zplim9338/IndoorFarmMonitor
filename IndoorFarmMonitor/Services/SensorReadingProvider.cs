using IndoorFarmMonitor.Models.DTOs;
using IndoorFarmMonitor.Services.Interfaces;
using System.Text.Json;

namespace IndoorFarmMonitor.Services
{
    public class SensorReadingProvider : ISensorReadingProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SensorReadingProvider> _logger;

        private const string SensorApiUrl = "http://3.0.148.231:8010/sensor-readings";

        public SensorReadingProvider(HttpClient httpClient, ILogger<SensorReadingProvider> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<SensorReading>> GetSensorReadingsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(SensorApiUrl);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var sensors = JsonSerializer.Deserialize<List<SensorReading>>(content);

                if (sensors == null)
                    throw new Exception("Sensor API returned null or malformed data.");

                return sensors;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "Sensor API request timed out in GetSensorReadingsAsync.");
                throw;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request error when calling Sensor API in GetSensorReadingsAsync.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch sensor readings in GetSensorReadingsAsync");
                throw;
            }
        }
    }
}
