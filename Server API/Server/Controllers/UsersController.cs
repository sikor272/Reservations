using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using LoggerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : Controller
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public UsersController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST /users 
        /// </remarks>
        /// <response code="201"> User successfully required </response>
        /// <response code="400"> Bad request </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="500"> Internal server error </response>
        [HttpPost(Name = "CreateUser")]
        [Authorize]
        public async Task<IActionResult> CreateUser([FromBody]Users user)
        {
            try
            {
                if (user == null)
                {
                    _logger.LogError("User object sent from client is null");
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid user object sent from client");
                    return BadRequest("Invalid model object");
                }
                Bcrypt hash = new Bcrypt(_repository);
                user.Password = hash.Crypting(user.Password);
                await _repository.Users.CreateUsersAsync(user);
                _repository.Save();

                // return 201 status
                return CreatedAtRoute("CreateUser", new { id = user.Id }, user);
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong inside CreateUsers action: {e.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     DELETE /users/1
        /// </remarks>
        /// <response code="201"> User successfully required </response>
        /// <response code="404"> Not found </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="500"> Internal server error </response>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _repository.Users.GetUsersByIdAsync(id);
                if (user == null)
                {
                    _logger.LogError($"User with id {id}, hasn't been found in database");
                    return NotFound();
                }

                await _repository.Users.DeleteUsersAsync(user);
                _repository.Save();

                _logger.LogInfo($"Used with id {id} deleted");
                return Ok("User removed successfully");
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong inside DeleteUsers action: {e.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
