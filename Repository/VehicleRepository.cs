using Microsoft.EntityFrameworkCore;
using SegurOsCar.Models;

namespace SegurOsCar.Repository
{
    public class VehicleRepository : IRepository<Vehicle>
    {
        private readonly SecureContext _secureContext;

        public VehicleRepository (SecureContext secureContext)
        {
            _secureContext = secureContext;
        }
        public async Task Add(Vehicle vehicle)
            => await _secureContext.Cars.AddAsync((Car)vehicle);

        public void Delete(Vehicle vehicle)
            => _secureContext.Cars.Remove((Car)vehicle);

        public async Task<IEnumerable<Vehicle>> Get()
            => await _secureContext.Cars.ToListAsync();

        public async Task<Vehicle?> GetById(string id)
            => await _secureContext.Cars.FirstOrDefaultAsync(car => car.LicencePlate == id);

        public async Task<IEnumerable<Vehicle>> GetVehicleListByOwner(string owner)
            => await _secureContext.Cars.Where(car => car.ClientId == owner).ToListAsync();

        public async Task Save()
            => await _secureContext.SaveChangesAsync();


        public void Update(Vehicle vehicle)
        {
            _secureContext.Cars.Attach((Car)vehicle);
            _secureContext.Entry(vehicle).State = EntityState.Modified;
        }
    }
}
