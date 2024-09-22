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
    [Authorize(Roles = "Operator,Admin")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly ILogger<AppointmentsController> _logger;

        public AppointmentsController(IAppointmentService appointmentService, ILogger<AppointmentsController> logger)
        {
            _appointmentService = appointmentService;
            _logger = logger;
        }

        [Authorize(Roles = "Operator")]
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

        [Authorize(Roles = "Operator")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointment(int id)
        {
            try
            {
                var appointment = await _appointmentService.GetAppointment(id);
                if (appointment == null)
                    {
                    _logger.LogError( $"Appointment with ID {id} not found");
                    return NotFound();
                }

                return Ok(appointment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving appointment with ID {id}.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
public async Task<IActionResult> CreateAppointment([FromBody] Appointment appointment)
{
    if (!ModelState.IsValid)
    {
        _logger.LogError("Model state is invalid. Appointment");
        return BadRequest(ModelState);
    }

    try
    {
        var createdAppointment = await _appointmentService.CreateAppointment(appointment);
        return CreatedAtAction(nameof(GetAppointment), new { id = createdAppointment.Id }, createdAppointment);
    }
    catch (InvalidOperationException ex)
    {
        _logger.LogError(ex, "Invalid appointment data.");
        return BadRequest(new { message = ex.Message });
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "An error occurred while creating the appointment.");
        return StatusCode(500, "Internal server error.");
    }
}


        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] Appointment appointment)
        {
            if (id != appointment.Id)
               {
                _logger.LogError("Appointment object is null or ID mismatch.");
                return BadRequest();}

            try
            {
                var updated = await _appointmentService.UpdateAppointment(id, appointment);
                if (!updated)
                   {
                    _logger.LogWarning($"Appointmetn with ID {id} not found for update.");
                    return NotFound();
                    }

                 return Ok("Appointment has been successfully updated.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating appointment with ID {id}.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [Authorize(Roles = "Operator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            try
            {
                var deleted = await _appointmentService.DeleteAppointment(id);
                if (!deleted)
                 {

                       _logger.LogWarning($"Appointment with ID {id} not found for deletion.");
                    return NotFound();
                    }


                 return Ok("Appointment has been successfully deleted.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting appointment with ID {id}.");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
