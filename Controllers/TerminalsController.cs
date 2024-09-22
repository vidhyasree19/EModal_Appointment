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
    public class TerminalsController : ControllerBase
    {
        private readonly ITerminalService _terminalService;
        private readonly ILogger<TerminalsController> _logger;

        public TerminalsController(ITerminalService terminalService, ILogger<TerminalsController> logger)
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
                _logger.LogError(ex, "An error occurred while retrieving terminals.please check user credentials");
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
            {
                _logger.LogWarning($"Terminal with ID {id} not found.");
                return NotFound();
            }


                return Ok(terminal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving terminal with ID {id}.please ccheck crdentials");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Terminal terminal)
        {
            if (terminal == null)
             {
                _logger.LogError("Terminal object is null.");
                return BadRequest("Terminal object is null.");
            }

             if (!ModelState.IsValid)
            {
                _logger.LogError("Model state is invalid for Terminal.");
                return BadRequest(ModelState);
            }

            
                if (await _terminalService.TerminalExists(terminal.Name, terminal.GateNo))
               {
                _logger.LogWarning("A terminal with the same name or gate number already exists.");
                return Conflict("A terminal with the same name or gate number already exists.");
            }

                var createdTerminal = await _terminalService.Create(terminal);
                return CreatedAtAction(nameof(Get), new { id = createdTerminal.Id }, createdTerminal);
            
           
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Terminal terminal)
        {
            if (terminal == null)
                {
                _logger.LogError("Terminal object is null while updating.");
                return BadRequest("Terminal object is null.");
            }

            if (terminal.Id != id)
                 {
                _logger.LogError("Terminal ID mismatch.");
                return BadRequest("Terminal ID mismatch.");
            }

            if (!ModelState.IsValid)
                 {
                _logger.LogError("Invalid model object for terminal.");
                return BadRequest("Invalid model object.");
            }

            try
            {
                var updated = await _terminalService.Update(id, terminal);
                if (!updated)
                 {
                _logger.LogWarning($"Terminal with ID {id} not found for update.");
                return NotFound();
            }

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
                 {
                _logger.LogWarning($"Terminal with ID {id} not found for deletion.");
                return NotFound();
            }

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
