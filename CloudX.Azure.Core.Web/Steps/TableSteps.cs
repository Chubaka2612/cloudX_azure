using log4net;
using CloudX.Azure.Core.Extensions;
using CloudX.Azure.Core.Web.PageObjects.Elements.Interfaces.Complex;
using CloudX.Azure.Core.Utils;
using CloudX.Azure.Core.Enums;
using CloudX.Azure.Core.Web.PageObjects.Elements.Base;
using CloudX.Azure.Core.Web.PageObjects.Pages;
using CloudX.Azure.Core.Web.PageObjects.Elements.Complex.Table;
using CloudX.Azure.Core.Web.PageObjects.Elements.Composite;
using CloudX.Azure.Core.Web.Exceptions;
using OpenQA.Selenium;

namespace CloudX.Azure.Core.Web.Steps
{
    public static class TableSteps
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(TableSteps));

        public static ITable VerifyHasColumns(this ITable table, IList<string> expectedColumns)
        {
            var actualColumns = table.HeadersText;

            var result = actualColumns.Select(col => col.ToUpper()).SequenceEqual(expectedColumns.Select(col => col.ToUpper()));

            VerifyThat.IsTrue(result, $"Verify table '{table.Name}' has columns" +
                                        $"\nExpected: {expectedColumns.ToJoinString()};" +
                                        $"\nActual  : {actualColumns.ToJoinString()}");
            return table;
        }

        public static ITable VerifyContainsColumns(this ITable table, IList<string> expectedColumns, bool shouldContain = true)
        {
            var actualColumns = table.HeadersText;
            if (shouldContain)
            {
                VerifyThat.CollectionContains(actualColumns, expectedColumns, $"Verify table '{table.Name}' contains columns" +
                        $"\nExpected: {expectedColumns.ToJoinString()};" +
                        $"\nActual  : {actualColumns.ToJoinString()}");
            }
            else
            {
                VerifyThat.CollectionNotContains(actualColumns, expectedColumns, $"Verify table '{table.Name}' doesn't contains columns" +
                        $"\nExpected: {expectedColumns.ToJoinString()};" +
                        $"\nActual  : {actualColumns.ToJoinString()}");
            }
            return table;
        }

        public static ITable VerifyRowsCount(this ITable table, int expectedCount)
        {
            Waiter.Wait.Until(driver => table.TableSize >= expectedCount);
            var actualCount = table.TableSize;
            VerifyThat.IsTrue(actualCount == expectedCount, $"Verify records count for {table.Name} table" +
                $"\nExpected: {expectedCount};" +
                $"\nActual  : {actualCount}");
            return table;
        }

        public static ITable VerifyRowsCountMoreThanExpected(this ITable table, bool isMore, int expectedCount)
        {
            var actualCount = table.TableSize;
            VerifyThat.IsTrue(isMore ? actualCount >= expectedCount : actualCount <= expectedCount,
                $"Verify rows count in '{table.Name}' table" +
                $"\nExpected: {(isMore ? "more" : "less")} than {expectedCount}" +
                $"\nActual  : {actualCount} \n");
            return table;
        }

        public static ITable VerifyColumnValuesOrdering(this ITable table, string columnName, SortOrder orderBy, string format = default)
        {
            Log.Info($"Verify values of column '{columnName}' of '{table.Name}' table are sorted by '{orderBy}'");
            var actualRows = table.GetRowsValuesWithHeaders();
            var expectedRows = RecordsHelper.RecordsOrderBy(actualRows, columnName, orderBy, format);
            VerifyThat.IsTrue(RecordsHelper.AreRecordsEqual(actualRows, expectedRows), "Verify records are equal", false);
            return table;
        }

        public static ITable VerifyContainsRows(this ITable table, IList<Dictionary<string, string>> expectedRows, bool shouldContain = true)
        {
            Log.Info($"Verify '{table.Name}' {(shouldContain ? "contains" : "doesn't contain")} the following rows:" +
                $"\n'{expectedRows.ToJoinString()}'");

            if (shouldContain)
            {
                Waiter.Wait.Until(driver => table.TableSize >= 1);
            }

            var actualRows = table.GetRowsValuesWithHeaders();
            foreach (var row in expectedRows)
            {
                var isRowFound = RecordsHelper.HasRecord(actualRows, row);

                if (!shouldContain && isRowFound)
                {
                    VerifyThat.ThrowFail($"The following record was found on the '{table.Name}' table: \n'{row.ToJoinString()}'");
                }
                if (shouldContain && !isRowFound)
                {
                    VerifyThat.ThrowFail($"The following record was not found on the '{table.Name}' table: \n'{row.ToJoinString()}'");
                }
            }
            return table;
        }

        public static bool ContainsRows(this ITable table, IList<Dictionary<string, string>> expectedRows, bool shouldContain = true)
        {
            Waiter.Wait.Until(driver => table.TableSize >= 1);
            var actualRows = table.GetRowsValuesWithHeaders();
            foreach (var row in expectedRows)
            {
                var isRowFound = RecordsHelper.HasRecord(actualRows, row);

                if (!shouldContain && isRowFound || shouldContain && !isRowFound)
                {
                    return false;
                }
            }
            return true;
        }

        public static ITable VerifyContainsRow(this ITable table, Dictionary<string, string> expectedRow, bool shouldContain = true)
        {
            var dictionaryToList = new List<Dictionary<string, string>>
            {
                expectedRow
            };
            table.VerifyContainsRows(dictionaryToList, shouldContain);
            return table;
        }


        //click on the cell with name 'cellName' of row which has a specific value: 'requiredCellName' = 'requiredCellValue'
        public static ITable CellClick(this ITable table,
            string cellName,
            string requiredColumnName,
            string requiredColumnValue)
        {
            Log.Info($"Click on '{cellName}' of row which has a specific value: '{requiredColumnName}' = '{requiredColumnValue}'");
            GetCell(table, cellName, requiredColumnName, requiredColumnValue).FindElement(By.XPath(".//a"))
                .Click();

            return table;
        }

        //get cell with name 'cellName' of row which has a specific value: 'requiredCellName' = 'requiredCellValue'
        public static Cell GetCell(this ITable table, string cellName, string requiredColumnName, string requiredColumnValue)
        {
            var rows = table.GetRowsValues();
            var requiredRow = rows.First(row =>
            {
                var requiredCell = row.Cells.First(cell =>
                     string.Equals(cell.HeaderText, requiredColumnName, StringComparison.Ordinal));
                return requiredCell.Value.Contains(requiredColumnValue, StringComparison.Ordinal);
            });
            return requiredRow.GetCell(cellName);
        }

    }
}