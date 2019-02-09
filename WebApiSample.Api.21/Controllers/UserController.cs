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
    public class UserController : ControllerBase
    {
        private readonly UserRepository _repository;
        private readonly ILogger _logger;

        public UserController(UserRepository repository, ILogger<UserController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllAsync()
        {
            _logger.LogDebug("Getting All Users");
            var users = await _repository.GetUsersAsync();

            return users;
        }

        [HttpPost("{name}/{password}", Name = "Create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
        public async Task<ActionResult<UserResponse>> CreateAsync(string name, string password)
        {
            var usRes = new UserResponse();
            _logger.LogDebug("Create User Request Received, name = {0}, password = {1}", name, password);
            try
            {
                var user = await _repository.CreateAsync(name, password);
                usRes.User = user;
                usRes.IsCreated = true;
                return usRes;
            }
            catch (Exception e)
            {
                var user = new User { UserName = name, PassWord = password };
                usRes.User = user;
                usRes.IsCreated = false;
                usRes.FailedMessage = e.Message;
                return usRes;
            }
        }
    }


}