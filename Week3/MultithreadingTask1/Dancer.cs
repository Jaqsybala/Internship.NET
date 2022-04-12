namespace Week3.MultithreadingTask1
{
    public class Dancer
    {
        static TypeOfMusic track = new();
        static List<TypeOfMusic> trackList = new();
        static List<TypeOfMusic> shuffled = new();
        private static ManualResetEvent mre = new ManualResetEvent(false);
        static void Main(string[] args)
        {
            for (int i = 0; i < 20; i++)
            {
                if (i >= 0 && i < 6)
                {
                    trackList.Add(TypeOfMusic.HARDBASS);
                }
                else if (i > 5 && i < 12)
                {
                    trackList.Add(TypeOfMusic.LATINO);
                }
                else
                {
                    trackList.Add(TypeOfMusic.ROCK);
                }
            }

            shuffled = trackList
                .OrderBy(x => Guid.NewGuid()).ToList();

            for (int i = 1; i <= shuffled.Count; i++)
            {
                Thread dancer = new Thread(Print);
                dancer.Name = "Dancer " + i;
                dancer.Start();
            }

            SessionBegins();
        }

        static void Print()
        {
            for (int i = 0; i < shuffled.Count; i++)
            {
                bool isSignaled = mre.WaitOne();
                Thread.Sleep(500);
                if (isSignaled)
                {
                    if (track.Equals(TypeOfMusic.HARDBASS))
                    {
                        Console.WriteLine("Dancer dancing {0} dance - {1}", TypeOfDanceMoves.ELBOW, Thread.CurrentThread.Name);
                    }

                    if (track.Equals(TypeOfMusic.LATINO))
                    {
                        Console.WriteLine("Dancer dancing {0} dance - {1}", TypeOfDanceMoves.HIPS, Thread.CurrentThread.Name);
                    }

                    if (track.Equals(TypeOfMusic.ROCK))
                    {
                        Console.WriteLine("Dancer dancing {0} dance - {1}", TypeOfDanceMoves.HEAD, Thread.CurrentThread.Name);
                    }
                    mre.Reset();
                }
            }
        }

        static void SessionBegins()
        {
            Console.WriteLine($"DJ begins the session!");
            for (int i = 0; i < shuffled.Count; i++)
            {
                track = shuffled[i];
                Console.WriteLine($"Currently playing: {shuffled[i]} - {i}");
                mre.Set();
                Thread.Sleep(1000);
            }
            Thread.Sleep(1000);
            Console.WriteLine("Session ended.");
        }
    }
}
