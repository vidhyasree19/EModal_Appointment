using Microsoft.EntityFrameworkCore;
using TruckingCompanyApi.Models;
using TermianlApi.Models;

namespace AppointmentApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TruckingCompany> TruckingCompanies { get; set; }
        public DbSet<Terminal> Terminals { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}
