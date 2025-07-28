using IndoorFarmMonitor.Models.DTOs;
using IndoorFarmMonitor.Services.Interfaces;
using System.Text.Json;

namespace IndoorFarmMonitor.Services
{
    public class PlantConfigurationProvider : IPlantConfigurationProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PlantConfigurationProvider> _logger;

        private const string ConfigApiUrl = "http://3.0.148.231:8020/plant-configurations";

        public PlantConfigurationProvider(HttpClient httpClient, ILogger<PlantConfigurationProvider> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<PlantConfiguration>> GetPlantConfigurationsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(ConfigApiUrl);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var configs = JsonSerializer.Deserialize<List<PlantConfiguration>>(content);

                if (configs == null)
                    throw new Exception("Plant config API returned null or malformed data.");

                return configs;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "Plant config API request timed out in GetPlantConfigurationsAsync.");
                throw;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request error when calling Plant config API in GetPlantConfigurationsAsync.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch plant configurations in GetPlantConfigurationsAsync.");
                throw;
            }
        }
    }

}
