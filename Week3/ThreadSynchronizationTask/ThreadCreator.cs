namespace Week3.ThreadSynchronizationTask
{
    public class ThreadCreator
    {
        private static ManualResetEvent mre = new ManualResetEvent(false);
        private static AutoResetEvent are = new AutoResetEvent(false);
        //static void Main(string[] args)
        //{
        //    for (int i = 1; i <= 2; i++)
        //    {
        //        Thread thread = new Thread(AutoManual);
        //        thread.Name = "Thread " + i;
        //        Thread.Sleep(10);
        //        thread.Start();
        //    }

        //    for (int i = 3; i <= 4; i++)
        //    {
        //        Thread manualThread = new Thread(ManualThread);
        //        manualThread.Name = "Thread " + i;
        //        manualThread.Start();
        //    }

        //    for (int i = 5; i <= 6; i++)
        //    {
        //        Thread autoThread = new Thread(AutoThread);
        //        autoThread.Name = "Thread " + i;
        //        autoThread.Start();
        //    }
        //}

        static void AutoManual()
        {
            string? name = Thread.CurrentThread.Name;
            Console.WriteLine($"{name} started");
            if (name.Equals("Thread 1"))
            {
                Thread.Sleep(100);
                Console.WriteLine($"{name} set signal");
                mre.Set();

                Thread.Sleep(40);
                Console.WriteLine($"{name} reset signal");
                mre.Reset();
            }
            else
            {
                for (int i = 0; i <= 1; i++)
                {
                    Thread.Sleep(70);
                    Console.WriteLine($"{name} set signal");
                    are.Set();
                }
            }
        }

        static void ManualThread()
        {
            string? name = Thread.CurrentThread.Name;
            Console.WriteLine($"{name} is waiting for a manual signal from Thread 1");
            mre.WaitOne();
            if (name.Equals("Thread 4"))
            {
                Thread.Sleep(10);
            }
            Console.WriteLine($"{name} received a manual signal, continue working");
        }

        static void AutoThread()
        {
            string? name = Thread.CurrentThread.Name;
            Console.WriteLine($"{name} is waiting for an auto signal from Thread 2");
            bool isSignaled = are.WaitOne();
            if (isSignaled)
            {
                Console.WriteLine($"{name} received an auto signal, continue working");
            }
        }
    }
}
