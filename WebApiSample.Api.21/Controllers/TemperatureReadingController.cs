using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using HourRegistration.DataAccess.Models;
using HourRegistration.DataAccess.Repositories;
using System;
using Microsoft.AspNetCore.Http;

namespace HourRegistration.API
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TemperatureReadingController : ControllerBase
    {
        private readonly TemperatureReadingRepository _repository;
        private readonly ILogger _logger;

        public TemperatureReadingController(TemperatureReadingRepository repository, ILogger<TemperatureReadingController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<TemperatureReading>>> GetAllAsync()
        {
            _logger.LogDebug("Getting All temperature readings");
            var temps = await _repository.GetTemperatureReadingsAsync();

            return temps;
        }
    }


}