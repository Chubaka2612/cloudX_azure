

namespace CloudX.Azure.Core.Utils
{
    public static class CommonUtils
    {
        public static IList<T> GetAttributes<T>(Type type) where T : class
        {
            return Attribute.GetCustomAttributes(type, typeof(T), false).Cast<T>().ToList();
        }

        public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            if (sequence == null)
                return;
            foreach (var obj in sequence)
                action(obj);
        }
    }
}