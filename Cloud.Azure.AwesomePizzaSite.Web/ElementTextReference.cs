
namespace Cloud.Azure.AwesomePizzaSite.Web
{
    public class ElementTextReference
    {
        public static class Menu
        {
            public static readonly string ManageIngredientsAddNew = "/ingredients/add";
            public static readonly string ManageIngredientsList = "/ingredient";
            public static readonly string OurIngredients = "/our_ingredients";
            public static readonly string ManagePizzaAddNew = "/pizzas/add";
            public static readonly string ManagePizzaList = "/pizzas";
        }

        public static class Table
        {
            public static class Ingredients
            {
                public static readonly string Title = "Title";
                public static readonly string Id = "Id";
            }

            public static class Pizza
            {
                public static readonly string Title = "Title";
                public static readonly string Id = "Id";
                public static readonly string Actions = "Actions";
            }
        }
    }
}
