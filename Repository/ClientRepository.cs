using Microsoft.EntityFrameworkCore;
using SegurOsCar.Models;
using SegurOsCar.DTOs;
namespace SegurOsCar.Repository
{
    public class ClientRepository : IRepository<Client>
    {
        private SecureContext _secureContext;

        public ClientRepository(SecureContext secureContext)
        {
            _secureContext = secureContext;         
        }
        public async Task Add(Client entity)
            => await _secureContext.Clients.AddAsync(entity);

        public async void Delete(Client entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Client>> Get() 
            => await _secureContext.Clients.ToListAsync();

        public async Task<Client> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Client> Search(Func<Client, bool> filter)
        {
            throw new NotImplementedException();
        }

        public void Update(Client entity)
        {
            throw new NotImplementedException();
        }
    }
}
