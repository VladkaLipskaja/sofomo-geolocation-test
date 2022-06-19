namespace Sofomo.Models
{
    public class IPStackOptions
    {
        public string ClientName { get; set; }
        
        public string Address { get; set; }
        
        public string Parameters { get; set; }

        public int RetrySeconds { get; set; }

        public int RetryTimes { get; set; }
    }
}