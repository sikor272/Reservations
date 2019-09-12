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
    [Route("api/rooms")]
    [ApiController]
    
    public class RoomsController : Controller
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private Bcrypt hash;

        public RoomsController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
            hash = new Bcrypt(_repository);
        }
        /// <summary>
        /// Create new room
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST /rooms 
        /// </remarks>
        /// <response code="201"> Room successfully required </response>
        /// <response code="400"> Bad request </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="402"> Name is use </response>
        /// <response code="500"> Internal server error </response>
        [HttpPost(Name = "CreateRoom")]
        [Authorize]
        public async Task<IActionResult> CreateRoom([FromBody]Rooms room)
        {
            try
            {
                if (room == null)
                {
                    _logger.LogError("Rooms object sent from client is null");
                    return BadRequest("Rooms object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid room object sent from client");
                    return BadRequest("Invalid model object");
                }
                Rooms test = await _repository.Rooms.GetRoomsByNameAsync(room.Name);
                if (test != null && test.Name == room.Name)
                {
                    _logger.LogError("Name of room object sent from client is use");
                    return BadRequest("Name is in use");
                }
                await _repository.Rooms.CreateRoomsAsync(room);
                _repository.Save();

                // return 201 status
                return CreatedAtRoute("CreateRoom", new { id = room.Id }, room.Name);
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong inside CreateRoom action: {e.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Delete room
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     DELETE /rooms/1
        /// </remarks>
        /// <response code="201"> Room successfully required </response>
        /// <response code="404"> Not found </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="500"> Internal server error </response>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteRooms(int id)
        {
            try
            {
                var room = await _repository.Rooms.GetRoomsByIdAsync(id);
                if (room == null)
                {
                    _logger.LogError($"Room with id {id}, hasn't been found in database");
                    return NotFound();
                }

                await _repository.Rooms.DeleteRoomsAsync(room);
                _repository.Save();

                _logger.LogInfo($"Room with id {id} deleted");
                return Ok("Room removed successfully");
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong inside DeleteRooms action: {e.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Update room
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /rooms/1
        ///     
        /// </remarks>
        /// <response code="200"> Room successfully modified </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="404"> Not found </response> 
        /// <response code="500"> Internal server error </response>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> ModifyRooms(int id, [FromBody]Rooms room)
        {
            try
            {
                Rooms target = await _repository.Rooms.GetRoomsByIdAsync(id);
                if (target == null)
                {
                    return NotFound();
                }
                target.Name = room.Name;
                Rooms test = await _repository.Rooms.GetRoomsByNameAsync(room.Name);
                if (test != null && test.Name == room.Name)
                {
                    _logger.LogError("Name of room object sent from client is use");
                    return BadRequest("Name is in use");
                }
                await _repository.Rooms.ModifyRoomsAsync(target);
                return Ok("Room successfully modified");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Return all rooms in database
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /rooms
        ///     
        /// </remarks>
        /// <returns> Array of rooms </returns>
        /// <response code="200"> Return all rooms </response>
        /// <response code="400"> Item is null</response> 
        /// <response code="401"> Unauthorized </response>
        /// <response code="500"> Internal server error </response>
        [HttpGet]
        public async Task<IActionResult> GetAllRooms()
        {
            try
            {
                var rooms = await _repository.Rooms.GetAllRoomsAsync();
                _logger.LogInfo($"Returned all rooms from database.");

                return Ok(rooms);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllRooms action: {ex.Message}");
                return NotFound();
                // return StatusCode(500, "Internal server error" + ex);
            }
        }
        /// <summary>
        /// Find room by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /rooms/1
        ///     
        /// </remarks>
        /// <returns> Rooms object </returns>
        /// <response code="200"> Return rooms object </response>
        /// <response code="400"> Item is null </response> 
        /// <response code="401"> Unauthorized </response>
        /// <response code="500"> Internal server error </response>
        [HttpGet("{id}", Name = "RoomsById")]
        public async Task<IActionResult> GetRoomsById(int id)
        {
            try
            {
                Rooms ret = await _repository.Rooms.GetRoomsByIdAsync(id);
                _logger.LogInfo($"Returned room by id from database.");
                return Ok(ret);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetRoomsById action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }
    }
}
