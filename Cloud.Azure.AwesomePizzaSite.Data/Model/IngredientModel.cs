using Newtonsoft.Json;


namespace Cloud.Azure.AwesomePizzaSite.Data.Model
{
    public class IngredientModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
