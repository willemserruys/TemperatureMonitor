using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HourRegistration.DataAccess.Models;
using System;
using Microsoft.Extensions.Logging;


namespace HourRegistration.DataAccess.Repositories
{
    public class TemperatureReadingRepository
    {
        private readonly ILogger _logger;
        private readonly TemperatureReadingContext _context;

        public TemperatureReadingRepository(TemperatureReadingContext context, ILogger<TemperatureReadingContext> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<TemperatureReading>> GetTemperatureReadingsAsync()
        {
            return await _context.TemperatureReading.ToListAsync();
        }


    }
}
