using IndoorFarmMonitor.Models.DTOs;

namespace IndoorFarmMonitor.Services.Interfaces
{
    public interface IPlantSensorRepository
    {
        Task SaveAsync(List<CombinedPlantSensorData> data);
    }
}
