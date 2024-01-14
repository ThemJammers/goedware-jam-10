using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<KeyValuePair<int, T>> Enumerate<T>(this IEnumerable<T> items) {
            return items.Select((item, key) => KeyValuePair.Create(key, item));
        }
    }
}