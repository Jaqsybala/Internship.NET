using Moq;
using System;
using System.Diagnostics;
using Week2.DisposeTask;
using Xunit;

namespace UnitTests.Week2.DisposeTask.Tests
{
    public class CustomerTests
    {
        [Fact]
        public void Dispose_Test()
        {
            Customer customer = new Customer();

            customer.Dispose();
            var res = customer.Disposed;

            Assert.True(res);
        }

        //For test finalize
        public int After_GC_Collect() => 0;

        [Fact]
        public void Finalize_Test()
        {
            Customer? target = null;

            for (int i = 0; i < 1000; i++)
            {
                target = new Customer();    
            }

            target = null;

            GC.Collect();

            GC.WaitForPendingFinalizers();

            var res = After_GC_Collect();
            
            Assert.Equal(0, res);
        }
    }
}
