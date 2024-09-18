using TruckingCompanyApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TruckingCompanyApi.Services
{
    public interface ITruckingCompanyService
    {
        Task<IEnumerable<TruckingCompany>> GetAll();
        Task<TruckingCompany> Get(int id);
        Task<TruckingCompany> Create(TruckingCompany company);
        Task<bool> Update(int id, TruckingCompany company);
        Task<bool> Delete(int id);
        Task<bool> TruckingCompanyExists(string name);
    }
}
