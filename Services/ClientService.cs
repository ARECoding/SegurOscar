using SegurOsCar.DTOs;
using SegurOsCar.Models;
using SegurOsCar.Repository;
using System.Linq;

namespace SegurOsCar.Services
{
    public class ClientService : ICommonServices<ClientDto, ClientInsertDto, ClientUpdateDto>
    {
        private readonly IRepository<Client> _clientRepository;
        private readonly VehicleService _vehicleService;

        public ClientService(IRepository<Client> clientRepository, VehicleService vehicleService ) 
        { 
            _clientRepository = clientRepository;
            _vehicleService = vehicleService;
        }

        public List<string> Errors => throw new NotImplementedException();

        public async Task Add(ClientInsertDto clientInsertDto)
        {
            
            var newClient = new Client()
            {
                ClientId = clientInsertDto.Id,
                Name = clientInsertDto.Name,
                Email = clientInsertDto.Email,
                Address = clientInsertDto.Address,
            };
            await _clientRepository.Add(newClient);
            await _clientRepository.Save();
            await _vehicleService.AddVehicleList(clientInsertDto.Id, clientInsertDto.Vehicles);
        }

        public async Task<ClientDto?> Delete(string id) 
        {
            var client = await _clientRepository.GetById(id);
            if (client == null)
                return null;
            var vehicles = await _vehicleService.DeleteVehicleList(client.ClientId);
            if (vehicles == null)
                return null;

            var clientDto = new ClientDto
            {
                Id = client.ClientId,
                Name = client.Name,
                Email = client.Email ?? string.Empty,
                Address = client.Address ?? string.Empty,
                Vehicles = vehicles
            };
            _clientRepository.Delete(client);
            await _clientRepository.Save();
            return clientDto;
        }

        public async Task<IEnumerable<ClientDto>> Get()
        {
            var clientList = await _clientRepository.Get();

            var clientDtoList = clientList.Select(async client
                => new ClientDto
                {
                    Id = client.ClientId,
                    Name = client.Name,
                    Email = client.Email ?? string.Empty,
                    Vehicles = (await _vehicleService.GetVehicleListByOwner(client.ClientId)).ToList()
                });
            return [.. clientDtoList.Where(clientElem => clientElem != null).Select(clientElem => clientElem.Result)];
        }

        public async Task<ClientDto?> GetById(string id)
        {
            var client = await _clientRepository.GetById(id);
            if (client == null)
                return null;
            var vehicleList = await _vehicleService.GetVehicleListByOwner(client.ClientId);
            var clientDto = new ClientDto
            {
                Id = client.ClientId,
                Name = client.Name,
                Email = client.Email ?? string.Empty,
                Vehicles = (ICollection<VehicleDto>) await _vehicleService.GetVehicleListByOwner(client.ClientId)
            };
            return clientDto;
        }

        public async Task<ClientDto?> Update(string id, ClientUpdateDto clientToUpdate)
        {
            var client = await _clientRepository.GetById(id);
            if (client == null)
                return null;
            client.Name = clientToUpdate.Name;
            client.Email = clientToUpdate.Email;
            client.Address = clientToUpdate.Address;

            var updatedVehicleList = await _vehicleService.UpdateVehicleList((ICollection<VehicleUpdateDto>)clientToUpdate.Vehicles);
            if(updatedVehicleList == null)
                return null;
            var clientDto = new ClientDto
            {
                Id = client.ClientId,
                Name = client.Name,
                Email = client.Email ?? string.Empty,
                Address = client.Address ?? string.Empty,
                Vehicles = (updatedVehicleList).ToList()
            };
            return clientDto;
        }

        public bool Validate(ClientInsertDto dto)
        {
            throw new NotImplementedException();
        }

        public bool Validate(ClientUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
