using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IndoorFarmMonitor.Data
{
    [Table("combined_sensor_data")]
    public class CombinedSensorDataEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("tray_id")]
        public long TrayId { get; set; }
        [Column("plant_type")]
        public required string PlantType { get; set; } 
        [Column("temperature")]
        public float Temperature { get; set; }
        [Column("target_temperature")]
        public float TargetTemperature { get; set; }
        [Column("humidity")]
        public float Humidity { get; set; }
        [Column("target_humidity")]
        public float TargetHumidity { get; set; }
        [Column("light")]
        public float Light { get; set; }
        [Column("target_light")]
        public float TargetLight { get; set; }
        [Column("is_temperature_out_of_range")]
        public bool IsTemperatureOutOfRange { get; set; }
        [Column("is_humidity_out_of_range")]
        public bool IsHumidityOutOfRange { get; set; }
        [Column("is_light_out_of_range")]
        public bool IsLightOutOfRange { get; set; }
        [Column("timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    }
}
