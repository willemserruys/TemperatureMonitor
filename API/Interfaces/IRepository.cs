using System.Threading.Tasks;
using API.Models;

namespace API.Interfaces
{
    public interface IRepository
    {
         Task<TemperatureReading> GetLatestReadingAsync();
    }
}