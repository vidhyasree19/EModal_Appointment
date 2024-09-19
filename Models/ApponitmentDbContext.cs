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
        public DbSet<User> Users { get; set; } // Add this line for User entity

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationships as necessary

            // Uncomment and adjust these if needed
            // modelBuilder.Entity<Truck>()
            //     .HasOne(t => t.TruckingCompany)
            //     .WithMany(tc => tc.Trucks)
            //     .HasForeignKey(t => t.TruckingCompanyId);

            // modelBuilder.Entity<Driver>()
            //     .HasOne(d => d.TruckingCompany)
            //     .WithMany(tc => tc.Drivers)
            //     .HasForeignKey(d => d.TruckingCompanyId);

            // modelBuilder.Entity<Appointment>()
            //     .HasOne(a => a.Truck)
            //     .WithMany()
            //     .HasForeignKey(a => a.TruckId)
            //     .OnDelete(DeleteBehavior.Restrict);
        }
    }

    // Add User model to represent users in the database
   
}
