using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TruckingCompanyApi.Models;
using TruckingCompanyApi.Services;
using Microsoft.AspNetCore.Authorization;
namespace TruckingCompanyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class TrucksController : ControllerBase
    {
        private readonly ITruckService _truckService;
         private readonly ILogger<TrucksController> _logger;

        public TrucksController(ITruckService truckService,ILogger<TrucksController> logger)
        {
            _truckService = truckService;
            _logger = logger;
        }

        // GET: api/Truck
        [HttpGet]
        public async Task<IActionResult> GetTrucks()
        {
            var trucks = await _truckService.GetAllTrucksAsync();
            return Ok(trucks);
        }

        // GET: api/Truck/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Truck>> GetTruck(int id)
        {
            var truck = await _truckService.GetTruckByIdAsync(id);
            if (truck == null)
            {
                _logger.LogWarning($"Truck with ID {id} not found.");
                return NotFound();
            }

            return truck;
        }

        // POST: api/Truck
        [HttpPost]
        public async Task<ActionResult<Truck>> CreateTruck(Truck truck)
        {
             if (truck == null)
            {
                _logger.LogError("Truck object is null. check all the fields of truck");
                return BadRequest("Truck object is null.");
            }
            var createdTruck = await _truckService.CreateTruckAsync(truck);
            return CreatedAtAction(nameof(GetTruck), new { id = createdTruck.Id }, createdTruck);
        }

        // PUT: api/Truck/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTruck(int id, Truck truck)
        {
            if (id != truck.Id||truck==null)
            {
                  _logger.LogError("Tried for updating truck details without details,  truck object null or truck ID mismatch");
                return BadRequest("Truck object is null.Try with correct ID");
            }

            await _truckService.UpdateTruckAsync(id, truck);
            return Ok("Truck has been successfully updated.");
        }

        // DELETE: api/Truck/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTruck(int id)
        {
            var truck = await _truckService.GetTruckByIdAsync(id);
            if (truck == null)
            {
                _logger.LogWarning($" tried Truck with ID {id} not found for deletion.");
                return NotFound();
            }

            await _truckService.DeleteTruckAsync(id);
             return Ok("Truck has been successfully deleted.");
        }
    }
}
