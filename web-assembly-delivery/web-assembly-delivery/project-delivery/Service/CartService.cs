namespace project_delivery.Service
{

    using System.Collections.Generic;
    using project_delivery.Models;

    public class CartService
    {
        public static List<FoodCart> CartItems { get; private set; } = new();

        public static bool AddToCart(Food food)
        {
            var item = CartItems.FirstOrDefault(i => i.Id == food.Id);
            if (item != null)
            {
                item.Quantity++;
                return true;
            }
            
            else
            {
                CartItems.Add(new FoodCart
                {
                    Id = food.Id,
                    Name = food.Name,
                    Description = food.Description,
                    ImageUrl = food.ImageUrl,
                    Price = food.Price,
                    Quantity = 1
                });
                return true;
            }
        }

        public void RemoveFromCart(FoodCart item) => CartItems.Remove(item);

        public double GetTotal() => CartItems.Sum(i => i.Price * i.Quantity);
    }

}