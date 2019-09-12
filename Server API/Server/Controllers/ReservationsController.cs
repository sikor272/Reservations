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
    [Route("api/reservations")]
    [ApiController]
    public class ReservationsController : Controller
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private Bcrypt hash;

        public ReservationsController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
            hash = new Bcrypt(_repository);
        }
        /// <summary>
        /// Create new reservation
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST /reservations 
        /// </remarks>
        /// <response code="201"> Reservation successfully required </response>
        /// <response code="400"> Bad request </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="402"> Name is use </response>
        /// <response code="500"> Internal server error </response>
        [HttpPost(Name = "CreateReservation")]
        [Authorize]
        public async Task<IActionResult> CreateReservation([FromBody]Reservations reservation)
        {
            try
            {
                if (reservation == null)
                {
                    _logger.LogError("Reservations object sent from client is null");
                    return BadRequest("Reservations object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid reservation object sent from client");
                    return BadRequest("Invalid model object");
                }
                if (reservation.Begin < 8 || reservation.End > 20)
                {
                    _logger.LogError("Invalid input sent from client");
                    return BadRequest("Invalid input");
                }
                if (!await _repository.Reservations.CheckReservations(reservation))
                {
                    _logger.LogError("Reservations conflict sent from client");
                    return BadRequest("Reservations conflict");
                }
                await _repository.Reservations.CreateReservationsAsync(reservation);
                _repository.Save();

                // return 201 status
                return CreatedAtRoute("CreateReservation", new { id = reservation.Id }, reservation);
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong inside CreateReservation action: {e.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Delete reservation
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     DELETE /reservations/1
        /// </remarks>
        /// <response code="201"> Reservation successfully required </response>
        /// <response code="404"> Not found </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="500"> Internal server error </response>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReservations(int id)
        {
            try
            {
                var reservation = await _repository.Reservations.GetReservationsByIdAsync(id);
                if (reservation == null)
                {
                    _logger.LogError($"Reservation with id {id}, hasn't been found in database");
                    return NotFound();
                }

                await _repository.Reservations.DeleteReservationsAsync(reservation);
                _repository.Save();

                _logger.LogInfo($"Reservation with id {id} deleted");
                return Ok("Reservation removed successfully");
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong inside DeleteReservations action: {e.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Update reservation
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /reservations/1
        ///     
        /// </remarks>
        /// <response code="200"> Reservation successfully modified </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="404"> Not found </response> 
        /// <response code="500"> Internal server error </response>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> ModifyReservations(int id, [FromBody]Reservations reservation)
        {
            try
            {
                Reservations target = await _repository.Reservations.GetReservationsByIdAsync(id);
                if (target == null)
                {
                    return NotFound();
                }
                target.Room_id = reservation.Room_id;
                target.Subject_id = reservation.Subject_id;
                target.Teacher_id = reservation.Teacher_id;
                target.Date = reservation.Date;
                target.Begin = reservation.Begin;
                target.End = reservation.End;
                if(target.Begin < 8 || target.End > 20)
                {
                    _logger.LogError("Invalid input sent from client");
                    return BadRequest("Invalid input");
                }
                if(! await _repository.Reservations.CheckReservationsUpdate(target))
                {
                    _logger.LogError("Reservations conflict with update sent from client");
                    return BadRequest("Reservations conflict with update");
                }
                await _repository.Reservations.ModifyReservationsAsync(target);
                return Ok("Reservation successfully modified");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Return all reservations in database
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /reservations
        ///     
        /// </remarks>
        /// <returns> Array of reservations </returns>
        /// <response code="200"> Return all reservations </response>
        /// <response code="400"> Item is null</response> 
        /// <response code="401"> Unauthorized </response>
        /// <response code="500"> Internal server error </response>
        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            try
            {
                var reservations = await _repository.Reservations.GetAllReservationsAsync();
                _logger.LogInfo($"Returned all reservations from database.");

                return Ok(reservations);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllReservations action: {ex.Message}");
                return NotFound();
                // return StatusCode(500, "Internal server error" + ex);
            }
        }
        /// <summary>
        /// Find reservation by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /reservations/1
        ///     
        /// </remarks>
        /// <returns> Reservations object </returns>
        /// <response code="200"> Return reservations object </response>
        /// <response code="400"> Item is null </response> 
        /// <response code="401"> Unauthorized </response>
        /// <response code="500"> Internal server error </response>
        [HttpGet("{id}", Name = "ReservationsById")]
        public async Task<IActionResult> GetReservationsById(int id)
        {
            try
            {
                Reservations ret = await _repository.Reservations.GetReservationsByIdAsync(id);
                _logger.LogInfo($"Returned reservation by id from database.");
                return Ok(ret);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetReservationsById action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }
        /// <summary>
        /// Find reservation by subject id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /reservations/subject/1
        ///     
        /// </remarks>
        /// <returns> Reservations object </returns>
        /// <response code="200"> Return reservations object </response>
        /// <response code="400"> Item is null </response> 
        /// <response code="401"> Unauthorized </response>
        /// <response code="500"> Internal server error </response>
        [HttpGet("subject/{id}", Name = "ReservationsBySubjectsId")]
        public async Task<IActionResult> GetReservationsBySubjectsId(int id)
        {
            try
            {
                Reservations[] ret = await _repository.Reservations.GetReservationsBySubjectsIdAsync(id);
                _logger.LogInfo($"Returned reservation by subjects id from database.");
                return Ok(ret);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetReservationsBySubjectsId action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }
        /// <summary>
        /// Find reservation by teacher id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /reservations/teacher/1
        ///     
        /// </remarks>
        /// <returns> Reservations object </returns>
        /// <response code="200"> Return reservations object </response>
        /// <response code="400"> Item is null </response> 
        /// <response code="401"> Unauthorized </response>
        /// <response code="500"> Internal server error </response>
        [HttpGet("teacher/{id}", Name = "ReservationsByTeachersId")]
        public async Task<IActionResult> GetReservationsByTeachersId(int id)
        {
            try
            {
                Reservations[] ret = await _repository.Reservations.GetReservationsByTeachersIdAsync(id);
                _logger.LogInfo($"Returned reservation by teachers id from database.");
                return Ok(ret);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetReservationsByTeachersId action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }
        /// <summary>
        /// Find reservation by room id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /reservations/room/1
        ///     
        /// </remarks>
        /// <returns> Reservations object </returns>
        /// <response code="200"> Return reservations object </response>
        /// <response code="400"> Item is null </response> 
        /// <response code="401"> Unauthorized </response>
        /// <response code="500"> Internal server error </response>
        [HttpGet("room/{id}", Name = "ReservationsByRoomsId")]
        public async Task<IActionResult> GetReservationsByRoomsId(int id)
        {
            try
            {
                Reservations[] ret = await _repository.Reservations.GetReservationsByRoomsIdAsync(id);
                _logger.LogInfo($"Returned reservation by rooms id from database.");
                return Ok(ret);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetReservationsByRoomsId action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }
        /// <summary>
        /// Find reservation by date
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /reservations/date/2019-09-07
        ///     
        /// </remarks>
        /// <returns> Reservations object </returns>
        /// <response code="200"> Return reservations object </response>
        /// <response code="400"> Item is null </response> 
        /// <response code="401"> Unauthorized </response>
        /// <response code="500"> Internal server error </response>
        [HttpGet("date/{date}", Name = "ReservationsByDate")]
        public async Task<IActionResult> GetReservationsByRoomsId(DateTime date)
        {
            try
            {
                Reservations[] ret = await _repository.Reservations.GetReservationsByDateAsync(date);
                _logger.LogInfo($"Returned reservation by date from database.");
                return Ok(ret);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetReservationsByDate action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }
    }
}
