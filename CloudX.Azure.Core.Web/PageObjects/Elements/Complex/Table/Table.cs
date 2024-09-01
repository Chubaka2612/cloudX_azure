using OpenQA.Selenium;
using System.Reflection;
using CloudX.Azure.Core.Extensions;
using CloudX.Azure.Core.Web.PageObjects.Elements.Composite;
using CloudX.Azure.Core.Web.PageObjects.Pages.Utils;
using CloudX.Azure.Core.Web.PageObjects.Extensions;
using CloudX.Azure.Core.Web.PageObjects.Elements.Interfaces.Complex;
using CloudX.Azure.Core.Web.Attributes;
using CloudX.Azure.Core.Web.PageObjects.Elements.Base;

namespace CloudX.Azure.Core.Web.PageObjects.Elements.Complex.Table
{
    public class Table : Container, ITable
    {
        public Table(By locator) : base(locator)
        {
        }
        protected virtual string JsFileName => "GetTableCellValues";

        protected virtual By TableHeadersByLocator { get; set; } = By.XPath(".//thead//th");

        protected virtual By TableBodyByLocator { get; set; } = By.XPath(".//tbody");

        protected virtual By TableRowsByLocator { get; set; } = By.XPath(".//tr");

        protected virtual By TableCellsByLocator { get; set; } = By.XPath(".//td|.//th");

        protected UiElement Body => UiElementFactory.CreateInstance<UiElement>(TableBodyByLocator, this);

        protected IList<UiElement> Headers => UiElementsInit.GetChildElements<UiElement>(this, TableHeadersByLocator);

        public virtual IList<string> HeadersText => Headers.Select(x => x.Text.Trim()).ToList();

        public IList<Row> Rows => RetrieveRowsList();

        public int TableSize => Rows.Count;

        public bool IsTableEmpty => TableSize == 0;

        public virtual IList<Dictionary<string, string>> GetRowsValuesWithHeaders()
        {
            var rows = GetRowsValues();
            return rows.Select(row => row.Cells.ToDictionary(cell => cell.HeaderText.Trim(),
                cell => cell.Value.Replace("\n", " ").Replace("\r", " ")))
                .ToList();
        }

        public IList<string> GetColumnValues(string columnName)
        {
            var rows = GetRowsValues();
            return rows.Select(row => row.Cells.Where(cell => cell.HeaderText == columnName).First())
                   .ToList()
                   .Select(cell => cell.Value).ToList();
        }


        public virtual IList<Row> GetRowsValues()
        {
            var rowsCollection = new List<Row>();
            foreach (var row in Rows)
            {
                row.Cells = row.RetrieveCells();
                rowsCollection.Add(row);
            }
            return rowsCollection;
        }

        protected IList<Row> RetrieveRowsList()
        {
            if (!Body.Exists)
            {
                return new List<Row>();
            }

            return Body.FindElements(TableRowsByLocator).Select(rowElement =>
            {
                var row = UiElementFactory.CreateInstance<Row>(TableRowsByLocator, this, rowElement);
                row.CellsLocator = TableCellsByLocator;
                return row;
            }).ToList();
        }

       
        public Row GetRow(int index)
        {
            var rows = RetrieveRowsList();
            if (rows.Count < index)
            {
                throw new ArgumentException("The amount of rows is less than index");
            }
            return rows[index];
        }

        public virtual UiElement GetColumn(string columnName)
        {
            return Headers.FirstOrDefault(h => string.Equals(h.GetAttribute("aria-label").Trim(),
                       columnName,
                       StringComparison.OrdinalIgnoreCase)) ??
                   throw new NotFoundException($"The column {columnName} was not found on the {Name} table");
        }
    }
}
