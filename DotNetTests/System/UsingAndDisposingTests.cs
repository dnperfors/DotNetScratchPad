﻿using System;
using Xunit;

namespace DotNetTests.System
{
    public class UsingAndDisposingTests
    {
        [Fact]
        public void Using_DisposableField_DisposeOldValue()
        {
            DisposableObject object1 = new DisposableObject();
            DisposableObject object1Copy = object1;

            using (object1)
                object1 = new DisposableObject();

            Assert.True(object1Copy.IsDisposed);
        }

        private class DisposableObject : IDisposable
        {
            public bool IsDisposed = false;

            public void Dispose()
            {
                IsDisposed = true;
            }
        }
    }
}
