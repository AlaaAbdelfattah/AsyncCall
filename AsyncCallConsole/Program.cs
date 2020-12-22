using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCallConsole
{
    class Program
    {
        #region Fields and Properties


        static bool IsExceptionPath = bool.Parse(ConfigurationManager.AppSettings["IsExceptionPath"]);
        static bool IsThread = bool.Parse(ConfigurationManager.AppSettings["IsThread"]);
        static bool IsSequential = bool.Parse(ConfigurationManager.AppSettings["IsSequential"]);
        static bool IsTask = bool.Parse(ConfigurationManager.AppSettings["IsTask"]);
        static bool IsAsyncTask = bool.Parse(ConfigurationManager.AppSettings["IsAsyncTask"]);

        #endregion

        #region Main

        static void Main(string[] args)
        {
            try
            {
                #region IsSequential 

                if (IsSequential)
                {
                    //Average Response Time : 11005
                    //Average Response Time : 11018
                    Console.WriteLine("Hello World! - Test Sequential Cases");
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    SequentialDelayApp(10);
                    stopwatch.Stop();
                    Console.WriteLine("Sequential Time Call :" + stopwatch.ElapsedMilliseconds);
                    Console.WriteLine("--------------------------------------------------------");
                    Console.WriteLine("Complete the main senario processing");
                    Stopwatch stopwatch2 = Stopwatch.StartNew();
                    for (int i = 0; i < 20; i++)
                    {
                        Console.WriteLine("Index of Complete the main senario processing: " + i);
                    }
                    stopwatch2.Stop();
                    Console.WriteLine("Main Senario processing Time Call :" + stopwatch2.ElapsedMilliseconds);
                    Console.WriteLine("--------------------------------------------------------");
                    Console.WriteLine("The total response time:" + (stopwatch2.ElapsedMilliseconds + stopwatch.ElapsedMilliseconds));
                }

                #endregion

                #region IsThread

                if (IsThread)
                {
                    #region UsingJoinFunction

                    var IsUsingJoinFunction = false;
                    var IsOnethread = false;
                    var IsMultiThreads = true;
                    if (IsUsingJoinFunction)
                    {
                        //Thread Average Response Time : 11009
                        //Total Average Response Time : 11020
                        Console.WriteLine("Hello World! - Test Thread Cases");
                        Stopwatch stopwatch = Stopwatch.StartNew();
                        Thread thread1 = new Thread(delegate () { ThreadDelayApp1(10); });
                        thread1.Start();
                        thread1.Join(); // to stop the processing  untill Task finished it's work
                        stopwatch.Stop();
                        Console.WriteLine("Thread Time Call :" + stopwatch.ElapsedMilliseconds);
                        Console.WriteLine("--------------------------------------------------------");
                        Console.WriteLine("Complete the main senario processing");
                        Stopwatch stopwatch2 = Stopwatch.StartNew();
                        for (int i = 0; i < 20; i++)
                        {
                            Console.WriteLine("Index of Complete the main senario processing: " + i);
                        }
                        stopwatch2.Stop();
                        Console.WriteLine("Main Senario processing Time Call :" + stopwatch2.ElapsedMilliseconds);
                        Console.WriteLine("--------------------------------------------------------");
                        Console.WriteLine("The total response time:" + (stopwatch2.ElapsedMilliseconds + stopwatch.ElapsedMilliseconds));
                    }
                    if (IsOnethread)
                    {
                        Console.WriteLine("Hello World! - Test One Thread Cases");
                        Thread thread1 = new Thread(delegate () { ThreadDelayApp1(5000); });
                        thread1.Start();
                        //thread1.Join(); // to stop the processing  untill Task finished it's work
                        Console.WriteLine("--------------------------------------------------------");
                        Console.WriteLine("Complete the main senario processing");
                        Stopwatch stopwatch2 = Stopwatch.StartNew();
                        for (int i = 0; i < 100; i++)
                        {
                            Console.WriteLine("Index of Complete the main senario processing: " + i);
                        }
                        stopwatch2.Stop();
                        Console.WriteLine("Main Senario processing Time Call :" + stopwatch2.ElapsedMilliseconds);
                        Console.WriteLine("--------------------------------------------------------");
                    }
                    if (IsMultiThreads)
                    {
                        Console.WriteLine("Hello World! - Test Multi Threads Cases AT:" + DateTime.Now);
                        //Clean Old File Data 
                        File.WriteAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\ThreadDelayApp1.txt", "");
                        File.WriteAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\ThreadDelayApp2.txt", "");
                        File.WriteAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\ThreadDelayApp3.txt", "");
                        //Call Threads
                        Thread thread1 = new Thread(delegate () { ThreadDelayApp1(2000); });
                        thread1.Start();
                        Thread thread2 = new Thread(delegate () { ThreadDelayApp2(2000); });
                        thread2.Start();
                        Thread thread3 = new Thread(delegate () { ThreadDelayApp3(2000); });
                        thread3.Start();
                        Console.WriteLine("--------------------------------------------------------");
                        Console.WriteLine("Complete the main senario processing");
                        Stopwatch stopwatch2 = Stopwatch.StartNew();
                        for (int i = 0; i < 20; i++)
                        {
                            Console.WriteLine("Index of Complete the main senario processing: " + i);
                        }
                        stopwatch2.Stop();
                        Console.WriteLine("Main Senario processing Time Call :" + stopwatch2.ElapsedMilliseconds + "At time" + DateTime.Now);
                        Console.WriteLine("--------------------------------------------------------");
                    }

                    #endregion
                }

                #endregion

                #region IsTask

                if (IsTask)
                {
                    Console.WriteLine("Hello World! - Test Task Cases" + DateTime.Now);
                    //Clean Old File Data 
                    File.WriteAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\TaskDelayApp1.txt", "");
                    File.WriteAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\TaskDelayApp2.txt", "");
                    File.WriteAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\TaskDelayApp3.txt", "");
                    //Call Tasks
                    Task task1 = new Task(delegate () { TaskDelayApp1(2500); });
                    task1.Start();
                    //task1.Wait();
                    Task task2 = new Task(delegate () { TaskDelayApp2(2500); });
                    task2.Start();
                    //task2.Wait();
                    Task task3 = new Task(delegate () { TaskDelayApp3(2500); });
                    task3.Start();
                    //task3.Wait();
                    Console.WriteLine("--------------------------------------------------------");
                    Console.WriteLine("Complete the main senario processing");
                    Stopwatch stopwatch2 = Stopwatch.StartNew();
                    for (int i = 0; i < 20; i++)
                    {
                        Console.WriteLine("Index of Complete the main senario processing: " + i);
                    }
                    stopwatch2.Stop();
                    Console.WriteLine("Main Senario processing Time Call :" + stopwatch2.ElapsedMilliseconds + "At time" + DateTime.Now);
                    Console.WriteLine("--------------------------------------------------------");
                    Console.ReadLine();
                }

                #endregion

                #region IsAsyncTask

                if (IsAsyncTask)
                {
                    //Clean Logs Files 
                    Console.WriteLine("Hello World! - Test Async Cases" + DateTime.Now);
                    File.WriteAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\AsyncTaskApp1.txt", "");
                    File.WriteAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\AsyncTaskApp2.txt", "");
                    //Async Task 1
                    Stopwatch stopwatch1 = Stopwatch.StartNew();
                    File.AppendAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\AsyncTaskApp1.txt", " ,asyncTask1 started at: " + DateTime.Now + Environment.NewLine);
                    Task asyncTask1 = new Task(delegate () { Mytask1("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\AsyncTask.txt"); });
                    asyncTask1.Start();
                    stopwatch1.Stop();
                    File.AppendAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\AsyncTaskApp1.txt", " ,asyncTask1 ended at: " + DateTime.Now + Environment.NewLine);
                    File.AppendAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\AsyncTaskApp1.txt", " ,asyncTask1 response time: " + stopwatch1.ElapsedMilliseconds + Environment.NewLine);
                    //Async Task 2 
                    Stopwatch stopwatch2 = Stopwatch.StartNew();
                    File.AppendAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\AsyncTaskApp2.txt", " ,asyncTask2 started at: " + DateTime.Now + Environment.NewLine);
                    Task asyncTask2 = new Task(delegate () { Mytask2("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\AsyncTask.txt"); });
                    asyncTask2.Start();
                    stopwatch2.Stop();
                    File.AppendAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\AsyncTaskApp2.txt", " ,asyncTask2 ended at: " + DateTime.Now + Environment.NewLine);
                    File.AppendAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\AsyncTaskApp2.txt", " ,asyncTask2 response time: " + stopwatch2.ElapsedMilliseconds + Environment.NewLine);
                    //Main Scenario 
                    Console.WriteLine("---------------------------------------------------");
                    Console.WriteLine("Main senario Started At: " + DateTime.Now);
                    Stopwatch stopwatch3 = Stopwatch.StartNew();
                    for (int i = 0; i < 400; i++)
                    {
                        Console.WriteLine("Index Of Async Main Senario: " + i);
                    }
                    stopwatch3.Stop();
                    Console.WriteLine("Main senario Ended At: " + DateTime.Now);
                    Console.WriteLine("Main scenario Response Time:" + stopwatch3.ElapsedMilliseconds);
                    Console.WriteLine("---------------------------------------------------");
                    Console.ReadLine();
                }

                #endregion

            }
            catch (Exception ex)
            {
                File.AppendAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\LogFile.txt", ex.Message);
            }
        }

        #endregion

        #region IsAsyncTask

        static async void Mytask1(string filePath)
        {
            var counter1 = await ReadFromFile1(filePath);
            Console.WriteLine("Mytask1 File Data Count:" + (counter1));
        }

        static async void Mytask2(string filePath)
        {
            var counter1 = await ReadFromFile2(filePath);
            Console.WriteLine("Mytask2 File Data Count:" + (counter1));
        }

        static async Task<int> ReadFromFile1(string filePath)
        {
            int count = 0;
            using (StreamReader stream = new StreamReader(filePath))
            {
                string fileData = await stream.ReadToEndAsync();
                count += fileData.Length;
            }
            return count;
        }

        static async Task<int> ReadFromFile2(string filePath)
        {
            int count = 0;
            using (StreamReader stream = new StreamReader(filePath))
            {
                string fileData = await stream.ReadToEndAsync();
                count += fileData.Length;
            }
            return count;
        }


        #endregion

        #region IsSequential 

        static void SequentialDelayApp(int i)
        {
            Thread.Sleep(1000);
            if (i > 0)
            {
                SequentialDelayApp(i - 1);
            }
        }

        #endregion

        #region IsThread

        static void ThreadDelayApp1(int i)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            if (i > 0)
            {
                ThreadDelayApp1(i - 1);
            }
            stopwatch.Stop();
            File.AppendAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\ThreadDelayApp1.txt", "AsyncDelayApp1 Finished At: "
                + stopwatch.ElapsedMilliseconds + "  At Current time:  " + DateTime.Now + Environment.NewLine);
        }

        static void ThreadDelayApp2(int i)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            if (i > 0)
            {
                ThreadDelayApp2(i - 1);
            }
            stopwatch.Stop();
            File.AppendAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\ThreadDelayApp2.txt", "AsyncDelayApp2 Finished At: "
                + stopwatch.ElapsedMilliseconds + "  At Current time:  " + DateTime.Now + Environment.NewLine);
        }

        static void ThreadDelayApp3(int i)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            if (i > 0)
            {
                ThreadDelayApp3(i - 1);
            }
            stopwatch.Stop();
            File.AppendAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\ThreadDelayApp3.txt", "AsyncDelayApp3 Finished At: "
                + stopwatch.ElapsedMilliseconds + "  At Current time:  " + DateTime.Now + Environment.NewLine);
        }

        #endregion

        #region Task

        static int TaskDelayApp1(int i)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            if (i > 0)
            {
                TaskDelayApp1(i - 1);
            }
            stopwatch.Stop();
            File.AppendAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\TaskDelayApp1.txt", "AsyncDelayApp1 Finished At: "
                + stopwatch.ElapsedMilliseconds + "  At Current time:  " + DateTime.Now + Environment.NewLine);
            return 1;
        }

        static void TaskDelayApp2(int i)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            if (i > 0)
            {
                TaskDelayApp2(i - 1);
            }
            stopwatch.Stop();
            File.AppendAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\TaskDelayApp2.txt", "TaskDelayApp2 Finished At: "
                + stopwatch.ElapsedMilliseconds + "  At Current time:  " + DateTime.Now + Environment.NewLine);
        }

        static void TaskDelayApp3(int i)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            if (i > 0)
            {
                TaskDelayApp3(i - 1);
            }
            stopwatch.Stop();
            File.AppendAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\TaskDelayApp3.txt", "AsyncDelayApp3 Finished At: "
                + stopwatch.ElapsedMilliseconds + "  At Current time:  " + DateTime.Now + Environment.NewLine);
        }

        #endregion

        #region Helpers

        private static void FileLogger(NLog.LogType logType, string logValue)
        {
            NlogManager.Log(logType, logValue);
        }

        #endregion


    }
}
