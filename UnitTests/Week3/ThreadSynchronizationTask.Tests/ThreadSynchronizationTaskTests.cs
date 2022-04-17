using System;
using System.IO;
using System.Threading;
using Xunit;

namespace UnitTests.Week3.ThreadSynchronizationTask.Tests
{
    public class ThreadSynchronizationTaskTests
    {
        private static ManualResetEvent mre = new(false);
        private static AutoResetEvent are = new(false);

        [Fact]
        public void Test_Returns_CorrectSequenseOfMessages_ToTheConsole()
        {
            //arrange
            string res = "";

            //act
            using (StringWriter s = new StringWriter())
            {
                Console.SetOut(s);

                for (int i = 1; i <= 2; i++)
                {
                    Thread thread = new Thread(AutoManual);
                    thread.Name = "Thread " + i;
                    Thread.Sleep(10);
                    thread.Start();
                }

                for (int i = 3; i <= 4; i++)
                {
                    Thread manualThread = new Thread(ManualThread);
                    manualThread.Name = "Thread " + i;
                    manualThread.Start();
                }

                for (int i = 5; i <= 6; i++)
                {
                    Thread autoThread = new Thread(AutoThread);
                    autoThread.Name = "Thread " + i;
                    autoThread.Start();
                }

                Thread.Sleep(250);

                res = s.ToString();

                s.Close();
            }

            //assert
            Assert.Equal("Thread 1 started\r\n" +
                         "Thread 2 started\r\n" +
                         "Thread 3 is waiting for a manual signal from Thread 1\r\n" +
                         "Thread 4 is waiting for a manual signal from Thread 1\r\n" +
                         "Thread 5 is waiting for an auto signal from Thread 2\r\n" +
                         "Thread 6 is waiting for an auto signal from Thread 2\r\n" +
                         "Thread 2 set signal\r\n" +
                         "Thread 5 received an auto signal, continue working\r\n" +
                         "Thread 1 set signal\r\n" +
                         "Thread 3 received a manual signal, continue working\r\n" +
                         "Thread 4 received a manual signal, continue working\r\n" +
                         "Thread 1 reset signal\r\n" +
                         "Thread 2 set signal\r\n" +
                         "Thread 6 received an auto signal, continue working\r\n", res);

        }

        void AutoManual()
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

        void ManualThread()
        {
            string? name = Thread.CurrentThread.Name;
            Console.WriteLine($"{name} is waiting for a manual signal from Thread 1");
            bool isSignaled = mre.WaitOne();
            if (isSignaled)
            {
                if (name.Equals("Thread 4"))
                {
                    Thread.Sleep(10);
                }
                Console.WriteLine($"{name} received a manual signal, continue working");
            }
        }

        void AutoThread()
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
