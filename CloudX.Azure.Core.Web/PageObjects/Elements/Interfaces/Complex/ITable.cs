using CloudX.Azure.Core.Web.PageObjects.Elements.Complex.Table;
using CloudX.Azure.Core.Web.PageObjects.Elements.Interfaces.Composite;

namespace CloudX.Azure.Core.Web.PageObjects.Elements.Interfaces.Complex
{
    public interface ITable : IContainer
    {
        IList<string> HeadersText
        {
            get;
        }

        int TableSize
        {
            get;
        }

        IList<Dictionary<string, string>> GetRowsValuesWithHeaders();

        IList<string> GetColumnValues(string columnName);

        IList<Row> GetRowsValues();

    }
}
