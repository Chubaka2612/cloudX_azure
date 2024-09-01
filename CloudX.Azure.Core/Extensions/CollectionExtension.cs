namespace CloudX.Azure.Core.Extensions
{
    public static class CollectionExtension
    {
        public static string ToJoinString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return string.Join(", ", dictionary.Select(kv => kv.Key + ": " + kv.Value).ToArray());
        }

        public static string ToJoinString<TKey, TValue>(this IList<Dictionary<TKey, TValue>> dictionary)
        {
            var resultString = "";
            foreach (var pair in dictionary)
            {
                resultString += pair.ToJoinString() + "; ";
            }
            return resultString;
        }

        public static string ToJoinString<T>(this IList<T> list)
        {
            return string.Join(", ", list);
        }

        public static string ToJoinStringNewRow<T>(this IList<T> list)
        {
            return string.Join(Environment.NewLine, list);
        }

        public static T GetRandom<T>(this IList<T> list)
        {
            var random = new Random();
            return list[random.Next(list.Count)];
        }
    }
}
