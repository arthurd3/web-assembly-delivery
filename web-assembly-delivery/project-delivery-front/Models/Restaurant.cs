namespace project_delivery.Models
{
    public class Restaurant
    {
        public string Name { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public double Rating { get; set; }  = 0.0 ;
        public string Category { get; set; } = string.Empty;
        public int DeliveryTime { get; set; } = 0 ;
        public string DeliveryFee { get; set; } = string.Empty;
    }
}
