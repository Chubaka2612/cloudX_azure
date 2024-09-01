using CloudX.Azure.Core.Enums;
using CloudX.Azure.Core.Extensions;
using System.Globalization;


namespace CloudX.Azure.Core.Utils
{
    public class RecordsHelper
    {
        public static IList<Dictionary<string, string>> RecordsOrderBy(IList<Dictionary<string, string>> records,
            string field, SortOrder sortOrder, string format = default)
        {
            var result = new List<Dictionary<string, string>>();

            if (sortOrder == SortOrder.Asc)
            {
                if (IsSequenceOfType<double>(records, double.TryParse, field))
                {
                    return new List<Dictionary<string, string>>(records.OrderBy(record => double.Parse(record[field])));
                }

                if (!string.IsNullOrEmpty(format) && IsSequenceOfType<DateTimeOffset>(records, DateTimeOffset.ParseExact, format, field))
                {
                    return new List<Dictionary<string, string>>(records.OrderBy(record =>
                        DateTimeOffset.ParseExact(record[field], format, CultureInfo.InvariantCulture)));
                }
                return new List<Dictionary<string, string>>(records.OrderBy(record => record[field],
                    StringComparer.InvariantCultureIgnoreCase));
            }
            else
            {
                if (IsSequenceOfType<double>(records, double.TryParse, field))
                {
                    return new List<Dictionary<string, string>>(
                        records.OrderByDescending(record => double.Parse(record[field])));
                }

                if (!string.IsNullOrEmpty(format) && IsSequenceOfType<DateTimeOffset>(records, DateTimeOffset.ParseExact, format, field))
                {
                    return new List<Dictionary<string, string>>(
                        records.OrderByDescending(record => DateTimeOffset.ParseExact(record[field], format, CultureInfo.InvariantCulture)));
                }

                return new List<Dictionary<string, string>>(records.OrderByDescending(record => record[field],
                    StringComparer.InvariantCultureIgnoreCase));
            }
        }

        private static IList<Dictionary<string, string>> RecordsOrderByType<T>(
            IList<Dictionary<string, string>> records, string field, SortOrder sortOrder,
            TryParseHandler<T> tryParseHandler)
        {

            var isSequenceOfType = IsSequenceOfType(records, tryParseHandler, field);
            if (sortOrder == SortOrder.Asc)
            {
                return isSequenceOfType
                    ? new List<Dictionary<string, string>>(records.OrderBy(record =>
                        tryParseHandler(record[field], out _)))
                    : new List<Dictionary<string, string>>(records.OrderBy(record => record[field],
                        StringComparer.InvariantCultureIgnoreCase));
            }
            else
            {
                return isSequenceOfType
                    ? new List<Dictionary<string, string>>(records.OrderByDescending(record =>
                        tryParseHandler(record[field], out _)))
                    : new List<Dictionary<string, string>>(records.OrderByDescending(record => record[field],
                        StringComparer.InvariantCultureIgnoreCase));
            }
        }

        //Sequence of rows is important
        public static bool AreRecordsEqual(IList<Dictionary<string, string>> actualRecords, IList<Dictionary<string, string>> expectedRecords)
        {
            bool areEqual = false;
            if (actualRecords.Count != expectedRecords.Count)
            {
                VerifyThat.Logger.Warn($"The amount of records is not the same." +
                    $"\nExpected: {expectedRecords.Count};" +
                    $"\nActual  : {actualRecords.Count}");
                return areEqual;
            }

            for (var i = 0; i < expectedRecords.Count; i++)
            {
                areEqual = expectedRecords[i].SequenceEqual(actualRecords[i]);
                if (!areEqual)
                {
                    VerifyThat.Logger.Warn($"Records have incorrect order." +
                        $"\nExpected: {expectedRecords[i].ToJoinString()};" +
                        $"\nActual  : {actualRecords[i].ToJoinString()}");
                    return false;
                }
            }
            return areEqual;
        }

        public static bool HasRecord(IList<Dictionary<string, string>> actualRecords, Dictionary<string, string> expectedRecord)
        {
            foreach (var actualRecord in actualRecords)
            {
                var recordFound = IsRecordEqualTo(actualRecord, expectedRecord);

                if (recordFound)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsRecordEqualTo(Dictionary<string, string> actualRecord, Dictionary<string, string> expectedRecord)
        {
            var isRecordFound = true;

            //Transform key to simplify handling properties from different DTOs
            var actualRecordsKeysTransformed = new Dictionary<string, string>();
            foreach (var pair in actualRecord)
            {
                actualRecordsKeysTransformed.Add(pair.Key.RemoveWhitespace().ToUpperInvariant().Trim(), pair.Value);
            }
            var expectedRecordsKeysTransformed = new Dictionary<string, string>();
            foreach (var pair in expectedRecord)
            {
                expectedRecordsKeysTransformed.Add(pair.Key.RemoveWhitespace().ToUpperInvariant().Trim(), pair.Value);
            }

            foreach (var key in expectedRecordsKeysTransformed.Keys)
            {
                VerifyThat.IsTrue(actualRecordsKeysTransformed.ContainsKey(key), $"Verify key exists: '{key}'", false);
                var actualValue = actualRecordsKeysTransformed[key] ?? "null";
                isRecordFound = isRecordFound && actualValue.Contains(expectedRecordsKeysTransformed[key], StringComparison.CurrentCultureIgnoreCase);
                if (!isRecordFound)
                {
                    VerifyThat.Logger.Debug($"Records are different: Parameter: {key}; \nExpected: {expectedRecordsKeysTransformed[key]}, Actual: {actualValue}");
                }
            }
            return isRecordFound;
        }

        private static bool IsSequenceOfType<T>(IList<Dictionary<string, string>> records, TryParseHandler<T> tryParseHandler, string field)
        {
            return records.Select(record => record[field]).All(value => tryParseHandler(value, out _));
        }

        private static bool IsSequenceOfType<T>(IList<Dictionary<string, string>> records, TryParseExactHandler<T> tryParseExactHandler, string format, string field)
        {
            return records.Select(record => record[field]).All(value =>
            {
                DateTimeOffset dateTimeOffset = tryParseExactHandler(value, format, CultureInfo.InvariantCulture);
                return dateTimeOffset != default;
            });
        }

        private delegate bool TryParseHandler<T>(string value, out T result);

        private delegate DateTimeOffset TryParseExactHandler<T>(string input, string format, IFormatProvider formatProvider);
    }
}

