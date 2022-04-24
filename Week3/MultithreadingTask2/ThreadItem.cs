namespace Week3.MultithreadingTask2
{
    public class ThreadItem
    {
        public HttpClient Client { get; set; } = new HttpClient();
        public int Start { get; set; }
        public int End { get; set; }
    }
}
