using System;
using System.Threading;
using System.Threading.Tasks;
using API.Constants;
using API.Hubs;
using API.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace API.HostedServices
{
    public class TemperatureReadingHostedService : IHostedService, IDisposable
    {
        private readonly ISubscriber _pubSub;
        private readonly ILogger<TemperatureReadingHostedService> _logger;
        private readonly IHubContext<TemperatureReadingHub> _tempReadingHub;

        public TemperatureReadingHostedService(ILogger<TemperatureReadingHostedService> logger,
        IHubContext<TemperatureReadingHub> tempReadingHub)
        {
            _logger = logger;

            _logger.LogInformation("TemperatureReading Hosted Service Created...");

            // Redis server runs locally, on default port
            _pubSub = ConnectionMultiplexer.Connect("localhost").GetSubscriber();

            _tempReadingHub = tempReadingHub;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("TemperatureReading Hosted Service Started...");
            // Subscribe to channel where temperature updates will be published
            return _pubSub.SubscribeAsync("TemperatureUpdated", (channel, message) => MessageAction(message));
        }

        public void Dispose()
        {
            _logger.LogInformation("TemperatureReading Hosted Service is disposing...");
        }

        public async void MessageAction(RedisValue msg)
        {
            _logger.LogInformation($"Message received is {msg}. Sending To group {Constants.SignalRConstants.TemperatureReadings}");

            await _tempReadingHub.Clients.All.SendAsync(Constants.SignalRConstants.TemperatureReadings, msg);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Unsubscribe from the channel
            return _pubSub.UnsubscribeAllAsync();
        }
    }
}