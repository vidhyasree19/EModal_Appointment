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
         public DbSet<Truck> Trucks { get; set; }
    public DbSet<Driver> Drivers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
        modelBuilder.Entity<Truck>()
            .HasOne(t => t.TruckingCompany)
            .WithMany(tc => tc.Trucks)
            .HasForeignKey(t => t.TruckingCompanyId);

        modelBuilder.Entity<Driver>()
            .HasOne(d => d.TruckingCompany)
            .WithMany(tc => tc.Drivers)
            .HasForeignKey(d => d.TruckingCompanyId);

        base.OnModelCreating(modelBuilder);
    }

    }
}
