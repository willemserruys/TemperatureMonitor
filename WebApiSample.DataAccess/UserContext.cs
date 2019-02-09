using Microsoft.EntityFrameworkCore;
using HourRegistration.DataAccess.Models;

namespace HourRegistration.DataAccess
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=HourRegistration.db");
        }
    }
}
