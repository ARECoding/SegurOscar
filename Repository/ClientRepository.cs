using Microsoft.EntityFrameworkCore;
using SegurOsCar.Models;
namespace SegurOsCar.Repository
{
    public class ClientRepository : IRepository<Client>
    {
        private readonly SecureContext _secureContext;

        public ClientRepository(SecureContext secureContext)
        {
            _secureContext = secureContext;         
        }
        public async Task Add(Client client)
            => await _secureContext.Clients.AddAsync(client);

        public void Delete(Client client)
            => _secureContext.Clients.Remove(client);

        public async Task<IEnumerable<Client>> Get() 
            => await _secureContext.Clients.ToListAsync();

        public async Task<Client?> GetById(string id) 
            => await _secureContext.Clients.FirstOrDefaultAsync(client => client.ClientId == id);


        public async Task Save()
            => await _secureContext.SaveChangesAsync();

        public void Update(Client client)
        {
            _secureContext.Clients.Attach(client);
            _secureContext.Entry(client).State = EntityState.Modified;
        }
    }
}
