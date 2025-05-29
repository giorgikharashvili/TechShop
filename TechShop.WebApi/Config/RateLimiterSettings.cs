namespace TechShop.WebAPI.Config
{
    public class RateLimiterSettings
    {
        public int MaxRequests { get; set; }
        public int WindowSeconds { get; set; }

    }
}
