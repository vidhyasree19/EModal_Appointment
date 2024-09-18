using TermianlApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TermianlApi.Services
{
    public interface ITerminalService
    {
        Task<IEnumerable<Terminal>> GetAll();
        Task<Terminal> Get(int id);
        Task<Terminal> Create(Terminal terminal);
        Task<bool> Update(int id, Terminal terminal);
        Task<bool> Delete(int id);
        Task<bool> TerminalExists(string name, string gateNo);
    }
}
