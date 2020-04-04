
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }
        public DbSet<TemperatureReading> TemperatureReadings {get; set;}
    }
}