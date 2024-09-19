using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TermianlApi.Models;
using TermianlApi.Services;
using System.Threading.Tasks;

namespace TermianlApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Operator")]
    public class TerminalController : ControllerBase
    {
        private readonly ITerminalService _terminalService;
        private readonly ILogger<TerminalController> _logger;

        public TerminalController(ITerminalService terminalService, ILogger<TerminalController> logger)
        {
            _terminalService = terminalService;
            _logger = logger;
        }


        [HttpGet]
       
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var terminals = await _terminalService.GetAll();
                return Ok(terminals);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving terminals.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var terminal = await _terminalService.Get(id);
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
        public async Task<IActionResult> Create([FromBody] Terminal terminal)
        {
            if (terminal == null)
                return BadRequest("Terminal object is null.");

            if (!ModelState.IsValid)
                return BadRequest("Invalid model object.");

            try
            {
                if (await _terminalService.TerminalExists(terminal.Name, terminal.GateNo))
                    return Conflict("A terminal with the same name or gate number already exists.");

                var createdTerminal = await _terminalService.Create(terminal);
                return CreatedAtAction(nameof(Get), new { id = createdTerminal.Id }, createdTerminal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the terminal.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Terminal terminal)
        {
            if (terminal == null)
                return BadRequest("Terminal object is null.");

            if (terminal.Id != id)
                return BadRequest("Terminal ID mismatch.");

            if (!ModelState.IsValid)
                return BadRequest("Invalid model object.");

            try
            {
                var updated = await _terminalService.Update(id, terminal);
                if (!updated)
                    return NotFound();

                 return Ok("Terminal has been successfully updated.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating terminal with ID {id}.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _terminalService.Delete(id);
                if (!deleted)
                    return NotFound();

                 return Ok("Terminal has been successfully deleted.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting terminal with ID {id}.");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
