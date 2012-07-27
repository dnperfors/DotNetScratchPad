using System;
using System.Collections.Generic;

namespace LearningLinq
{
    public static partial class Enumerable
    {
        public static IEnumerable<TResult> Empty<TResult>()
        {
            return EmptyEnumerable<TResult>.Instance;
        }

        class EmptyEnumerable<TResult>
        {
            public static TResult[] Instance = new TResult[0];
        }
    }
}
