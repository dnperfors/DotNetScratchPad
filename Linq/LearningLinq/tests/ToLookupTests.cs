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
using LearningLinq;
using Xunit;

namespace LearningLinqTests
{
    public class ToLookupTests
    {
        [Fact]
        public void ToLookUp_sourceIsNull_ThrowArgumentException()
        {
            IEnumerable<string> source = null;
            Assert.Throws<ArgumentNullException>(() => source.ToLookup(x => x));
            Assert.Throws<ArgumentNullException>(() => source.ToLookup(x => x, StringComparer.OrdinalIgnoreCase));
            Assert.Throws<ArgumentNullException>(() => source.ToLookup(x => x, e => e[0]));
            Assert.Throws<ArgumentNullException>(() => source.ToLookup(x => x, e => e[0], StringComparer.OrdinalIgnoreCase));
        }

        [Fact]
        public void ToLookup_keySelectorIsNull_ThrowArgumentException()
        {
            string[] source = { "abc", "ABC", "def" };
            Func<string, string> keySelector = null;

            Assert.Throws<ArgumentNullException>(() => source.ToLookup(keySelector));
            Assert.Throws<ArgumentNullException>(() => source.ToLookup(keySelector, StringComparer.OrdinalIgnoreCase));
            Assert.Throws<ArgumentNullException>(() => source.ToLookup(keySelector, e => e[0]));
            Assert.Throws<ArgumentNullException>(() => source.ToLookup(keySelector, e => e[0], StringComparer.OrdinalIgnoreCase));
        }

        [Fact]
        public void ToLookup_elementSelectorIsNull_ThrowArgumentException()
        {
            string[] source = { "abc", "ABC", "def" };
            Func<string, string> elementSelector = null;

            Assert.Throws<ArgumentNullException>(() => source.ToLookup(x => x, elementSelector));
            Assert.Throws<ArgumentNullException>(() => source.ToLookup(x => x, elementSelector, StringComparer.OrdinalIgnoreCase));
        }

        [Fact]
        public void ToLookup_NoElementSelectorNoComparer_ReturnCorrectLookup()
        {
            string[] source = { "abc", "def", "x", "y", "ghi", "z", "00" };

            var lookup = source.ToLookup(x => x.Length);
            Assert.Equal(new string[] { "x", "y", "z" }, lookup[1]);
            Assert.Equal(new string[] { "00" }, lookup[2]);
            Assert.Equal(new string[] { "abc", "def", "ghi" }, lookup[3]);
        }

        [Fact]
        public void ToLookup_ComplexSourceElementSelectorAndComparer_returnCorrectLookup()
        {
            var source = new[]
            {
                new { First = "Jan", Last = "Jansen" },
                new { First = "Klaas", Last = "Willemsen" },
                new { First = "Sjaakie", Last = (string)null },
                new { First ="Miep", Last = "JANSEN"},
            };

            var lookup = source.ToLookup(x => x.Last, x => x.First, StringComparer.OrdinalIgnoreCase);
            Assert.Equal(new string[] { "Sjaakie" }, lookup[null]);
            Assert.Equal(new string[] { "Jan", "Miep" }, lookup["Jansen"]);
            Assert.Equal(new string[] { "Klaas" }, lookup["Willemsen"]);
            Assert.Empty(lookup["Klaassen"]);
        }
    }
}
