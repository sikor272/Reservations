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
    [Route("api/teachers")]
    [ApiController]
    public class TeachersController : Controller
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private Bcrypt hash;

        public TeachersController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
            hash = new Bcrypt(_repository);
        }
        /// <summary>
        /// Create new teacher
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST /teachers 
        /// </remarks>
        /// <response code="201"> Teacher successfully required </response>
        /// <response code="400"> Bad request </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="402"> Name is use </response>
        /// <response code="500"> Internal server error </response>
        [HttpPost(Name = "CreateTeacher")]
        [Authorize]
        public async Task<IActionResult> CreateTeacher([FromBody]Teachers teacher)
        {
            try
            {
                if (teacher == null)
                {
                    _logger.LogError("Teachers object sent from client is null");
                    return BadRequest("Teachers object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid teacher object sent from client");
                    return BadRequest("Invalid model object");
                }
                await _repository.Teachers.CreateTeachersAsync(teacher);
                _repository.Save();

                // return 201 status
                return CreatedAtRoute("CreateTeacher", new { id = teacher.Id }, teacher);
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong inside CreateTeacher action: {e.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Delete teacher
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     DELETE /teachers/1
        /// </remarks>
        /// <response code="201"> Teacher successfully required </response>
        /// <response code="404"> Not found </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="500"> Internal server error </response>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTeachers(int id)
        {
            try
            {
                var teacher = await _repository.Teachers.GetTeachersByIdAsync(id);
                if (teacher == null)
                {
                    _logger.LogError($"Teacher with id {id}, hasn't been found in database");
                    return NotFound();
                }

                await _repository.Teachers.DeleteTeachersAsync(teacher);
                _repository.Save();

                _logger.LogInfo($"Teacher with id {id} deleted");
                return Ok("Teacher removed successfully");
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong inside DeleteTeachers action: {e.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Update teacher
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /teachers/1
        ///     
        /// </remarks>
        /// <response code="200"> Teacher successfully modified </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="404"> Not found </response> 
        /// <response code="500"> Internal server error </response>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> ModifyTeachers(int id, [FromBody]Teachers teacher)
        {
            try
            {
                Teachers target = await _repository.Teachers.GetTeachersByIdAsync(id);
                if (target == null)
                {
                    return NotFound();
                }
                target.Name = teacher.Name;
                target.Surname = teacher.Surname;
                target.Title = teacher.Title;
                await _repository.Teachers.ModifyTeachersAsync(target);
                return Ok("Teacher successfully modified");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Return all teachers in database
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /teachers
        ///     
        /// </remarks>
        /// <returns> Array of teachers </returns>
        /// <response code="200"> Return all teachers </response>
        /// <response code="400"> Item is null</response> 
        /// <response code="401"> Unauthorized </response>
        /// <response code="500"> Internal server error </response>
        [HttpGet]
        public async Task<IActionResult> GetAllTeachers()
        {
            try
            {
                var teachers = await _repository.Teachers.GetAllTeachersAsync();
                _logger.LogInfo($"Returned all teachers from database.");

                return Ok(teachers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllTeachers action: {ex.Message}");
                return NotFound();
                // return StatusCode(500, "Internal server error" + ex);
            }
        }
        /// <summary>
        /// Find teacher by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /teachers/1
        ///     
        /// </remarks>
        /// <returns> Teachers object </returns>
        /// <response code="200"> Return teachers object </response>
        /// <response code="400"> Item is null </response> 
        /// <response code="401"> Unauthorized </response>
        /// <response code="500"> Internal server error </response>
        [HttpGet("{id}", Name = "TeachersById")]
        public async Task<IActionResult> GetTeachersById(int id)
        {
            try
            {
                Teachers ret = await _repository.Teachers.GetTeachersByIdAsync(id);
                _logger.LogInfo($"Returned teacher by id from database.");
                return Ok(ret);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetTeachersById action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }
        /// <summary>
        /// Find teachers by name
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /teachers/name/Adam
        ///     
        /// </remarks>
        /// <returns> Teachers object </returns>
        /// <response code="200"> Return teachers object </response>
        /// <response code="400"> Item is null </response> 
        /// <response code="401"> Unauthorized </response>
        /// <response code="500"> Internal server error </response>
        [HttpGet("name/{name}", Name = "TeachersByName")]
        public async Task<IActionResult> GetTeachersByName(string name)
        {
            try
            {
                Teachers[] ret = await _repository.Teachers.GetTeachersByNameAsync(name);
                _logger.LogInfo($"Returned teachers by name from database.");
                return Ok(ret);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetTeachersByName action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }
        /// <summary>
        /// Find teachers by surname
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /teachers/surname/Kowalski
        ///     
        /// </remarks>
        /// <returns> Teachers object </returns>
        /// <response code="200"> Return teachers object </response>
        /// <response code="400"> Item is null </response> 
        /// <response code="401"> Unauthorized </response>
        /// <response code="500"> Internal server error </response>
        [HttpGet("surname/{surname}", Name = "TeachersBySurname")]
        public async Task<IActionResult> GetTeachersBySurname(string surname)
        {
            try
            {
                Teachers[] ret = await _repository.Teachers.GetTeachersBySurnameAsync(surname);
                _logger.LogInfo($"Returned teachers by name from database.");
                return Ok(ret);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetTeachersBySurname action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }
    }
}
