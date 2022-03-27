namespace Week1.QueueTask
{
    public interface IQueue
    {
        void Enqueue(int input);
        int Dequeue();
        int Peek();
    }
}
