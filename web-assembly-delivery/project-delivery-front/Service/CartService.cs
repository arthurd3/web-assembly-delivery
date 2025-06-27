using project_delivery.Models;
using project_delivery.Shared;

namespace project_delivery.Service
{

    public class CartService
    {
        public event Action? OnChange;
        public List<FoodCart> CartItems { get; private set; } = new();

        public bool AddToCart(Food food)
        {
            var item = CartItems.FirstOrDefault(i => i.Id == food.Id);
            if (item != null)
            {
                item.Quantity++;
                
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
                
            }
            NotifyStateChanged();
            return  true;

        }
        private void NotifyStateChanged() => OnChange?.Invoke();
        public void RemoveFromCart(FoodCart item) => CartItems.Remove(item);

        public double GetTotal() => CartItems.Sum(i => i.Price * i.Quantity);
    }

}