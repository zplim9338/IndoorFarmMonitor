using IndoorFarmMonitor.Models.DTOs;
using IndoorFarmMonitor.Services.Interfaces;

namespace IndoorFarmMonitor.Repositories
{
    public class InMemoryPlantSensorRepository : IPlantSensorRepository
    {
        private static readonly List<CombinedPlantSensorData> _dataStore = new();

        public Task SaveAsync(List<CombinedPlantSensorData> data)
        {
            _dataStore.AddRange(data);
            return Task.CompletedTask;
        }
    }
}
