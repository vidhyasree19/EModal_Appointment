using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TruckingCompanyApi.Models;
using TruckingCompanyApi.Services;
using System.Threading.Tasks;

namespace TruckingCompanyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class TruckingCompaniesController : ControllerBase
    {
        private readonly ITruckingCompanyService _truckingCompanyService;
        private readonly ILogger<TruckingCompaniesController> _logger;

        public TruckingCompaniesController(ITruckingCompanyService truckingCompanyService,ILogger<TruckingCompaniesController> logger)
        {
            _truckingCompanyService = truckingCompanyService;
            _logger=logger;
        }

        // GET: api/TruckingCompany
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _truckingCompanyService.GetAll();
            return Ok(companies);
        }

        // GET: api/TruckingCompany/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var company = await _truckingCompanyService.Get(id);
            if (company == null)
                {
                _logger.LogWarning($"Trucking company with ID {id} not found.");
                return NotFound();
            }

            return Ok(company);
        }

        // POST: api/TruckingCompany
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TruckingCompany company)
        {
            if (company == null)
                {
                _logger.LogError("Trucking company object is null.");
                return BadRequest("Trucking company object is null.");
            }

            if (!ModelState.IsValid)
                {
                _logger.LogError("Model state is invalid. Trucking Company");
                return BadRequest(ModelState);
            }

            if (await _truckingCompanyService.TruckingCompanyExists(company.Name))
           {
                _logger.LogWarning($"A TruckingCompany with the name {company.Name} already exists.");
                return Conflict(new { message = "A TruckingCompany with the same name already exists." });
            }

            var createdCompany = await _truckingCompanyService.Create(company);

            return CreatedAtAction(nameof(Get), new { id = createdCompany.Id }, createdCompany);
        }

        // PUT: api/TruckingCompany/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TruckingCompany company)
        {
            if (company == null || company.Id != id)
            {
                _logger.LogError("Trucking company object is null or ID mismatch.");
            var m ="enter correct id";
                return BadRequest(m);
            }
            var updated = await _truckingCompanyService.Update(id, company);
            if (!updated)
               {
                _logger.LogWarning($"Trucking company with ID {id} not found for update.");
                return NotFound();
            }

            return Ok("Trucking company updated successfully.");
        }

        // DELETE: api/TruckingCompany/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _truckingCompanyService.Delete(id);
            if (!deleted)
               if (!deleted)
            {
                _logger.LogWarning($"Trucking company with ID {id} not found for deletion.");
                return NotFound();
            }

             return Ok("Trucking company has been deleted successfully .");

        }
    }
}
