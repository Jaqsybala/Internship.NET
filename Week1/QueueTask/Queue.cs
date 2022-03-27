namespace Week1.QueueTask
{
    public class Queue : IQueue
    {
        private List<int> queue = new List<int>();
        
        //For unit tests
        public int this[int index]
        { 
            get 
            { 
                if (index < 0 || index >= queue.Count) throw new IndexOutOfRangeException();
                return queue[index]; 
            }
            set 
            {
                if (index < 0 || index >= queue.Count) throw new IndexOutOfRangeException();
                queue[index] = value; 
            }
        }

        //For unit tests
        public int Count() => queue.Count;

        public int Dequeue()
        {
            if (queue.Count == 0) throw new InvalidOperationException("Queue has no elements");
            var result = queue[0];
            queue.RemoveAt(0);
            return result;
        }

        public void Enqueue(int input) => queue.Add(input);

        public int Peek()
        {
            if (queue.Count == 0) throw new InvalidOperationException("Queue has no elements");
            return queue[0];
        }
    }
}
