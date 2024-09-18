using TruckingCompanyApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TruckingCompanyApi.Services
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAppointments();
        Task<Appointment> GetAppointment(int id);
        Task<Appointment> CreateAppointment(Appointment appointment);
        Task<bool> UpdateAppointment(int id, Appointment appointment);
        Task<bool> DeleteAppointment(int id);
    }
}
