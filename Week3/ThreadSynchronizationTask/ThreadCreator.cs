namespace Week3.ThreadSynchronizationTask
{
    public class ThreadCreator
    {
        private static ManualResetEvent mre = new ManualResetEvent(false);
        private static AutoResetEvent are = new AutoResetEvent(false);
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(ManualThread);
            thread1.Name = "Thread 1";
            thread1.Start();

            Thread.Sleep(1000);

            Thread thread2 = new Thread(AutoThread);
            thread2.Name = "Thread 2";
            thread2.Start();

            Thread.Sleep(1000);

            for (int i = 3; i <= 4; i++)
            {
                Thread manualThread = new Thread(ManualThread);
                manualThread.Name = "Thread " + i;
                manualThread.Start();
            }

            Thread.Sleep(1500);

            for (int i = 5; i <= 6; i++)
            {
                Thread autoThread = new Thread(AutoThread);
                autoThread.Name = "Thread " + i;
                autoThread.Start();
            }

            Thread.Sleep(1500);
            Console.WriteLine("Thread 2 set signal");
            are.Set();

            Thread.Sleep(1500);
            Console.WriteLine("Thread 1 set signal");
            mre.Set();

            Thread.Sleep(1500);
            Console.WriteLine("Thread 1 reset signal");
            mre.Reset();

            Thread.Sleep(1500);
            Console.WriteLine("Thread 2 set signal");
            are.Set();
        }

        static void ManualThread()
        {
            if (Thread.CurrentThread.Name.Equals("Thread 1")) Console.WriteLine($"{Thread.CurrentThread.Name} started");
            else
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} is waiting for a manual signal from Thread 1");
                bool isSignaled = mre.WaitOne();
                if (isSignaled)
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name} received a manual signal, continue working");
                }
            }
        }

        static void AutoThread()
        {
            if (Thread.CurrentThread.Name.Equals("Thread 2")) Console.WriteLine($"{Thread.CurrentThread.Name} started");
            else
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} is waiting for an auto signal from Thread 2");
                bool isSignaled = are.WaitOne();
                if (isSignaled)
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name} received an auto signal, continue working");
                }
            }
        }
    }
}
