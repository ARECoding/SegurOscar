using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SegurOsCar.DTOs;
using SegurOsCar.Models;
using SegurOsCar.Repository;

namespace SegurOsCar.Services
{
    public class VehicleService : ICommonServices<VehicleDto, VehicleInsertDto, VehicleUpdateDto>
    {
        public List<string> Errors => throw new NotImplementedException();
        private readonly VehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;
        public VehicleService(VehicleRepository vehicleRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task Add(VehicleInsertDto vehicleInsertDto)
        {
            var vehicleToSend = _mapper.Map<Car>(vehicleInsertDto);


            await _vehicleRepository.Add(vehicleToSend);
            await _vehicleRepository.Save();
        }

        public async Task AddVehicleList(string ownerId, ICollection<VehicleDto> vehiclesToAdd)
        { 
            foreach(var vehicleToAdd in vehiclesToAdd)
            {
                
                await Add(
                    //_mapper.Map<VehicleInsertDto>(vehicleToAdd)
                    new VehicleInsertDto
                    {
                        LicencePlate = vehicleToAdd.LicencePlate,
                        ModelYear = vehicleToAdd.ModelYear,
                        Model = vehicleToAdd.Model,
                        Brand = vehicleToAdd.Brand,
                        Kilometers = vehicleToAdd.Kilometers,
                        ClientId = ownerId
                    }
                    );
            }
        }

        public async Task<VehicleDto> Delete(string vehicleId)
        {
            var vehicle = await _vehicleRepository.GetById(vehicleId);
            if (vehicle == null)
                return null;
            var vehicleDto = _mapper.Map<VehicleDto>(vehicle);
           
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

            return vehicle.Select(v =>  _mapper.Map<VehicleDto>(vehicle));
 
        }

        public async Task<VehicleDto?> GetById(string vehicleId)
        {
            var vehicle = await _vehicleRepository.GetById(vehicleId);

            if (vehicle == null)
                return null;

            var vehicleDto = _mapper.Map<VehicleDto>(vehicleId);    

            return vehicleDto;
        }

        public async Task<IEnumerable<VehicleDto>> GetVehicleListByOwner(string owner)
        {
            var vehicles = await _vehicleRepository.GetVehicleListByOwner(owner);
            if (vehicles == null)
                return null;
            var vehicleDtos = vehicles.Select(v => _mapper.Map<VehicleDto>(v)).ToList();

            return vehicleDtos;
        }

        public async Task<VehicleDto?> Update(string vehicleId, VehicleUpdateDto vehicleUpdateDto)
        {
            var vehicle = await _vehicleRepository.GetById(vehicleId);
            if (vehicle == null)
                return null;
            //vehicle = _mapper.Map<Car>(vehicleUpdateDto);
            vehicle.ModelYear = vehicleUpdateDto.ModelYear;
            vehicle.Model = vehicleUpdateDto.Model ?? vehicle.Model;
            vehicle.Brand = vehicleUpdateDto.Brand ?? vehicle.Brand;
            vehicle.Kilometers = vehicleUpdateDto.Kilometers;

            _vehicleRepository.Update(vehicle);
            await _vehicleRepository.Save();

            var vehicleUpdatedDto = _mapper.Map<VehicleDto>(vehicle);

            return vehicleUpdatedDto;
        }

        public async Task<IEnumerable<VehicleDto>> UpdateVehicleList(ICollection<VehicleUpdateDto> vehicleListToUpdate, string owner = null)
        {
            ICollection<VehicleDto> vehicleList = new List<VehicleDto>();
            foreach (var vehicleToUpdate in vehicleListToUpdate)
            {
                var a = await Update(vehicleToUpdate.LicencePlate, vehicleToUpdate);
                if (a != null)
                    vehicleList.Add(await Update(vehicleToUpdate.LicencePlate, vehicleToUpdate));
                else
                {
                    var vehicleToAdd = _mapper.Map<VehicleInsertDto>(vehicleToUpdate);
                    vehicleToAdd.ClientId = owner;
                    await Add(vehicleToAdd);
                    vehicleList.Add(_mapper.Map<VehicleDto>(vehicleToAdd));
                }
                    
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
