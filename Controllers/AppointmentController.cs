using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TruckingCompanyApi.Models;
using TruckingCompanyApi.Services;

namespace TruckingCompanyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "operator,admin")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly ILogger<AppointmentController> _logger;

        public AppointmentController(IAppointmentService appointmentService, ILogger<AppointmentController> logger)
        {
            _appointmentService = appointmentService;
            _logger = logger;
        }

        [Authorize(Roles = "operator")]
        [HttpGet]
        public async Task<IActionResult> GetAppointments()
        {
            try
            {
                var appointments = await _appointmentService.GetAppointments();
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving appointments.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [Authorize(Roles = "operator")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointment(int id)
        {
            try
            {
                var appointment = await _appointmentService.GetAppointment(id);
                if (appointment == null)
                    return NotFound();

                return Ok(appointment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving appointment with ID {id}.");
                return StatusCode(500, "Internal server error.");
            }
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] Appointment appointment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdAppointment = await _appointmentService.CreateAppointment(appointment);
                return CreatedAtAction(nameof(GetAppointment), new { id = createdAppointment.Id }, createdAppointment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the appointment.");
                return StatusCode(500, "Internal server error.");
            }
        }

        //[Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] Appointment appointment)
        {
            if (id != appointment.Id)
                return BadRequest();

            try
            {
                var updated = await _appointmentService.UpdateAppointment(id, appointment);
                if (!updated)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating appointment with ID {id}.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [Authorize(Roles = "operator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            try
            {
                var deleted = await _appointmentService.DeleteAppointment(id);
                if (!deleted)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting appointment with ID {id}.");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
