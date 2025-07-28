using Microsoft.EntityFrameworkCore;

namespace IndoorFarmMonitor.Data
{
    public class IndoorFarmDbContext : DbContext
    {
        public IndoorFarmDbContext(DbContextOptions<IndoorFarmDbContext> options) : base(options) { }

        public DbSet<CombinedSensorDataEntity> CombinedSensorData { get; set; }
    }
}
