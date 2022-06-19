using Sofomo.Network;

namespace Sofomo.Services
{
    public interface IGeolocationService
    {
        Task<GeolocationDto> GetGeolocationAsync(string ip);
        Task<GeolocationDto> AddGeolocationAsync(string address);
        Task DeleteGeolocationAsync(string ip);
    }
}