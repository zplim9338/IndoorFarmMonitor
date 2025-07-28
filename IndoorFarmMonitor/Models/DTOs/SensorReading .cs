using System.Text.Json.Serialization;

namespace IndoorFarmMonitor.Models.DTOs
{
    public class SensorReading
    {
        [JsonPropertyName("tray_id")]
        public long TrayId { get; set; }
        [JsonPropertyName("temperature")]
        public float Temperature { get; set; }
        [JsonPropertyName("humidity")]
        public float Humidity { get; set; }
        [JsonPropertyName("light")]
        public float Light { get; set; }
    }
}
