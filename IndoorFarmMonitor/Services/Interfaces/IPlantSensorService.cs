using IndoorFarmMonitor.Models.DTOs;

namespace IndoorFarmMonitor.Services.Interfaces
{
    public interface IPlantSensorService
    {
        Task<List<CombinedPlantSensorData>> GetCombinedSensorDataAsync();
    }
}
