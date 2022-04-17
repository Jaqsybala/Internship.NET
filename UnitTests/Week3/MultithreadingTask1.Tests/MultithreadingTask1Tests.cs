using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Week3.MultithreadingTask1;
using Xunit;

namespace UnitTests.Week3.MultithreadingTask1.Tests
{
    public class MultithreadingTask1Tests
    {
        static TypeOfMusic track = new();
        static List<TypeOfMusic> trackList = new();
        private static ManualResetEvent mre = new ManualResetEvent(false);

        [Fact]
        public void Test_Returns_Correct_TypeOfDanceMoves_DependingOn_TypeOfMusic()
        {
            //Arrange
            string res = "";

            for (int i = 0; i < 10; i++)
            {
                if (i >= 0 && i < 3)
                {
                    trackList.Add(TypeOfMusic.HARDBASS);
                }
                else if (i > 2 && i < 6)
                {
                    trackList.Add(TypeOfMusic.LATINO);
                }
                else
                {
                    trackList.Add(TypeOfMusic.ROCK);
                }
            }

            //Act          
            using (StringWriter s = new StringWriter())
            {
                Console.SetOut(s);

                for (int i = 1; i <= 1; i++)
                {
                    Thread dancer = new Thread(Print);
                    dancer.Name = "Dancer " + i;
                    dancer.Start();
                }

                SessionBegins();
                res = s.ToString();
                s.Close();
            }

            //Assert
            Assert.Equal("DJ begins the session!\r\n" +
                        "Currently playing: HARDBASS\r\n" +
                        "Dancer dancing ELBOW dance\r\n" +
                        "Currently playing: HARDBASS\r\n" +
                        "Dancer dancing ELBOW dance\r\n" +
                        "Currently playing: HARDBASS\r\n" +
                        "Dancer dancing ELBOW dance\r\n" +
                        "Currently playing: LATINO\r\n" +
                        "Dancer dancing HIPS dance\r\n" +
                        "Currently playing: LATINO\r\n" +
                        "Dancer dancing HIPS dance\r\n" +
                        "Currently playing: LATINO\r\n" +
                        "Dancer dancing HIPS dance\r\n" +
                        "Currently playing: ROCK\r\n" +
                        "Dancer dancing HEAD dance\r\n" +
                        "Currently playing: ROCK\r\n" +
                        "Dancer dancing HEAD dance\r\n" +
                        "Currently playing: ROCK\r\n" +
                        "Dancer dancing HEAD dance\r\n" +
                        "Currently playing: ROCK\r\n" +
                        "Dancer dancing HEAD dance\r\n" +
                        "Session ended.\r\n", res);
        }

        void Print()
        {
            for (int i = 0; i < trackList.Count; i++)
            {
                bool isSignaled = mre.WaitOne();
                Thread.Sleep(50);
                if (isSignaled)
                {
                    if (track.Equals(TypeOfMusic.HARDBASS))
                    {
                        Console.WriteLine("Dancer dancing {0} dance", TypeOfDanceMoves.ELBOW);
                    }

                    if (track.Equals(TypeOfMusic.LATINO))
                    {
                        Console.WriteLine("Dancer dancing {0} dance", TypeOfDanceMoves.HIPS);
                    }

                    if (track.Equals(TypeOfMusic.ROCK))
                    {
                        Console.WriteLine("Dancer dancing {0} dance", TypeOfDanceMoves.HEAD);
                    }
                    mre.Reset();
                }
            }
        }

        void SessionBegins()
        {
            Console.WriteLine($"DJ begins the session!");
            for (int i = 0; i < trackList.Count; i++)
            {
                track = trackList[i];
                Console.WriteLine($"Currently playing: {trackList[i]}");
                mre.Set();
                Thread.Sleep(100);
            }
            Console.WriteLine("Session ended.");
        }
    }
}