using AutoMapper;
using SegurOsCar.DTOs;
using SegurOsCar.Models;
namespace SegurOsCar.AutoMappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Client, ClientDto>()
                .ForMember(cdto => cdto.Id, c => c.MapFrom(src => src.ClientId));
            CreateMap<ClientDto, Client>()
                .ForMember(c => c.ClientId, cdto => cdto.MapFrom(src => src.Id));
            CreateMap<ClientInsertDto, Client>()
                .ForMember(c => c.ClientId, opt => opt.MapFrom(src => src.Id));

            CreateMap<Car, VehicleDto>();
            CreateMap<Car, VehicleInsertDto>();

            CreateMap<VehicleDto, Car>();

            CreateMap<VehicleInsertDto, Car>();
            CreateMap<VehicleInsertDto, VehicleDto>();

            CreateMap<VehicleUpdateDto, VehicleInsertDto>();
            CreateMap<VehicleUpdateDto, Car>();
            CreateMap<Car, VehicleUpdateDto>();
            

            


        }
    }
}
