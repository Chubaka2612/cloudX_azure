using OpenQA.Selenium;
using CloudX.Azure.Core.Exceptions;
using CloudX.Azure.Core.Web.PageObjects.Elements.Interfaces.Complex;
using CloudX.Azure.Core.Web.PageObjects.Elements.Base;

namespace CloudX.Azure.Core.Web.PageObjects.Elements.Complex.Table
{
    public class Row : UiElement
    {
        public Row(By locator) : base(locator)
        {
        }

        public virtual By CellsLocator { get; set; }

        protected IList<Cell> CellsList;

        public virtual IList<Cell> Cells
        {
            get => CellsList ?? RetrieveCells();
            set => CellsList = value;
        }

        protected virtual IList<string> Headers => ((ITable)Parent).HeadersText;


        public virtual IList<Cell> RetrieveCells()
        {
            var cellElements = FindElements(CellsLocator).ToList();
            var cellList = new List<Cell>();
            if (cellElements.Count != Headers.Count)
            {
                throw new InitializationException("Incorrect amount of cells. Cell amount should be equal to table header amount");
            }

            for (var i = 0; i < cellElements.Count; i++)
            {
                var cell = UiElementFactory.CreateInstance<Cell>(null, this, cellElements[i]);
                cell.HeaderText = Headers[i];
                cell.Locator = CellsLocator;
                cellList.Add(cell);
            }
            return cellList;
        }

        public Cell GetCell(string cellName) => Cells.FirstOrDefault(cell => string.Equals(cell.HeaderText.Trim(), cellName, StringComparison.OrdinalIgnoreCase))
            ?? throw new NotFoundException($"Can't find '{cellName}' cell");
    }
}
