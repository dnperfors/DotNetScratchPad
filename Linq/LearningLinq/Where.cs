using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearningLinq
{
    public static partial class Enumerable
    {
        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (predicate == null)
                throw new ArgumentNullException("predicate");
            return WhereImpl(source, predicate);
        }

        private static IEnumerable<TSource> WhereImpl<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            foreach (TSource item in source)
            {
                if (predicate(item))
                    yield return item;
            }
        }

        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (predicate == null)
                throw new ArgumentNullException("predicate");
            return WhereImpl(source, predicate);
        }

        private static IEnumerable<TSource> WhereImpl<TSource>(IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            int count = 0;
            foreach (TSource item in source)
            {
                if (predicate(item, count))
                    yield return item;
                count++;
            }
        }
    }
}
