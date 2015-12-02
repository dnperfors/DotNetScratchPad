using System;
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
            {
#pragma warning disable CS0728 // Possibly incorrect assignment to local which is the argument to a using or lock statement
                object1 = new DisposableObject();
#pragma warning restore CS0728 // Possibly incorrect assignment to local which is the argument to a using or lock statement
            }

            Assert.True(object1Copy.IsDisposed);
        }

        [Fact]
        public void Using_supports_null()
        {
            DisposableObject object1 = null;
            using (object1)
            {
            }
            Assert.True(typeof(IDisposable).IsAssignableFrom(typeof(DisposableObject)));
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
