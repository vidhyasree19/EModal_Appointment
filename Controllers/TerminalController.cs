using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using TermianlApi.Models;
using AppointmentApi.Data;

namespace TermianlApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "operator")]
    public class TerminalController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TerminalController> _logger;

        public TerminalController(ApplicationDbContext context, ILogger<TerminalController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var terminals = _context.Terminals.ToList();
                return Ok(terminals);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving terminals.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var terminal = _context.Terminals.Find(id);
                if (terminal == null)
                    return NotFound();

                return Ok(terminal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving terminal with ID {id}.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Terminal terminal)
        {
            if (terminal == null)
                return BadRequest("Terminal object is null.");

            if (!ModelState.IsValid)
                return BadRequest("Invalid model object.");

            try
            {
                _context.Terminals.Add(terminal);
                _context.SaveChanges();
                return CreatedAtAction(nameof(Get), new { id = terminal.Id }, terminal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the terminal.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Terminal terminal)
        {
           if (terminal == null)
        return BadRequest("Terminal object is null.");

    if (terminal.Id != id)
        return BadRequest("Terminal ID mismatch.");

    if (!ModelState.IsValid)
        return BadRequest("Invalid model object.");
            try
            {
                var existingTerminal = _context.Terminals.Find(id);
                if (existingTerminal == null)
                    return NotFound();

                // Update properties
                existingTerminal.Name = terminal.Name;
                existingTerminal.GateNo = terminal.GateNo;
                existingTerminal.Slots = terminal.Slots;
                existingTerminal.Amount = terminal.Amount;

                _context.Terminals.Update(existingTerminal);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating terminal with ID {id}.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var terminal = _context.Terminals.Find(id);
                if (terminal == null)
                    return NotFound();

                _context.Terminals.Remove(terminal);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting terminal with ID {id}.");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
