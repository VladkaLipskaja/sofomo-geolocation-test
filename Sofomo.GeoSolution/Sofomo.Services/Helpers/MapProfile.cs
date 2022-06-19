using AutoMapper;
using Sofomo.Entities;
using Sofomo.Models;
using Sofomo.Network;

namespace Sofomo.Services
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<GeolocationDto, GeolocationEntity>().ReverseMap();
            CreateMap<GeolocationDto, GeolocationModel>().ReverseMap();
            CreateMap<GeolocationEntity, GeolocationModel>().ReverseMap();
        }
    }
}