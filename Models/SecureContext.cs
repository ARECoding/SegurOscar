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
            modelBuilder.Entity<Client>(c =>
            {
                c.HasKey(cId => cId.ClientId);
            });

            modelBuilder.Entity<Vehicle>(car =>
            {
                car.HasKey(cr => cr.LicencePlate);
                car.HasOne<Client>()
                    .WithMany()
                    .HasForeignKey(cr => cr.ClientId)
                    .HasConstraintName("FK_Car_ClientId");
            });
        }
    }
}
