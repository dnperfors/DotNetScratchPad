///   Copyright 2012 David Perfors
///
///   Licensed under the Apache License, Version 2.0 (the "License");
///   you may not use this file except in compliance with the License.
///   You may obtain a copy of the License at
///
///       http://www.apache.org/licenses/LICENSE-2.0
///
///   Unless required by applicable law or agreed to in writing, software
///   distributed under the License is distributed on an "AS IS" BASIS,
///   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
///   See the License for the specific language governing permissions and
///   limitations under the License.

using System;
using System.Collections.Generic;

namespace LearningLinq
{
    public static partial class Enumerable
    {
        enum Fallback
        {
            Throw,
            Default
        }

        static TSource Single<TSource>(IEnumerable<TSource> source, Fallback fallback)
        {
            using (IEnumerator<TSource> enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    if (fallback == Fallback.Throw)
                        throw new InvalidOperationException("Sequence is empty");
                    else
                        return default(TSource);
                }

                TSource result = enumerator.Current;
                if (enumerator.MoveNext())
                    throw new InvalidOperationException("More than one element");
                return result;
            }
        }

        public static TSource Single<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) throw new ArgumentNullException("source");

            return Single(source, Fallback.Throw);
        }

        public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) throw new ArgumentNullException("source");

            return Single(source, Fallback.Default);
        }

        static TSource Single<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate, Fallback fallback)
        {
            TSource result = default(TSource);
            bool foundAny = false;

            foreach (TSource element in source)
            {
                if (!predicate(element))
                    continue;

                if (foundAny)
                    throw new InvalidOperationException("More than one element");

                result = element;
                foundAny = true;
            }

            if (!foundAny && fallback == Fallback.Throw)
                throw new InvalidOperationException("No element found");
            return result;
        }

        public static TSource Single<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (predicate == null) throw new ArgumentNullException("predicate");

            return Single(source, predicate, Fallback.Throw);
        }

        public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (predicate == null) throw new ArgumentNullException("predicate");

            return Single(source, predicate, Fallback.Default);
        }
    }
}
