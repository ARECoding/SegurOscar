using SegurOsCar.DTOs;
using SegurOsCar.Models;
using SegurOsCar.Repository;

namespace SegurOsCar.Services
{
    public class ClientService : ICommonServices<ClientDto, ClientInsertDto, ClientUpdateDto>
    {
        private readonly IRepository<Client> _clientRepository;

        public ClientService (IRepository<Client> clientRepository)
        {
            _clientRepository = clientRepository;
        }   

        public List<string> Errors => throw new NotImplementedException();

        public async Task Add(ClientInsertDto insertDto)
        {
            var newClient = new Client(insertDto.Vehicles)
            {
                Id = insertDto.Id,
                Name = insertDto.Name,
                Email = insertDto.Email,
                Address = insertDto.Address,
                VehicleList = insertDto.Vehicles.Select(vehicle => new Car
                {
                    LicencePlate = vehicle.LicencePlate,
                    ModelYear = vehicle.ModelYear,
                    Brand = vehicle.Brand,
                    Model = vehicle.Model,
                    Kilometers = vehicle.Kilometers
                }).ToList<Vehicle>()
            };
            await _clientRepository.Add(newClient);
            await _clientRepository.Save();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ClientDto>> Get()
        {
            var clientList = await _clientRepository.Get();
            var clientDto = clientList.Select(client
                => new ClientDto
                {
                    Id = client.Id,
                    Name = client.Name,
                    Email = client.Email,
                    Vehicles = client.VehicleList?.Select(
                        vehicle =>
                        new VehicleDto
                        {
                            LicencePlate = vehicle.LicencePlate,
                            ModelYear = vehicle.ModelYear,
                            Brand = vehicle.Brand,
                            Model = vehicle.Model,
                            Kilometers = vehicle.Kilometers
                        }).ToList() ?? new List<VehicleDto>()

                });
            return clientDto;
        }

        public async Task<ClientDto> GetById(int id)
        {
            var client = await _clientRepository.GetById(id);
            if (client == null)
            {
                return null;
            }
            var clientDto = new ClientDto
            {
                Id = client.Id,
                Name = client.Name,
                Email = client.Email,
                Vehicles = client.VehicleList?.Select(
                    vehicle =>
                    new VehicleDto
                    {
                        LicencePlate = vehicle.LicencePlate,
                        ModelYear = vehicle.ModelYear,
                        Brand = vehicle.Brand,
                        Model = vehicle.Model,
                        Kilometers = vehicle.Kilometers
                    }).ToList() ?? new List<VehicleDto>()
            };
            return clientDto;
        }

        public Task Update(int id, ClientUpdateDto updateDto)
        {
            throw new NotImplementedException();
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
