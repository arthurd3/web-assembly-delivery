namespace project_delivery.Models
{
    public class Food
    {
        public double descountedPrice {get; set;} = 0;
        public string Name { get; set; } = string.Empty;
        TypeFood? typeFood { get; set; } = null; 
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public double Price { get; set; } = 0;
    }
}