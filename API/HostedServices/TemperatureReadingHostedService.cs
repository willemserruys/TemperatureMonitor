using System;
using System.Threading;
using System.Threading.Tasks;
using API.Constants;
using API.Helpers;
using API.Hubs;
using API.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly IConfiguration _config;

        private readonly IServiceProvider _serviceProvider;

        public TemperatureReadingHostedService(ILogger<TemperatureReadingHostedService> logger,
        IHubContext<TemperatureReadingHub> tempReadingHub, IConfiguration config, IServiceProvider serviceProvider)
        {
            _config = config;

            _logger = logger;

            _logger.LogInformation("TemperatureReading Hosted Service Created...");

            // Redis server runs locally, on default port
            _pubSub = ConnectionMultiplexer.Connect(_config["RedisSettings:ServerAddress"]).GetSubscriber();

            _tempReadingHub = tempReadingHub;

            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("TemperatureReading Hosted Service Started...");
            // Subscribe to channel where temperature updates will be published
            return _pubSub.SubscribeAsync(Constants.RedisConstants.TemperatureUpdatedSubscription, (channel, message) => MessageAction(message));
        }

        public void Dispose()
        {
            _logger.LogInformation("TemperatureReading Hosted Service is disposing...");
        }

        public async void MessageAction(RedisValue msg)
        {
            _logger.LogInformation($"Message received from {Constants.RedisConstants.TemperatureUpdatedSubscription}. Sending To group {Constants.SignalRConstants.TemperatureReadings}");

            using (var scope = _serviceProvider.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IRepository>();
                var temperatureReading = await repository.GetLatestReadingAsync();
                var temperatureMsg = TemperatureReadingHelper.GetTemperatureString(temperatureReading.Temperature);
                await _tempReadingHub.Clients.All.SendAsync(Constants.SignalRConstants.TemperatureReadings, temperatureMsg);
                _logger.LogInformation($"Following message was sent to clients: {temperatureMsg}");

            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Unsubscribe from the channel
            return _pubSub.UnsubscribeAllAsync();
        }
    }
}