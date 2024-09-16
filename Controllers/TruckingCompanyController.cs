using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TruckingCompanyApi.Models;
using AppointmentApi.Data;
 
namespace TruckingCompanyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class TruckingCompanyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
 
        public TruckingCompanyController(ApplicationDbContext context)
        {
            _context = context;
        }
 
        // GET: api/TruckingCompany
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _context.TruckingCompanies
              .Include(tc => tc.Trucks) // Optionally include related trucks if needed
                .ToListAsync();
            return Ok(companies);
        }
 
        // GET: api/TruckingCompany/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var company = await _context.TruckingCompanies
              .Include(tc => tc.Trucks) // Optionally include related trucks if needed
                .FirstOrDefaultAsync(tc => tc.Id == id);
 
            if (company == null)
                return NotFound();
 
            return Ok(company);
        }
 
        // POST: api/TruckingCompany
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TruckingCompany company)
        {
            if (company == null)
                return BadRequest("TruckingCompany cannot be null.");
 
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
 
            _context.TruckingCompanies.Add(company);
            await _context.SaveChangesAsync();
 
            return CreatedAtAction(nameof(Get), new { id = company.Id }, company);
        }
 
        // PUT: api/TruckingCompany/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TruckingCompany company)
        {
            if (company == null || company.Id != id)
                return BadRequest();
 
            var existingCompany = await _context.TruckingCompanies.FindAsync(id);
            if (existingCompany == null)
                return NotFound();
 
            // Update existing company details
            existingCompany.Name = company.Name;
            existingCompany.WorkType = company.WorkType;
            // Add or update other properties if needed
 
            _context.TruckingCompanies.Update(existingCompany);
            await _context.SaveChangesAsync();
 
            return NoContent();
        }
 
        // DELETE: api/TruckingCompany/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var company = await _context.TruckingCompanies.FindAsync(id);
            if (company == null)
                return NotFound();
 
            _context.TruckingCompanies.Remove(company);
            await _context.SaveChangesAsync();
 
            return NoContent();
        }
    }
}