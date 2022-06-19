namespace Sofomo.Entities
{
    public class GeolocationEntity
    {
        public string IP { get; set; }
        
        public string IPType { get; set; }
        
        public string ContinentCode { get; set; }
        
        public string ContinentName { get; set; }
        
        public string CountryCode { get; set; }
        
        public string CountryName { get; set; }
        
        public string RegionCode { get; set; }
        
        public string RegionName { get; set; }
        
        public string City { get; set; }
        
        public string Zip { get; set; }
        
        public decimal Latitude { get; set; }
        
        public decimal Longitude { get; set; }
    }
}