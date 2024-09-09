using Newtonsoft.Json;


namespace Cloud.Azure.AwesomePizzaSite.Data.Model.UI
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
