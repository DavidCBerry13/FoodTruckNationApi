using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Framework.Utility
{
    public static class LinqExtensions
    {

        public static IEnumerable<T> WhereNotExists<T, K>(this IEnumerable<T> source, IEnumerable<K> target, Func<T, K, bool> comparer)
        {
            return source.Where(s => !target.Any(t => comparer(s, t)));            
        }


        public static IEnumerable<T> WhereExists<T, K>(this IEnumerable<T> source, IEnumerable<K> target, Func<T, K, bool> comparer)
        {
            return source.Where(s => target.Any(t => comparer(s, t)));
        }

    }
}
