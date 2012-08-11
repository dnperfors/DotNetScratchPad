using System;
using System.Collections.Generic;

namespace LearningLinq
{
    public static partial class Enumerable
    {
        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source)
        {
            return Distinct(source, EqualityComparer<TSource>.Default);
        }

        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            if (source == null) throw new ArgumentNullException("source");
            return DistinctImpl(source, comparer);
        }

        static IEnumerable<TSource> DistinctImpl<TSource>(IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            HashSet<TSource> givenItems = new HashSet<TSource>(comparer);

            foreach (var item in source)
            {
                if (givenItems.Add(item))
                    yield return item;
            }
        }
    }
}
