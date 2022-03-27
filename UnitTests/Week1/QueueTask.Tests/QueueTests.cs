using Week1.QueueTask;
using Xunit;

namespace UnitTests.Week1.QueueTask.Tests
{
    public class QueueTests
    {
        [Fact]
        public void Enqueue_Add_Elements_In_Queue()
        { 
            var queue = new Queue();

            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            Assert.Equal(3, queue.Count());
        }

        [Fact]
        public void Dequeue_Deletes_First_Element_In_Queue()
        {
            var queue = new Queue();

            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            var result = queue.Dequeue();

            Assert.Equal(1, result);
        }

        [Fact]
        public void Peek_Returns_First_Element_In_Queue()
        {
            var queue = new Queue();

            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            var result = queue.Peek();

            Assert.Equal(1, result);
        }
    }
}
