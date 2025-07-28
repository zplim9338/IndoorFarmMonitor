using IndoorFarmMonitor.Models.DTOs;

namespace IndoorFarmMonitor.Services.Interfaces
{
    public interface IPlantConfigurationProvider
    {
        Task<List<PlantConfiguration>> GetPlantConfigurationsAsync();
    }
}
