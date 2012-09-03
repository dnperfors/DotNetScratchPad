using System;
using System.Collections.Generic;

namespace LearningLinq
{
    public static partial class Enumerable
    {
        public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
        {
            return Join(outer, inner, outerKeySelector, innerKeySelector, resultSelector, null);
        }

        public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            if (outer == null) throw new ArgumentNullException("outer");
            if (inner == null) throw new ArgumentNullException("inner");
            if (outerKeySelector == null) throw new ArgumentNullException("outerKeySelector");
            if (innerKeySelector == null) throw new ArgumentNullException("innerKeySelector");
            if (resultSelector == null) throw new ArgumentNullException("resultSelector");

            return JoinImpl(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer ?? EqualityComparer<TKey>.Default);
        }

        private static IEnumerable<TResult> JoinImpl<TOuter, TInner, TKey, TResult>(IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            var innerKeyLookup = inner.ToLookup(innerKeySelector, comparer);
            foreach (TOuter outerElement in outer)
            {
                var outerKey = outerKeySelector(outerElement);
                foreach (var innerElement in innerKeyLookup[outerKey])
                {
                    yield return resultSelector(outerElement, innerElement);
                }
            }
        }
    }
}
