using SegurOsCar.DTOs;
using SegurOsCar.Models;
using SegurOsCar.Repository;

namespace SegurOsCar.Services
{
    public class VehicleService : ICommonServices<VehicleDto, VehicleInsertDto, VehicleUpdateDto>
    {
        public List<string> Errors => throw new NotImplementedException();
        private readonly VehicleRepository _vehicleRepository;

        public VehicleService(VehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task Add(VehicleInsertDto vehicleInsertDto)
        {
            var vehicleToSend = new Car
            {
                LicencePlate = vehicleInsertDto.LicencePlate,
                Brand = vehicleInsertDto.Brand,
                Model = vehicleInsertDto.Model,
                ModelYear = vehicleInsertDto.ModelYear,
                Kilometers = vehicleInsertDto.Kilometers,
                ClientId = vehicleInsertDto.ClientId,
            };

            await _vehicleRepository.Add(vehicleToSend);
            await _vehicleRepository.Save();
        }

        public async Task AddVehicleList(string ownerId, ICollection<VehicleDto> vehiclesToAdd)
        { 
            foreach(var vehicleToAdd in vehiclesToAdd)
            {
                
                await Add(new VehicleInsertDto
                {
                    LicencePlate = vehicleToAdd.LicencePlate,
                    ModelYear = vehicleToAdd.ModelYear,
                    Model = vehicleToAdd.Model,
                    Brand = vehicleToAdd.Brand,
                    Kilometers = vehicleToAdd.Kilometers,
                    ClientId = ownerId
                });
            }
        }

        public async Task<VehicleDto> Delete(string vehicleId)
        {
            var vehicle = await _vehicleRepository.GetById(vehicleId);
            if (vehicle == null)
                return null;
            var vehicleDto = new VehicleDto
            {
                LicencePlate = vehicle.LicencePlate,
                ModelYear = vehicle.ModelYear,
                Model = vehicle.Model,
                Brand = vehicle.Brand,
                Kilometers = vehicle.Kilometers,
            };
            _vehicleRepository.Delete(vehicle);
            await _vehicleRepository.Save();
            return vehicleDto;
        }

        public async Task<ICollection<VehicleDto>> DeleteVehicleList(string ownerId)
        {
            var vehicleList = await _vehicleRepository.GetVehicleListByOwner(ownerId);
            ICollection<VehicleDto> vehicleDtoList = new List<VehicleDto>();
            foreach (var vehicleToDelete in vehicleList)
            {
                vehicleDtoList.Add(await Delete(vehicleToDelete.LicencePlate));
            }
            return vehicleDtoList;
        }


        public async Task<IEnumerable<VehicleDto>> Get()
        {
            var vehicle = await _vehicleRepository.Get();

            return vehicle.Select(v => new VehicleDto
            {
                LicencePlate = v.LicencePlate,
                ModelYear = v.ModelYear,
                Model = v.Model,
                Brand = v.Brand,
                Kilometers = v.Kilometers
            });
        }

        public async Task<VehicleDto?> GetById(string vehicleId)
        {
            var vehicle = await _vehicleRepository.GetById(vehicleId);

            if (vehicle == null)
                return null;

            var vehicleDto = new VehicleDto
            {
                LicencePlate = vehicle.LicencePlate,
                ModelYear = vehicle.ModelYear,
                Model = vehicle.Model,
                Brand = vehicle.Brand,
                Kilometers = vehicle.Kilometers
            };
            return vehicleDto;
        }

        public async Task<IEnumerable<VehicleDto>> GetVehicleListByOwner(string owner)
        {
            var vehicles = await _vehicleRepository.GetVehicleListByOwner(owner);
            if (vehicles == null)
                return null;
            var vehicleDtos = vehicles.Select(v => new VehicleDto
            {
                LicencePlate = v.LicencePlate,
                ModelYear = v.ModelYear,
                Model = v.Model,
                Brand = v.Brand,
                Kilometers = v.Kilometers
            }).ToList();
            return vehicleDtos;
        }

        public async Task<VehicleDto?> Update(string vehicleId, VehicleUpdateDto vehicleUpdateDto)
        {
            var vehicle = await _vehicleRepository.GetById(vehicleId);
            if (vehicle == null)
                return null;

            vehicle.ModelYear = vehicleUpdateDto.ModelYear;
            vehicle.Model = vehicleUpdateDto.Model ?? vehicle.Model;
            vehicle.Brand = vehicleUpdateDto.Brand ?? vehicle.Brand;
            vehicle.Kilometers = vehicleUpdateDto.Kilometers;
            _vehicleRepository.Update(vehicle);
            await _vehicleRepository.Save();

            var vehicleUpdatedDto = new VehicleDto
            {
                LicencePlate = vehicle.LicencePlate,
                ModelYear = vehicle.ModelYear,
                Model = vehicle.Model,
                Brand = vehicle.Brand,
                Kilometers = vehicle.Kilometers
            };
            return vehicleUpdatedDto;
        }

        public async Task<IEnumerable<VehicleDto>> UpdateVehicleList(ICollection<VehicleUpdateDto> vehicleListToUpdate)
        {
            ICollection<VehicleDto> vehicleList = new List<VehicleDto>();
            foreach (var vehicleToUpdate in vehicleListToUpdate)
            {
                vehicleList.Add(await Update(vehicleToUpdate.LicencePlate, vehicleToUpdate));
            }
            return vehicleList;
        }

        public bool Validate(VehicleInsertDto vehicleToValidate)
        {
            throw new NotImplementedException();
        }

        public bool Validate(VehicleUpdateDto vehicleToValidate)
        {
            throw new NotImplementedException();
        }
    }
}
