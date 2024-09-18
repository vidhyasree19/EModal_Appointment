using AppointmentApi.Data;
using Microsoft.EntityFrameworkCore;
using TermianlApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TermianlApi.Services
{
    public class TerminalService : ITerminalService
    {
        private readonly ApplicationDbContext _context;

        public TerminalService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Terminal>> GetAll()
        {
            return await _context.Terminals.ToListAsync();
        }

        public async Task<Terminal> Get(int id)
        {
            return await _context.Terminals.FindAsync(id);
        }

        public async Task<Terminal> Create(Terminal terminal)
        {
            _context.Terminals.Add(terminal);
            await _context.SaveChangesAsync();
            return terminal;
        }

        public async Task<bool> Update(int id, Terminal terminal)
        {
            var existingTerminal = await _context.Terminals.FindAsync(id);
            if (existingTerminal == null) return false;

            // Update terminal details
            existingTerminal.Slots = terminal.Slots;
            existingTerminal.Amount = terminal.Amount;

            _context.Terminals.Update(existingTerminal);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var terminal = await _context.Terminals.FindAsync(id);
            if (terminal == null) return false;

            _context.Terminals.Remove(terminal);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> TerminalExists(string name, string gateNo)
        {
            return await _context.Terminals.AnyAsync(t => t.Name == name || t.GateNo == gateNo);
        }
    }
}
