using Newtonsoft.Json;


namespace Cloud.Azure.AwesomePizzaSite.Data.Model.UI
{
    public class PizzaModel
    {
        public string Title { get; set; }

        public List<IngredientModel> Ingredients { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
