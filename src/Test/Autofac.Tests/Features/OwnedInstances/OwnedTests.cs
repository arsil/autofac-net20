using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Autofac.Features.OwnedInstances;

#if !NET20
using Moq;
#endif

namespace Autofac.Tests.Features.OwnedInstances
{
    [TestFixture]
    public class OwnedTests
    {
#if !NET20
        [Test]
        public void WhenInitialisedWithValue_ReturnsSameFromValueProperty()
        {
            var value = "Hello";
            var owned = new Owned<string>(value, new Mock<IDisposable>().Object);
            Assert.AreSame(value, owned.Value);
        }

        [Test]
        public void DisposingOwned_CallsDisposeOnLifetimeToken()
        {
            var lifetime = new Mock<IDisposable>();
            lifetime.Setup(l => l.Dispose()).Verifiable();
            var owned = new Owned<string>("unused", lifetime.Object);
            owned.Dispose();
            lifetime.VerifyAll();
        }

#else
        [Test]
        public void WhenInitialisedWithValue_ReturnsSameFromValueProperty()
        {
            var value = "Hello";
            var owned = new Owned<string>(value, new MockDisposable());
            Assert.AreSame(value, owned.Value);
        }

        [Test]
        public void DisposingOwned_CallsDisposeOnLifetimeToken()
        {
            var o = new MockDisposable();
            var owned = new Owned<string>("unused", o);
            owned.Dispose();
            Assert.AreEqual(1, o.disposed);
        }    
        
        class MockDisposable : IDisposable
        {
            void IDisposable.Dispose()
            {
                disposed++;
            }

            internal int disposed;
        }    
#endif        
    }
}
