using project_delivery.Models;

namespace project_delivery.Service
{

    public class HomeService
    {
        private List<Category> categories = new()
        {
            new Category { Name = "Pizza", Icon = "Images/foods/pizza_PNG43989.webp" }, 
            new Category { Name = "Hambúrguer", Icon = "Images/foods/burger-hamburger-burger-isolated-on-transparent-background-png.webp" },
            new Category { Name = "Sushi", Icon = "Images/foods/sushi-platter-with-different-types-of-sushi-free-png.webp" },
            new Category { Name = "Marmita", Icon = "Images/foods/serene-artistic-portable-meal-kit-eco-friendly-lunch-box-with-coffee-4k-png.webp" },
            new Category { Name = "Açaí", Icon = "Images/foods/polpa-norte-acai-b00079bedbd4e7ad11df08f4c8ff97059711.webp" },
            new Category { Name = "Bebidas", Icon = "Images/foods/Glass-Milkshake-PNG-Clipart.webp" }
        };

        private List<Restaurant> restaurants = new()
        {
            new Restaurant { Name = "Pizzaria Baggio", Image = "Images/places/baggio-pizzaria-moema.webp", Rating = 4.5, Category = "Pizza", DeliveryTime = 30, DeliveryFee = "R$ 5,00" },
            new Restaurant { Name = "Malola Burguer", Image = "Images/places/Restaurant-Malola-Burguer-photo.webp", Rating = 4.2, Category = "Hambúrguer", DeliveryTime = 25, DeliveryFee = "Grátis💲" },
            new Restaurant { Name = "Sushi2You", Image = "Images/places/sushi2you.webp", Rating = 4.6, Category = "Sushi", DeliveryTime = 40, DeliveryFee = "R$ 7,00" },
            new Restaurant { Name = "Madeiro", Image = "Images/places/MADERO JUIZ DE FORA - 01.jpg", Rating = 4.6, Category = "Hambúrguer", DeliveryTime = 40, DeliveryFee = "R$ 7,00" },
            new Restaurant { Name = "McDonald's", Image = "Images/places/bents.jpg", Rating = 4.6, Category = "Sushi", DeliveryTime = 40, DeliveryFee = "Grátis💲" }

        };

        private List<Promotion> promotions = new()
        {
            new Promotion { Title = "50% off em Pizzas", Description = "Aproveite meia pizza por metade do preço", Image = "Images/foods/pizza-de-calabresa-em-cima-da-mesa_140725-5396.webp" },
            new Promotion { Title = "Frete grátis no Sushi", Description = "Pedidos acima de R$50", Image = "Images/foods/prato-de-frutos-do-mar-frescos-com-sashimi-de-sushi-e-wasabi-gerado-por-ia-1_uid_651da32981f32.webp" },
        };


        public List<Category> getCategories(){
            return categories;
        }

        public List<Restaurant> getRestaurants(){
            return restaurants;
        }

        public List<Promotion> getPromotion(){
            return promotions;
        }
    }
}

