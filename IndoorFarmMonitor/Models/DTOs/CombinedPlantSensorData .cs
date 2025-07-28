namespace IndoorFarmMonitor.Models.DTOs
{
    public class CombinedPlantSensorData
    {
        public long TrayId { get; set; }
        public required string PlantType { get; set; }
        public float Temperature { get; set; }
        public float TargetTemperature { get; set; }
        public float Humidity { get; set; }
        public float TargetHumidity { get; set; }
        public float Light { get; set; }
        public float TargetLight { get; set; }
        public bool IsTemperatureOutOfRange { get; set; }
        public bool IsHumidityOutOfRange { get; set; }
        public bool IsLightOutOfRange { get; set; }
    }
}
