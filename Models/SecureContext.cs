using Microsoft.EntityFrameworkCore;
using SegurOsCar.Models;


namespace SegurOsCar.Models
{
    public class SecureContext : DbContext
    {
        public SecureContext(DbContextOptions<SecureContext> options) : base(options)
        {
            
        }


        public DbSet<Client> Clients { get; set; }
        public DbSet<Car> Cars { get; set; }

        public DbSet<Motorbike> Motorbikes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Client>(c =>
            {
                c.HasKey(cId => cId.Id);
            });

            modelBuilder.Entity<Vehicle>(car =>
            {
                car.HasKey(cr => cr.LicencePlate);
            });

            modelBuilder.Entity<Client>(clientEnt =>
            {
                clientEnt.HasKey(c => c.Id);
                clientEnt.HasMany(c => c.VehicleList);
            });
            
        }
    }
}
