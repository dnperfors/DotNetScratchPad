﻿///   Copyright 2012 David Perfors
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
        public static TSource[] ToArray<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) throw new ArgumentNullException("source");

            ICollection<TSource> collection = source as ICollection<TSource>;
            if (collection != null)
            {
                TSource[] quickResult = new TSource[collection.Count];
                collection.CopyTo(quickResult, 0);
                return quickResult;
            }

            TSource[] result = new TSource[16];
            int i = 0;

            foreach (var item in source)
            {
                result[i] = item;
                i++;
                if (i == result.Length)
                {
                    TSource[] oldResult = result;
                    result = new TSource[i * 2];
                    oldResult.CopyTo(result, 0);
                }
            }

            if (i != result.Length)
                Array.Resize(ref result, i);

            return result;
        }
    }
}