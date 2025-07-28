using IndoorFarmMonitor.Models.DTOs;

namespace IndoorFarmMonitor.Services.Interfaces
{
    public interface ISensorReadingProvider
    {
        Task<List<SensorReading>> GetSensorReadingsAsync();
    }
}
