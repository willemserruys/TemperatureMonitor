using System.Threading.Tasks;
using API.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace API.Hubs
{
    public class TemperatureReadingHub : Hub
    {
        public TemperatureReadingHub()
        {

        }
    }
}