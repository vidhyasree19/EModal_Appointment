using AppointmentApi.Data;
using Microsoft.EntityFrameworkCore;
using TruckingCompanyApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TruckingCompanyApi.Services
{
    public class TruckingCompanyService : ITruckingCompanyService
    {
        private readonly ApplicationDbContext _context;

        public TruckingCompanyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TruckingCompany>> GetAll()
        {
            return await _context.TruckingCompanies.Include(tc => tc.Trucks).ToListAsync();
        }

        public async Task<TruckingCompany> Get(int id)
        {
            return await _context.TruckingCompanies
                .Include(tc => tc.Trucks)
                .FirstOrDefaultAsync(tc => tc.Id == id);
        }

        public async Task<TruckingCompany> Create(TruckingCompany company)
        {
            _context.TruckingCompanies.Add(company);
            await _context.SaveChangesAsync();
            return company;
        }

        public async Task<bool> Update(int id, TruckingCompany company)
        {
            var existingCompany = await _context.TruckingCompanies.FindAsync(id);
            if (existingCompany == null) return false;

            // Update existing company details
            existingCompany.WorkType = company.WorkType;
            // Add or update other properties if needed

            _context.TruckingCompanies.Update(existingCompany);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var company = await _context.TruckingCompanies.FindAsync(id);
            if (company == null) return false;

            _context.TruckingCompanies.Remove(company);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> TruckingCompanyExists(string name)
        {
            return await _context.TruckingCompanies.AnyAsync(tc => tc.Name == name);
        }
    }
}
