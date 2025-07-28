using System.Text.Json.Serialization;

namespace IndoorFarmMonitor.Models.DTOs
{
    public class PlantConfiguration
    {
        [JsonPropertyName("tray_id")]
        public long TrayId { get; set; }
        [JsonPropertyName("plant_type")]
        public required string PlantType { get; set; }
        [JsonPropertyName("target_temperature")]
        public float TargetTemperature { get; set; }
        [JsonPropertyName("target_humidity")]
        public float TargetHumidity { get; set; }
        [JsonPropertyName("target_light")]
        public float TargetLight { get; set; }
    }
}
