using Sofomo.Models;
using Sofomo.Network;

namespace Sofomo.Services
{
    public interface IGeolocationHelper
    {
        Task<GeolocationModel> GetGeolocationAsync(string address);
        Task<string> GetIPFromAddressAsync(string address);
    }
}