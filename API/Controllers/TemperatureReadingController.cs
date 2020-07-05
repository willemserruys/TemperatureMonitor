using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Helpers;
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
        private readonly IRepository _repository;

        public TemperatureReadingController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCurrentTemperatureWithMessage()
        {
            var temperature = await _repository.GetLatestReadingAsync();
            if (temperature == null) return NotFound();
            return Ok(TemperatureReadingHelper.GetTemperatureString(temperature.Temperature));
        }

    }
}
