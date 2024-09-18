using System.Collections.Generic;
using System.Threading.Tasks;
using TruckingCompanyApi.Models;

namespace TruckingCompanyApi.Services
{
    public interface ITruckService
    {
        Task<IEnumerable<Truck>> GetAllTrucksAsync();
        Task<Truck> GetTruckByIdAsync(int id);
        Task<Truck> CreateTruckAsync(Truck truck);
        Task UpdateTruckAsync(int id, Truck truck);
        Task DeleteTruckAsync(int id);
        Task<bool> TruckExistsAsync(int id);
    }
}
