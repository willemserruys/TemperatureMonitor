using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemperatureReadingController : ControllerBase
    {
        private readonly ILogger<TemperatureReadingController> _logger;
        private readonly IRepository _repository;

        public TemperatureReadingController(ILogger<TemperatureReadingController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCurrentTemperatureWithMessage()
        {
            var temperature = await _repository.GetLatestReadingAsync();
            if(temperature==null) return NotFound();
            return Ok(GetTemperatureString(temperature));
        }

        private string GetTemperatureString(TemperatureReading reading)
        {
            var result = string.Empty;
            if (reading.Temperature >= 20)
            {
                result = "It's hot outside. Don't forget your sunglasses! ";
            } else if(reading.Temperature >= 19)
            {
                result = "It's not too hot nor too cold. ";
            } else if(reading.Temperature < 19)
            {
                result = "It is rather chilly outside. Wearing a coat is recommended. ";
            }
            result = result + ($"The temperature is {reading.Temperature:n2} °C.");
            return result;
        }
    }
}
