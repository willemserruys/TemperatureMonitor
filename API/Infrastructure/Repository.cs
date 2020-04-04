using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure
{
    public class Repository: IRepository
    {
        protected DataContext _dbContext { get; set;}
        public Repository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<TemperatureReading> GetLatestReadingAsync() {
            return _dbContext.TemperatureReadings.OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();
        }
    }
}