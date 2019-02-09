using Microsoft.EntityFrameworkCore;
using HourRegistration.DataAccess.Models;

namespace HourRegistration.DataAccess
{
    public class TemperatureReadingContext : DbContext
    {
        public DbSet<TemperatureReading> TemperatureReading { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=HourRegistration.db");
        }
    }
}

