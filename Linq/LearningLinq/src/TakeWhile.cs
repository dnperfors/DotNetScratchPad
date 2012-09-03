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
        public static IEnumerable<TSource> TakeWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (predicate == null) throw new ArgumentNullException("predicate");
            return TakeWhileImpl(source, predicate);
        }

        public static IEnumerable<TSource> TakeWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (predicate == null) throw new ArgumentNullException("predicate");

            return TakeWhileImpl(source, predicate);
        }

        private static IEnumerable<TSource> TakeWhileImpl<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            foreach (var element in source)
            {
                if (!predicate(element))
                    yield break;
                else
                    yield return element;
            }
        }

        private static IEnumerable<TSource> TakeWhileImpl<TSource>(IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            int index = 0;

            foreach (var element in source)
            {
                if (!predicate(element, index))
                    yield break;
                else
                    yield return element;

                index++;
            }
        }
    }
}
