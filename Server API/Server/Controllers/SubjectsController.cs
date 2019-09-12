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
    [Route("api/subjects")]
    [ApiController]
    public class SubjectsController : Controller
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private Bcrypt hash;

        public SubjectsController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
            hash = new Bcrypt(_repository);
        }
        /// <summary>
        /// Create new subject
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST /subjects 
        /// </remarks>
        /// <response code="201"> Subject successfully required </response>
        /// <response code="400"> Bad request </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="402"> Name is use </response>
        /// <response code="500"> Internal server error </response>
        [HttpPost(Name = "CreateSubject")]
        [Authorize]
        public async Task<IActionResult> CreateSubject([FromBody]Subjects subject)
        {
            try
            {
                if (subject == null)
                {
                    _logger.LogError("Subjects object sent from client is null");
                    return BadRequest("Subjects object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid subject object sent from client");
                    return BadRequest("Invalid model object");
                }
                Subjects test = await _repository.Subjects.GetSubjectsByNameAsync(subject.Name);
                if (test != null && test.Name == subject.Name)
                {
                    _logger.LogError("Name of subject object sent from client is use");
                    return BadRequest("Name is in use");
                }
                await _repository.Subjects.CreateSubjectsAsync(subject);
                _repository.Save();

                // return 201 status
                return CreatedAtRoute("CreateSubject", new { id = subject.Id }, subject);
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong inside CreateSubject action: {e.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Delete subject
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     DELETE /subjects/1
        /// </remarks>
        /// <response code="201"> Subject successfully required </response>
        /// <response code="404"> Not found </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="500"> Internal server error </response>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteSubjects(int id)
        {
            try
            {
                var subject = await _repository.Subjects.GetSubjectsByIdAsync(id);
                if (subject == null)
                {
                    _logger.LogError($"Subject with id {id}, hasn't been found in database");
                    return NotFound();
                }

                await _repository.Subjects.DeleteSubjectsAsync(subject);
                _repository.Save();

                _logger.LogInfo($"Subject with id {id} deleted");
                return Ok("Subject removed successfully");
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong inside DeleteSubjects action: {e.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Update subject
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /subjects/1
        ///     
        /// </remarks>
        /// <response code="200"> Subject successfully modified </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="404"> Not found </response> 
        /// <response code="500"> Internal server error </response>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> ModifySubjects(int id, [FromBody]Subjects subject)
        {
            try
            {
                Subjects target = await _repository.Subjects.GetSubjectsByIdAsync(id);
                if (target == null)
                {
                    return NotFound();
                }
                target.Name = subject.Name;
                Subjects test = await _repository.Subjects.GetSubjectsByNameAsync(target.Name);
                if (test != null && test.Name == target.Name)
                {
                    _logger.LogError("Name of subject object sent from client is use");
                    return BadRequest("Name is in use");
                }
                

                await _repository.Subjects.ModifySubjectsAsync(target);
                return Ok("Subject successfully modified");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Return all subjects in database
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /subjects
        ///     
        /// </remarks>
        /// <returns> Array of subjects </returns>
        /// <response code="200"> Return all subjects </response>
        /// <response code="400"> Item is null</response> 
        /// <response code="401"> Unauthorized </response>
        /// <response code="500"> Internal server error </response>
        [HttpGet]
        public async Task<IActionResult> GetAllSubjects()
        {
            try
            {
                var subjects = await _repository.Subjects.GetAllSubjectsAsync();
                _logger.LogInfo($"Returned all subjects from database.");

                return Ok(subjects);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllSubjects action: {ex.Message}");
                return NotFound();
                // return StatusCode(500, "Internal server error" + ex);
            }
        }
        /// <summary>
        /// Find subject by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /subjects/1
        ///     
        /// </remarks>
        /// <returns> Subjects object </returns>
        /// <response code="200"> Return subjects object </response>
        /// <response code="400"> Item is null </response> 
        /// <response code="401"> Unauthorized </response>
        /// <response code="500"> Internal server error </response>
        [HttpGet("{id}", Name = "SubjectsById")]
        public async Task<IActionResult> GetSubjectsById(int id)
        {
            try
            {
                Subjects ret = await _repository.Subjects.GetSubjectsByIdAsync(id);
                _logger.LogInfo($"Returned subject by id from database.");
                return Ok(ret);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetSubjectsById action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }
    }
}
