using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TruckingCompanyApi.Models;
using TruckingCompanyApi.Services;

namespace TruckingCompanyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TruckController : ControllerBase
    {
        private readonly ITruckService _truckService;

        public TruckController(ITruckService truckService)
        {
            _truckService = truckService;
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
                return NotFound();
            }

            return truck;
        }

        // POST: api/Truck
        [HttpPost]
        public async Task<ActionResult<Truck>> CreateTruck(Truck truck)
        {
            var createdTruck = await _truckService.CreateTruckAsync(truck);
            return CreatedAtAction(nameof(GetTruck), new { id = createdTruck.Id }, createdTruck);
        }

        // PUT: api/Truck/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTruck(int id, Truck truck)
        {
            if (id != truck.Id)
            {
                return BadRequest();
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
                return NotFound();
            }

            await _truckService.DeleteTruckAsync(id);
             return Ok("Truck has been successfully deleted.");
        }
    }
}
