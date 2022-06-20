using System.Net;
using AutoMapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sofomo.Models;
using Sofomo.Network;

namespace Sofomo.Services
{
    public class GeolocationHelper : IGeolocationHelper
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IMapper _mapper;
        private readonly IPStackOptions _ipStackOptions;

        public GeolocationHelper(IHttpClientFactory clientFactory, IOptions<IPStackOptions> ipStackOptions,
            IMapper mapper)
        {
            _mapper = mapper;
            _clientFactory = clientFactory;
            _ipStackOptions = ipStackOptions.Value;
        }

        public async Task<GeolocationModel> GetGeolocationAsync(string ip)
        {
           var response = await GetIPStackResponseAsync(ip);

           try
           {
               var result = JsonConvert.DeserializeObject<GeolocationDto>(response);
               return _mapper.Map<GeolocationModel>(result);
           }
           catch (Exception e)
           {
               try
               {
                   var result = JsonConvert.DeserializeObject<IPStackErrorResponseDto>(response);
               }
               catch (Exception exception)
               {
                   throw new InfrastructureException(ip, InfrastructureErrorCode.IPStackUnavailable);
               }
                
               throw new InfrastructureException(ip, InfrastructureErrorCode.IPStackResponseError);
           }
        }

        public async Task<string> GetIPStackResponseAsync(string ip)
        {
            var client = _clientFactory.CreateClient(_ipStackOptions.ClientName);
            var requestMessage = GetRequestMessage(ip);
            using var httpResponse = await client.SendAsync(requestMessage);
            return await httpResponse.Content.ReadAsStringAsync();
        }

        public async Task<string> GetIPFromAddressAsync(string address)
        {
            try
            {
                var ips = await Dns.GetHostAddressesAsync(address);
                return ips[0].ToString();
            }
            catch (Exception e)
            {
                throw new AddressException(address, AddressErrorCode.InvalidAddress);
            }
        }

        public HttpRequestMessage GetRequestMessage(string ip)
        {
            var parameters = string.Format(_ipStackOptions.Parameters, ip);
            return new HttpRequestMessage(HttpMethod.Get, parameters);
        }
    }
}