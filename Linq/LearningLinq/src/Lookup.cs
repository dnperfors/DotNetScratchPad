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
    sealed class Lookup<TKey, TElement> : ILookup<TKey, TElement>
    {
        class Grouping<TGroupingKey, TGroupingElement> : IGrouping<TGroupingKey, TGroupingElement>
        {
            private readonly TGroupingKey key;
            private readonly List<TGroupingElement> elements = new List<TGroupingElement>();

            internal Grouping(TGroupingKey key)
            {
                this.key = key;
            }

            internal void Add(TGroupingElement element)
            {
                elements.Add(element);
            }

            public TGroupingKey Key { get { return key; } }

            public IEnumerator<TGroupingElement> GetEnumerator()
            {
                return elements.Select(x => x).GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private readonly List<Grouping<TKey, TElement>> groupings = new List<Grouping<TKey, TElement>>();
        private readonly IEqualityComparer<TKey> comparer;

        internal Lookup(IEqualityComparer<TKey> comparer)
        {
            this.comparer = comparer;
        }

        internal void Add(TKey key, TElement element)
        {
            Grouping<TKey, TElement> grouping = FindGrouping(key, true);
            grouping.Add(element);
        }

        private Grouping<TKey, TElement> FindGrouping(TKey key, bool createNew)
        {
            Grouping<TKey, TElement> grouping = groupings.SingleOrDefault(x => comparer.Equals(key, x.Key));
            if (grouping == null && createNew)
            {
                grouping = new Grouping<TKey, TElement>(key);
                groupings.Add(grouping);
            }
            return grouping;
        }

        public int Count
        {
            get { return groupings.Count; }
        }

        public IEnumerable<TElement> this[TKey key]
        {
            get
            {
                IEnumerable<TElement> result = FindGrouping(key, false);
                if (result == null)
                    return Enumerable.Empty<TElement>();
                return result;
            }
        }

        public bool Contains(TKey key)
        {
            return FindGrouping(key, false) != null;
        }

        public IEnumerator<IGrouping<TKey, TElement>> GetEnumerator()
        {
            return groupings.Select(x => (IGrouping<TKey, TElement>)x).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
