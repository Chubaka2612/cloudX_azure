using CloudX.Azure.Core.Attributes;
using CloudX.Azure.Core.Web;
using NUnit.Framework;
using Cloud.Azure.AwesomePizzaSite.Web.Tests.Meta;
using Cloud.Azure.AwesomePizzaSite.Web.Pages;
using Cloud.Azure.AwesomePizzaSite.Data.Meta;
using CloudX.Azure.Core.Utils;

namespace Cloud.Azure.AwesomePizzaSite.Web.Tests.ManageIngredients
{
    public class PizzaCatalogTest : BaseTest
    {
        [Test]
        [Component(ComponentName.PizzaCatalog)]
        [Category(TestType.WebUi)]
        public void PizzaCatalogSortingTest()
        {
            Ui.GetPage<PizzaCatalogPage>()
               .Open()
               .AscSortingButton.Click();

            Ui.GetPage<PizzaCatalogPage>()
                .PizzaCardColumnsContainer.PizzaCardColumns.ForEach(column =>
                {
                    var actualTitles = column.PizzaCards.Select(card => card.TitleTextElement.Text).ToList();
                    var expectedTitles = actualTitles.OrderBy(title => title).ToList();

                    VerifyThat.CollectionByOrderingEquals(expectedTitles, actualTitles, "Verify pizza catalog is sorted by ASC");
                });

            Ui.GetPage<PizzaCatalogPage>()
                .DescSortingButton.Click();
            Ui.GetPage<PizzaCatalogPage>()
                .PizzaCardColumnsContainer.PizzaCardColumns.ForEach(column =>
             {
                 var actualTitles = column.PizzaCards.Select(card => card.TitleTextElement.Text).ToList();
                 var expectedTitles = actualTitles.OrderByDescending(title => title).ToList();

                 VerifyThat.CollectionByOrderingEquals(expectedTitles, actualTitles, "Verify pizza catalog is sorted by DESC");
             });
        }
    }
}
