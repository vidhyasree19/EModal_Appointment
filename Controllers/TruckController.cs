using AppointmentApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckingCompanyApi.Models;

namespace TruckingCompanyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TruckController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TruckController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Truck
        [HttpGet]
        public async Task<IActionResult> GetTrucks()
        {
            var trucks = await _context.Trucks
                .Include(t => t.TruckingCompany)  // Eagerly load TruckingCompany
                .ToListAsync();

            return Ok(trucks);
        }

        // GET: api/Truck/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Truck>> GetTruck(int id)
        {
            var truck = await _context.Trucks.FindAsync(id);

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
            _context.Trucks.Add(truck);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTruck), new { id = truck.Id }, truck);
        }

        // PUT: api/Truck/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTruck(int id, Truck truck)
        {
            if (id != truck.Id)
            {
                return BadRequest();
            }

            _context.Entry(truck).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TruckExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Truck/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTruck(int id)
        {
            var truck = await _context.Trucks.FindAsync(id);
            if (truck == null)
            {
                return NotFound();
            }

            _context.Trucks.Remove(truck);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TruckExists(int id)
        {
            return _context.Trucks.Any(t => t.Id == id);
        }
    }
}
