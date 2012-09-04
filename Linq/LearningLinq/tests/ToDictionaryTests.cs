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
    public class ToDictionaryTests
    {
        [Fact]
        public void ToDictionary_NullSource_ThrowArgumentNullException()
        {
            string[] source = null;

            Assert.Throws<ArgumentNullException>(() => source.ToDictionary(x => x));
            Assert.Throws<ArgumentNullException>(() => source.ToDictionary(x => x, StringComparer.OrdinalIgnoreCase));
            Assert.Throws<ArgumentNullException>(() => source.ToDictionary(x => x, x => x));
            Assert.Throws<ArgumentNullException>(() => source.ToDictionary(x => x, x => x, StringComparer.OrdinalIgnoreCase));
        }

        [Fact]
        public void ToDictionary_NullKeySelector_ThrowArgumentNullException()
        {
            string[] source = { };
            Func<string, string> keySelector = null;
            Assert.Throws<ArgumentNullException>(() => source.ToDictionary(keySelector));
            Assert.Throws<ArgumentNullException>(() => source.ToDictionary(keySelector, StringComparer.OrdinalIgnoreCase));
            Assert.Throws<ArgumentNullException>(() => source.ToDictionary(keySelector, x => x));
            Assert.Throws<ArgumentNullException>(() => source.ToDictionary(keySelector, x => x, StringComparer.OrdinalIgnoreCase));
        }

        [Fact]
        public void ToDictionary_NullElementSelector_ThrowArgumentNullException()
        {
            string[] source = { };
            Func<string, string> elementSelector = null;
            Assert.Throws<ArgumentNullException>(() => source.ToDictionary(x => x, elementSelector));
            Assert.Throws<ArgumentNullException>(() => source.ToDictionary(x => x, elementSelector, StringComparer.OrdinalIgnoreCase));
        }

        [Fact]
        public void ToDictionary_NullComparer_DoesNotThrowException()
        {
            string[] source = { };
            IEqualityComparer<string> comparer = null;

            Assert.DoesNotThrow(() => source.ToDictionary(x => x, comparer));
            Assert.DoesNotThrow(() => source.ToDictionary(x => x, x => x, comparer));
        }

        [Fact]
        public void ToDictionary_OnlyKeySelector_CorrectResult()
        {
            string[] source = { "zero", "one", "two" };
            Dictionary<char, string> expectedDictionary = new Dictionary<char, string> { { 'z', "zero" }, { 'o', "one" }, { 't', "two" } };

            var result = source.ToDictionary(x => x[0]);
            
            Assert.Equal(expectedDictionary, result);
        }

        [Fact]
        public void ToDictionary_KeySelectorNullComparer_CorrectResult()
        {
            string[] source = { "zero", "one", "two" };
            Dictionary<char, string> expectedDictionary = new Dictionary<char, string> { { 'z', "zero" }, { 'o', "one" }, { 't', "two" } };

            var result = source.ToDictionary(x => x[0], null);

            Assert.Equal(expectedDictionary, result);
        }

        [Fact]
        public void ToDictionary_KeySelectorElementSelector_CorrectResult()
        {
            string[] source = { "zero", "one", "two" };
            Dictionary<char, int> expectedDictionary = new Dictionary<char, int> { { 'z', 4 }, { 'o', 3 }, { 't', 3 } };

            var result = source.ToDictionary(x => x[0], x => x.Length);

            Assert.Equal(expectedDictionary, result);
        }

        [Fact]
        public void ToDictionary_KeySelectorElementSelectorNullComparer_CorrectResult()
        {
            string[] source = { "zero", "one", "two" };
            Dictionary<char, int> expectedDictionary = new Dictionary<char, int> { { 'z', 4 }, { 'o', 3 }, { 't', 3 } };

            var result = source.ToDictionary(x => x[0], x => x.Length, null);

            Assert.Equal(expectedDictionary, result);
        }

        [Fact]
        public void ToDictionary_KeySelectorElementSelectorCustomComparer_CorrectResult()
        {
            string[] source = { "zero", "one", "two" };

            var result = source.ToDictionary(x => x.Substring(0, 1), x => x.Length, StringComparer.OrdinalIgnoreCase);

            Assert.Equal(4, result["z"]);
            Assert.Equal(3, result["O"]);
            Assert.Equal(3, result["T"]);
        }

        [Fact]
        public void ToDictionary_DuplicateKeys_ThrowArgumentException()
        {
            string[] source = { "a", "b",  "a" };
            Assert.Throws<ArgumentException>(() => source.ToDictionary(x => x));
        }

        [Fact]
        public void ToDictionary_DuplicateKeysCustomComparer_ThrowArgumentException()
        {
            string[] source = { "a", "b", "A" };
            Assert.Throws<ArgumentException>(() => source.ToDictionary(x => x, StringComparer.OrdinalIgnoreCase));
        }

        [Fact]
        public void ToDictionary_NullKey_ThrowArgumentNullException()
        {
            string[] source = { "a", "b", null };
            Assert.Throws<ArgumentNullException>(() => source.ToDictionary(x => x));
        }

        [Fact]
        public void ToDictionary_NullValue_DoesNotThrowException()
        {
            string[] source = { "a", "b" };
            Dictionary<string, string> expectedResult = new Dictionary<string, string>() { { "a", null }, { "b", null } };

            Dictionary<string, string> result = source.ToDictionary(x => x, x => (string)null);
            
            Assert.Equal(expectedResult, result);
        }
    }
}
