using IndoorFarmMonitor.Models.DTOs;
using IndoorFarmMonitor.Services.Interfaces;
using System.Text.Json;

namespace IndoorFarmMonitor.Repositories
{
    public class JsonFilePlantSensorRepository : IPlantSensorRepository
    {
        private const string FilePath = "plant_data.json";

        public async Task SaveAsync(List<CombinedPlantSensorData> data)
        {
            var json = JsonSerializer.Serialize(data);
            await File.WriteAllTextAsync(FilePath, json);
        }
    }
}
