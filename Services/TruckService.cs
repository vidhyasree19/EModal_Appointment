using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppointmentApi.Data;
using TruckingCompanyApi.Models;

namespace TruckingCompanyApi.Services
{
    public class TruckService : ITruckService
    {
        private readonly ApplicationDbContext _context;

        public TruckService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Truck>> GetAllTrucksAsync()
        {
            return await _context.Trucks.ToListAsync();
        }

        public async Task<Truck> GetTruckByIdAsync(int id)
        {
            return await _context.Trucks.FindAsync(id);
        }

        public async Task<Truck> CreateTruckAsync(Truck truck)
        {
            _context.Trucks.Add(truck);
            await _context.SaveChangesAsync();
            return truck;
        }

        public async Task UpdateTruckAsync(int id, Truck truck)
        {
            _context.Entry(truck).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTruckAsync(int id)
        {
            var truck = await _context.Trucks.FindAsync(id);
            if (truck != null)
            {
                _context.Trucks.Remove(truck);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> TruckExistsAsync(int id)
        {
            return await _context.Trucks.AnyAsync(t => t.Id == id);
        }
    }
}
