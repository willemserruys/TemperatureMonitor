using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HourRegistration.DataAccess.Models;
using System;
using Microsoft.Extensions.Logging;

namespace HourRegistration.DataAccess.Repositories
{

    public class UserRepository
    {
        private readonly ILogger _logger;
        private readonly UserContext _context;

        public UserRepository(UserContext context, ILogger<UserContext> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> CreateAsync(string name, string password)
        {
            try
            {

                var user = await _context.AddAsync(new User { UserName = name, PassWord = password });
                await _context.SaveChangesAsync();
                _logger.LogInformation("User Created with Name {0}", name);
                return user.Entity;
            }
            catch (Exception e)
            {
                var errorMessage = string.Format("User not created: {0}", e.InnerException.Message);
                _logger.LogError(errorMessage);
                throw (new Exception(errorMessage));
            };
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

    }
}
