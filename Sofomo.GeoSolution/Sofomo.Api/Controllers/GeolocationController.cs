using Microsoft.AspNetCore.Mvc;
using Sofomo.Models;
using Sofomo.Network;
using Sofomo.Services;

namespace Sofomo.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeolocationController : ControllerBase
    {
        private readonly IGeolocationService _geolocationService;

        public GeolocationController(IGeolocationService geolocationService)
        {
            _geolocationService = geolocationService;
        }

        [HttpPost]
        public async Task<JsonResult> AddGeolocation([FromBody] AddGeolocationRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request?.Address))
            {
                return this.JsonApi(new GeolocationException(GeolocationErrorCode.NoSuchGeolocation));
            }

            try
            {
                var geolocation = await _geolocationService.AddGeolocationAsync(request.Address);
                
                var response = new AddGeolocationResponseDto
                {
                    Geolocation = geolocation
                };

                return this.JsonApi(response);
            }
            catch (GeolocationException exception)
            {
                return this.JsonApi(exception);
            }
            catch (AddressException exception)
            {
                return this.JsonApi(exception);
            }
        }

        [HttpGet("{ip}")]
        public async Task<JsonResult> GetGeolocation(string ip)
        {
            try
            {
                var geolocation = await _geolocationService.GetGeolocationAsync(ip);
                var response = new GetGeolocationResponseDto
                {
                    Geolocation = geolocation
                };
                return this.JsonApi(response);
            }
            catch (AddressException exception)
            {
                return this.JsonApi(exception);
            }
            catch (Exception exception)
            {
                return this.JsonApi(exception);
            }
        }

        [HttpDelete("{address}")]
        public async Task<JsonResult> DeleteMission(string address)
        {
            try
            {
                await _geolocationService.DeleteGeolocationAsync(address);
                return this.JsonApi();
            }
            catch (GeolocationException exception)
            {
                return this.JsonApi(exception);
            }
            catch (AddressException exception)
            {
                return this.JsonApi(exception);
            }
            catch (Exception exception)
            {
                return this.JsonApi(exception);
            }
        }
    }
}