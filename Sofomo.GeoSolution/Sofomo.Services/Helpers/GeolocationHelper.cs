using System.Net;
using System.Text.Json;
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
           // var response = await GetIPStackResponseAsync(ip);
           var response =
               "{\"ip\": \"40.65.205.118\", \"type\": \"ipv4\", \"continent_code\": \"NA\", \"continent_name\": \"North America\", \"country_code\": \"US\", \"country_name\": \"United States\", \"region_code\": \"VA\", \"region_name\": \"Virginia\", \"city\": \"Boydton\", \"zip\": \"23917\", \"latitude\": 36.64046859741211, \"longitude\": -78.26995086669922, \"location\": {\"geoname_id\": null, \"capital\": \"Washington D.C.\" }}";
            
           //var response = "{\"success\": false,\"error\": {\"code\": 104,\"type\": \"monthly_limit_reached\",\"info\": \"Your monthly API request volume has been reached. Please upgrade your plan.\"}}";

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
            
            //_logger.Error($"[ERR] Request of sending item was failed. Request: {json} Response: {response}");
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