using System.Text.Json.Serialization;
using AutoMapper;
using Sofomo.Entities;
using Sofomo.Models;
using Sofomo.Network;

namespace Sofomo.Services
{
    public class GeolocationService : IGeolocationService
    {
        private readonly IMapper _mapper;
        private readonly IGeolocationHelper _geolocationHelper;
        private readonly IRepository<GeolocationEntity> _geolocationRepository;

        public GeolocationService(IMapper mapper, IRepository<GeolocationEntity> geolocationRepository,
            IGeolocationHelper geolocationHelper)
        {
            _mapper = mapper;
            _geolocationHelper = geolocationHelper;
            _geolocationRepository = geolocationRepository;
        }

        public async Task<GeolocationDto> GetGeolocationAsync(string address)
        {
            var ip = await _geolocationHelper.GetIPFromAddressAsync(address);

            var entity = await _geolocationRepository.GetAsync(ip);
            if (entity == null)
            {
                throw new GeolocationException(ip, GeolocationErrorCode.NoSuchGeolocation);
            }

            return _mapper.Map<GeolocationDto>(entity);
        }

        public async Task<GeolocationDto> AddGeolocationAsync(string address)
        {
            var ip = await _geolocationHelper.GetIPFromAddressAsync(address);
            
            var entity = await _geolocationRepository.GetAsync(ip);

            if (entity != null)
            {
                throw new GeolocationException(address, GeolocationErrorCode.GeolocationExists);
            }
            
            var model = await _geolocationHelper.GetGeolocationAsync(ip);

            entity = _mapper.Map<GeolocationEntity>(model);

            await _geolocationRepository.AddAsync(entity);

            return _mapper.Map<GeolocationDto>(entity);
        }

        public async Task DeleteGeolocationAsync(string address)
        {
            var ip = await _geolocationHelper.GetIPFromAddressAsync(address);
            var entity = await _geolocationRepository.GetAsync(ip);

            if (entity == null)
            {
                throw new GeolocationException(ip, GeolocationErrorCode.NoSuchGeolocation);
            }

            await _geolocationRepository.DeleteAsync(entity);
        }
    }
}