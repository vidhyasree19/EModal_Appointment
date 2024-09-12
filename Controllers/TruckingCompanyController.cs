using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TruckingCompanyApi.Models;
using TruckingCompanyApi.Data;
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

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.TruckingCompanies.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var company = _context.TruckingCompanies.Find(id);
            if (company == null)
                return NotFound();

            return Ok(company);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TruckingCompany company)
        {
            if (company == null)
                return BadRequest();

            _context.TruckingCompanies.Add(company);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = company.Id }, company);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TruckingCompany company)
        {
            if (company == null || company.Id != id)
                return BadRequest();

            var existingCompany = _context.TruckingCompanies.Find(id);
            if (existingCompany == null)
                return NotFound();

            existingCompany.TruckingCompany_Name = company.TruckingCompany_Name;
            existingCompany.Truck_No = company.Truck_No;
            existingCompany.Driver_Name = company.Driver_Name;
            existingCompany.Chassis = company.Chassis;
            existingCompany.Container_Size = company.Container_Size;

            _context.TruckingCompanies.Update(existingCompany);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var company = _context.TruckingCompanies.Find(id);
            if (company == null)
                return NotFound();

            _context.TruckingCompanies.Remove(company);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
