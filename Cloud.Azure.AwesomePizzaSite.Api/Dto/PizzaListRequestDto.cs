
namespace Cloud.Azure.AwesomePizzaSite.Api.Dto
{
    public class PizzaListRequestDto : AbstractDto
    {
        public List<Pizza> Pizzas { get; set; }

        public PizzaListRequestDto() 
        {
            Pizzas = new (); 
        }
    }

    public class Pizza
    {
        public object Name { get; set; }
       
        public List<object> Ingredients { get; set; }

        public Pizza()
        {
            Ingredients = new ();
        }
    }
}
