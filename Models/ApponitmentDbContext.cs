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
    // public DbSet<Driver> Drivers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
        // modelBuilder.Entity<Truck>()
        //     .HasOne(t => t.TruckingCompany)
        //     .WithMany(tc => tc.Trucks)
        //     .HasForeignKey(t => t.TruckingCompanyId);

        // modelBuilder.Entity<Driver>()
        //     .HasOne(d => d.TruckingCompany)
        //     .WithMany(tc => tc.Drivers)
        //     .HasForeignKey(d => d.TruckingCompanyId);
    //     modelBuilder.Entity<Appointment>()
    //     .HasOne(a => a.Truck)
    //     .WithMany()
    //     .HasForeignKey(a => a.TruckId)
    //     .OnDelete(DeleteBehavior.Restrict);

    // // Appointment -> TruckingCompany relationship (Restrict cascading)
    // modelBuilder.Entity<Appointment>()
    //     .HasOne(a => a.TruckingCompany)
    //     .WithMany()
    //     .HasForeignKey(a => a.TruckingCompanyId)
    //     .OnDelete(DeleteBehavior.Restrict);

    // // Appointment -> Terminal relationship (Restrict cascading)
    // modelBuilder.Entity<Appointment>()
    //     .HasOne(a => a.Terminal)
    //     .WithMany()
    //     .HasForeignKey(a => a.TerminalId)
    //     .OnDelete(DeleteBehavior.Restrict);
    //     base.OnModelCreating(modelBuilder);
    }

    }
}
