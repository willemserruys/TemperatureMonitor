using System.Threading.Tasks;
using API.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace API.Hubs
{
    public class TemperatureReadingHub : Hub
    {
        private readonly ILogger<TemperatureReadingHub> _logger;
        public TemperatureReadingHub(ILogger<TemperatureReadingHub> logger)
        {
            _logger = logger;
        }
        public Task SendTemperatureMessage(string message)
        {
            _logger.LogInformation($"Message {message} is being sent to client");
            return Clients.All.SendAsync("UpdateTemperature", message);
        }
    }
}