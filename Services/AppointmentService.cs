using AppointmentApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TruckingCompanyApi.Models;

namespace TruckingCompanyApi.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly TruckService _truckService;
        // private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<AppointmentService> _logger;


        public AppointmentService( ApplicationDbContext context,ITruckService truckService, ILogger<AppointmentService> logger)
        {
           _context = context;
            _truckService = (TruckService?)truckService;
            
            _logger = logger;
        }

        public async Task<IEnumerable<Appointment>> GetAppointments()
        {
            return await _context.Appointments.ToListAsync();
        }

        public async Task<Appointment> GetAppointment(int id)
        {
            return await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
        }


        public async Task<Appointment> CreateAppointment(Appointment appointment)
        {
            // Check if appointment is null
            if (appointment == null)
            {
                throw new ArgumentNullException(nameof(appointment), "Appointment cannot be null.");
            }

            // Ensure the TruckService is available
            if (_truckService == null)
            {
                throw new InvalidOperationException("Truck service is not initialized.");
            }

            // Fetch the truck to validate the relationship
            var truck = await _truckService.GetTruckByIdAsync(appointment.TruckId);
            if (truck == null)
            {
                _logger.LogWarning("Truck with ID {TruckId} not found.", appointment.TruckId);
                throw new InvalidOperationException("Truck not found.");
            }

            // Validate TruckingCompanyId
            if (truck.TruckingCompanyId != appointment.TruckingCompanyId)
            {
                _logger.LogWarning("Truck ID {TruckId} does not belong to Trucking Company ID {TruckingCompanyId}.",
                    appointment.TruckId, appointment.TruckingCompanyId);
                throw new InvalidOperationException("The TruckId does not belong to the specified TruckingCompanyId.");
            }

            // Add the appointment to the database
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return appointment;
        }



        public async Task<bool> UpdateAppointment(int id, Appointment appointment)
        {
            if (id != appointment.Id) return false;

            _context.Entry(appointment).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Appointments.AnyAsync(a => a.Id == id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return false;

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
