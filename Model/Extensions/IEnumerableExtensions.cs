using System.Collections.Generic;

namespace Model.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Returns the number of elements in an <see cref="IEnumerable{T}"/>.
        /// </summary>
        public static int GetCount<T>(this IEnumerable<T> list)
        {
            int count = 0;
            foreach (var entity in list)
            {
                count++;
            }
            return count;
        }
    }
}