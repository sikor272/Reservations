using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Server.Helpers;
using Authorization;
using Microsoft.Extensions.Configuration;
using Contracts;
using LoggerServices;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : Controller
    {
        private IConfiguration _config;
        private IRepositoryWrapper _repository;
        private ILoggerManager _logger;
        private JWToken JWT;
        private Bcrypt hash;

        public LoginController(IConfiguration config, IRepositoryWrapper repository, ILoggerManager logger)
        {
            _config = config;
            _repository = repository;
            _logger = logger;
            JWT = new JWToken(config);
            hash = new Bcrypt(repository);
        }
        /// <summary>
        /// Login to server
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /login
        ///     
        /// </remarks>
        /// <response code="200"> Success. Returns login and JSON web token </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="500"> Internal server error </response>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Users userData)
        {
            IActionResult response = Unauthorized();
            try
            {
                var user = await hash.AuthorisationUsers(userData);
                if (user != null)
                {
                    var tokenString = JWT.GenerateJSONWebToken(user);
                    response = Ok(new { id = user.Id, token = tokenString });
                }
                else
                {
                    response = Unauthorized();
                }
                return response;
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error" + e);
            }
        }
    }
}
