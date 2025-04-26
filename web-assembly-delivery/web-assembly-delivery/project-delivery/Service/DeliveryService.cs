using project_delivery.Models;

namespace project_delivery.Service
{
    public class DeliveryService
    {
        static string? h2Food {get ; set;} = null;
        private List<string> typeFoods = allTypes();
        private List<Food> foods = new()
        {
            new Food { Name = "Pizza", Price=39.90 , descountedPrice =50.00 , Description = "Pizza Rechada com caputiry" , ImageUrl ="https://imgs.search.brave.com/pVOxl6h5rJTyFNB5osu_G8pNEPZGtHez8KM3vzGVUwc/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly91cGxv/YWQud2lraW1lZGlh/Lm9yZy93aWtpcGVk/aWEvY29tbW9ucy90/aHVtYi85LzkxL1Bp/enphLTMwMDczOTUu/anBnLzUxMnB4LVBp/enphLTMwMDczOTUu/anBn"},
            new Food { Name = "Hambúrguer",Price=24.30, descountedPrice = 30.00,Description = "X-Tudo" , ImageUrl ="https://imgs.search.brave.com/xMBL0QeuIXT1KLKiMSObqVmFhiHKjJcLVD8QlYkjqvI/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly9jYXJh/cHJldGEudnRleGFz/c2V0cy5jb20vYXJx/dWl2b3MvaWRzLzE1/NTUzMy04MDAtYXV0/bz92PTYzODU1MjA5/MDUzODgwMDAwMCZ3/aWR0aD04MDAmaGVp/Z2h0PWF1dG8mYXNw/ZWN0PXRydWU"},
            new Food { Name = "Sushi", Price=43.90 ,  Description = "Temaki Salmão" , ImageUrl = "images/foods/temaki-salmao.png" },
            new Food { Name = "Sushi", Price=43.90 , Description = "Temaki Salmão Hot" , ImageUrl = "images/foods/temaki-salmaohot.png" },
            new Food { Name = "Pizza", Price=50.70 , Description = "Pizza Rechada com caputiry" , ImageUrl ="images/foods/pizzahut-carne-seca.jpg"},
            new Food { Name = "Pizza",Price=50.30, Description = "Pizza Pepperoni" , ImageUrl ="images/foods/pizza-pepperoni.png"},
            new Food { Name = "Pizza", Price=29.90, descountedPrice = 39.90, Description = "Pizza Brigadeiro" , ImageUrl ="images/foods/pizza-brigadeiro.png"},
            new Food { Name = "Sushi",Price=45.900, Description = "Temaki Filadelfia" , ImageUrl ="images/foods/temaki-filadelfia.png"},
            new Food { Name = "Pizza", Price=45.70 , Description = "Pizza Mussarela" , ImageUrl ="images/foods/pizza-mussarela.png"},
            new Food { Name = "Pizza",Price=50.30, Description = "Pizza 4 Queijos" , ImageUrl ="images/foods/pizza-dominos-queijo.jpg"},
            new Food { Name = "Bebidas", Price=3.50 , Description = "Agua com Gas" , ImageUrl ="images/foods/agua-gas.png"},
            new Food { Name = "Bebidas", Price=2.50 , Description = "Agua sem Gas" , ImageUrl ="images/foods/agua.png"},
            new Food { Name = "Bebidas", Price=5.50 , Description = "Coca-Cola Lata" , ImageUrl ="images/foods/coca-lata.png"},
            new Food { Name = "Bebidas", Price=5.50 , Description = "Pepsi Lata" , ImageUrl ="images/foods/pepsi-lata.png"},
            new Food { Name = "Bebidas", Price=5.50 , Description = "Guaraná Lata" , ImageUrl ="images/foods/guarana-lata.png"},
            new Food { Name = "Bebidas", Price=5.50 , Description = "Fanta Laranja Lata" , ImageUrl ="images/foods/fanta-laranja-lata.png"},
            new Food { Name = "Bebidas", Price=5.50 , Description = "Suco de Uva" , ImageUrl ="images/foods/suco-uva.png"},
            new Food { Name = "Bebidas", Price=5.50 , Description = "Suco de Laranja" , ImageUrl ="images/foods/suco-laranja.png"},
            new Food { Name = "Bebidas", Price=5.50 , Description = "Suco de Abacaxi" , ImageUrl ="images/foods/suco-abacaxi.png"},
            new Food { Name = "Acai", Price=11.90 , Description = "Acai Puro" , ImageUrl ="images/foods/acai.png"},
            new Food { Name = "Acai", Price=13.90 , Description = "Acai com Banana" , ImageUrl ="images/foods/acai-banana.png"},
            new Food { Name = "Acai", Price=13.90 , Description = "Acai com Morango" , ImageUrl ="images/foods/acai-morango.png"},
            new Food { Name = "Acai", Price=15.90 , Description = "Acai com Banana e Morango" , ImageUrl ="images/foods/acai-banana-morango.png"},
            new Food { Name = "Hamburguer", Price=23.90 , Description = "Hamburguer" , ImageUrl ="images/foods/hamburguer1.png"},
            new Food { Name = "Hamburguer", Price=25.90 , Description = "Hamburguer" , ImageUrl ="images/foods/hamburguer2.png"},
        };
        
        public List<Food> GetFoods()
        {   
            return foods;
        }

        public string? GetH2()
        {   
            return h2Food;
        }

        public List<string> getTypeFoods(){
            return typeFoods;;
        }



        public void changeFilter(string eventValue){

            if("Todos Produtos".Equals(eventValue)){
                h2Food = "Todos Produtos";
                typeFoods = allTypes();
                return;
                
            }

            foreach(var setFilter in typeFoods)
            {
                if(setFilter.Equals(eventValue))
                {
                   typeFoods = new List<string> {eventValue , "Todos Produtos" };
                }
            }
            h2Food = "Filtrando por: " + eventValue + "s";
        }


        public static List<string> allTypes()
        {
            List<string> listReturn = Enum.GetNames(typeof(TypeFood)).ToList();
            listReturn.Insert(0, "Todos Produtos"); 
            return listReturn;
        }


    }


}