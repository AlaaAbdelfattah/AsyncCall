using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCallConsole
{
    public class Program
    {

        #region Fields and Properties

        static readonly bool IsExceptionPath = bool.Parse(ConfigurationManager.AppSettings["IsExceptionPath"]);
        static readonly bool IsThread = bool.Parse(ConfigurationManager.AppSettings["IsThread"]);
        static readonly bool IsSequential = bool.Parse(ConfigurationManager.AppSettings["IsSequential"]);
        static readonly bool IsTask = bool.Parse(ConfigurationManager.AppSettings["IsTask"]);
        static readonly bool IsAsyncTask = bool.Parse(ConfigurationManager.AppSettings["IsAsyncTask"]);
        //Parallel Invoke 
        static readonly bool IsParallel_Invoke_Void = bool.Parse(ConfigurationManager.AppSettings["IsParallel_Invoke_Void"]);
        static readonly bool IsParallel_Invoke_ReturnValue = bool.Parse(ConfigurationManager.AppSettings["IsParallel_Invoke_ReturnValue"]);
        //Parallel For 
        static readonly bool IsParallel_For_Void = bool.Parse(ConfigurationManager.AppSettings["IsParallel_For_Void"]);
        static readonly bool IsParallel_For_ReturnValue = bool.Parse(ConfigurationManager.AppSettings["IsParallel_For_ReturnValue"]);
        //Parallel Foreach 
        static readonly bool IsParallel_Foreach_Void = bool.Parse(ConfigurationManager.AppSettings["IsParallel_Foreach_Void"]);
        static readonly bool IsParallel_Foreach_ReturnValue = bool.Parse(ConfigurationManager.AppSettings["IsParallel_Foreach_ReturnValue"]);

        #endregion

        #region Main

        static void Main(string[] args)
        {
            try
            {
                #region Sequential 

                if (IsSequential)
                {
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

                #region Thread

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

                #region Task

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

                #region Async

                if (IsAsyncTask)
                {
                    //Clean Logs Files 
                    Console.WriteLine("Hello World! - Test Async Cases" + DateTime.Now);
                    //File.WriteAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\AsyncTaskApp1.txt", "");
                    //File.WriteAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\AsyncTaskApp2.txt", "");
                    ////Async Task 1
                    //Stopwatch stopwatch1 = Stopwatch.StartNew();
                    //File.AppendAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\AsyncTaskApp1.txt", " ,asyncTask1 started at: " + DateTime.Now + Environment.NewLine);
                    //Task asyncTask1 = new Task(delegate () { Mytask1("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\AsyncTask.txt"); });
                    //asyncTask1.Start();
                    //stopwatch1.Stop();
                    //File.AppendAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\AsyncTaskApp1.txt", " ,asyncTask1 ended at: " + DateTime.Now + Environment.NewLine);
                    //File.AppendAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\AsyncTaskApp1.txt", " ,asyncTask1 response time: " + stopwatch1.ElapsedMilliseconds + Environment.NewLine);
                    ////Async Task 2 
                    //Stopwatch stopwatch2 = Stopwatch.StartNew();
                    //File.AppendAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\AsyncTaskApp2.txt", " ,asyncTask2 started at: " + DateTime.Now + Environment.NewLine);
                    //Task asyncTask2 = new Task(delegate () { Mytask2("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\AsyncTask.txt"); });
                    //asyncTask2.Start();
                    //stopwatch2.Stop();
                    //File.AppendAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\AsyncTaskApp2.txt", " ,asyncTask2 ended at: " + DateTime.Now + Environment.NewLine);
                    //File.AppendAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\AsyncTaskApp2.txt", " ,asyncTask2 response time: " + stopwatch2.ElapsedMilliseconds + Environment.NewLine);
                    //Main Scenario 

                    Task asyncTask2 = new Task(delegate () { CallAsyncMethodAsAforeachForOneAsyncMethod("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\AsyncTask.txt"); });
                    asyncTask2.Start();
                    Console.WriteLine("---------------------------------------------------");
                    Console.WriteLine("Main senario Started At: " + DateTime.Now);
                    Stopwatch stopwatch3 = Stopwatch.StartNew();
                    for (int i = 0; i < 500; i++)
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

                #region Parallel

                #region Invoke

                if (IsParallel_Invoke_Void)
                {
                    Console.WriteLine("----------------IsParallel_Invoke-----------------------");
                    Parallel.Invoke(
                        () => { Parallel_1_Void(); },
                        () => { Parallel_2_Void(); },
                        () => { Parallel_3_Void(); }
                        );
                }

                if (IsParallel_Invoke_ReturnValue)
                {
                    Console.WriteLine("Invoke String Calls");
                    List<string> resultStringsList = new List<string>();
                    Parallel.Invoke(
                        () => { resultStringsList.Add(Parallel_1_String()); },
                        () => { resultStringsList.Add(Parallel_2_String()); },
                        () => { resultStringsList.Add(Parallel_3_String()); }
                        );
                    Console.WriteLine("---------Print The Result of  Invoking the Parallel Calls--------");
                    foreach (var item in resultStringsList)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("---------------------Parallel Main Senario----------------------");
                    Console.WriteLine("Parallel Main Senario Started At: " + DateTime.Now);
                    for (int i = 0; i < 5; i++)
                    {
                        Console.WriteLine("Parallel Main Senario  :" + i);
                    }
                    Console.WriteLine("Parallel Main Senario Ended At: " + DateTime.Now);
                }

                #endregion

                #region For

                if (IsParallel_For_Void)
                {
                    Console.WriteLine("----------------IsParallel_For_Void-----------------------");
                    Parallel.For(1, 4, (Index) =>
                    {
                        if (Index == 1)
                        {
                            Parallel_1_Void();
                        }
                        else if (Index == 2)
                        {
                            Parallel_2_Void();
                        }
                        else if (Index == 3)
                        {
                            Parallel_3_Void();
                        }
                    });
                    Console.WriteLine("---------------------Parallel Main Senario----------------------");
                    Console.WriteLine("Parallel Main Senario Started At: " + DateTime.Now);
                    for (int i = 0; i < 5; i++)
                    {
                        Console.WriteLine("Parallel Main Senario  :" + i);
                    }
                    Console.WriteLine("Parallel Main Senario Ended At: " + DateTime.Now);
                }

                if (IsParallel_For_ReturnValue)
                {
                    Console.WriteLine("----------------IsParallel_For_ReturnValue-----------------------");
                    List<string> returnsStrings = new List<string>();
                    Parallel.For(1, 4, (Index) =>
                    {
                        if (Index == 1)
                        {
                            returnsStrings.Add(Parallel_1_String());
                        }
                        else if (Index == 2)
                        {
                            returnsStrings.Add(Parallel_2_String());
                        }
                        else if (Index == 3)
                        {
                            returnsStrings.Add(Parallel_3_String());
                        }
                    });
                    Console.WriteLine("---------Print The Result of  [For] the Parallel Calls--------");
                    foreach (var item in returnsStrings)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("---------------------Parallel Main Senario----------------------");
                    Console.WriteLine("Parallel Main Senario Started At: " + DateTime.Now);
                    for (int i = 0; i < 5; i++)
                    {
                        Console.WriteLine("Parallel Main Senario  :" + i);
                    }
                    Console.WriteLine("Parallel Main Senario Ended At: " + DateTime.Now);

                }

                #endregion

                #region Foreach

                if (IsParallel_Foreach_Void)
                {
                    Console.WriteLine("----------------IsParallel_Foreach_Void-----------------------");
                    List<int> countsList = new List<int>() { 1, 2, 3 };
                    Parallel.ForEach(countsList, (item) =>
                    {
                        if (item == 1)
                        {
                            Parallel_1_Void();
                        }
                        else if (item == 2)
                        {
                            Parallel_2_Void();
                        }
                        else if (item == 3)
                        {
                            Parallel_3_Void();
                        }
                    });
                    Console.WriteLine("---------------------Parallel Main Senario----------------------");
                    Console.WriteLine("Parallel Main Senario Started At: " + DateTime.Now);
                    for (int i = 0; i < 5; i++)
                    {
                        Console.WriteLine("Parallel Main Senario  :" + i);
                    }
                    Console.WriteLine("Parallel Main Senario Ended At: " + DateTime.Now);
                }

                if (IsParallel_Foreach_ReturnValue)
                {
                    Console.WriteLine("----------------IsParallel_Foreach_ReturnValue-----------------------");
                    List<int> countsList = new List<int>() { 1, 2, 3 };
                    List<string> returnStrings = new List<string>();
                    Parallel.ForEach(countsList, (item) =>
                    {
                        if (item == 1)
                        {
                            returnStrings.Add(Parallel_1_String());
                        }
                        else if (item == 2)
                        {
                            returnStrings.Add(Parallel_2_String());
                        }
                        else if (item == 3)
                        {
                            returnStrings.Add(Parallel_3_String());
                        }
                    });
                    Console.WriteLine("---------Print The Result of  [For] the Parallel Calls--------");
                    foreach (var item in returnStrings)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("---------------------Parallel Main Senario----------------------");
                    Console.WriteLine("Parallel Main Senario Started At: " + DateTime.Now);
                    for (int i = 0; i < 5; i++)
                    {
                        Console.WriteLine("Parallel Main Senario  :" + i);
                    }
                    Console.WriteLine("Parallel Main Senario Ended At: " + DateTime.Now);
                }

                #endregion

                #endregion

            }
            catch (Exception ex)
            {
                File.AppendAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\LogFile.txt", ex.Message);
            }
        }

        #endregion

        #region Parallel

        #region Void

        public static void Parallel_1_Void()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();
            Console.WriteLine("Parallel_1_Void Invoked At: " + DateTime.Now);
            Thread.Sleep(1000);
            Console.WriteLine("Parallel_1_Void Started At: " + DateTime.Now);
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Parallel_1 :" + i);
            }
            stopwatch.Stop();
            Console.WriteLine("Parallel_1_Void Ended At: " + DateTime.Now + " Execution Time: " + stopwatch.ElapsedMilliseconds);
        }

        public static void Parallel_2_Void()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();
            Console.WriteLine("Parallel_2_Void Invoked At: " + DateTime.Now);
            Thread.Sleep(1000);
            Console.WriteLine("Parallel_2_Void Started At: " + DateTime.Now);
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Parallel_2_Void :" + i);
            }
            stopwatch.Stop();
            Console.WriteLine("Parallel_2_Void Ended At: " + DateTime.Now + " Execution Time: " + stopwatch.ElapsedMilliseconds);
        }

        public static void Parallel_3_Void()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();
            Console.WriteLine("Parallel_3_Void Invoked At: " + DateTime.Now);
            Thread.Sleep(1000);
            Console.WriteLine("Parallel_3_Void Started At: " + DateTime.Now);
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Parallel_3_Void :" + i);
            }
            stopwatch.Stop();
            Console.WriteLine("Parallel_3_Void Ended At: " + DateTime.Now + " Execution Time: " + stopwatch.ElapsedMilliseconds);
        }

        #endregion

        #region Return String

        public static string Parallel_1_String()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();
            Console.WriteLine("Parallel_1_String Invoked At: " + DateTime.Now);
            Thread.Sleep(1000);
            Console.WriteLine("Parallel_1_String Started At: " + DateTime.Now);
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Parallel_1_String :" + i);
            }
            Console.WriteLine("Parallel_1_String Ended At: " + DateTime.Now);
            stopwatch.Stop();
            return "Parallel_1_String Ended At: " + DateTime.Now + " Execution Time: " + stopwatch.ElapsedMilliseconds;
        }

        public static string Parallel_2_String()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();
            Console.WriteLine("Parallel_2_String Invoked At: " + DateTime.Now);
            Thread.Sleep(1000);
            Console.WriteLine("Parallel_2_String Started At: " + DateTime.Now);
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Parallel_2_String :" + i);
            }
            Console.WriteLine("Parallel_2_String Ended At: " + DateTime.Now);
            stopwatch.Stop();
            return "Parallel_2_String Ended At: " + DateTime.Now + " Execution Time: " + stopwatch.ElapsedMilliseconds;
        }

        public static string Parallel_3_String()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();
            Console.WriteLine("Parallel_3_String Invoked At: " + DateTime.Now);
            Thread.Sleep(1000);
            Console.WriteLine("Parallel_3_String Started At: " + DateTime.Now);
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Parallel_3_String :" + i);
            }
            Console.WriteLine("Parallel_3_String Ended At: " + DateTime.Now);
            stopwatch.Stop();
            return "Parallel_3_String Ended At: " + DateTime.Now + " Execution Time: " + stopwatch.ElapsedMilliseconds;
        }

        #endregion

        #endregion

        #region Async

        static async void Mytask1(string filePath)
        {
            Console.WriteLine("-------------------------Start Mytask1 Call----------------------");
            Console.WriteLine();
            Console.WriteLine("Mytask1 Stated  At:" + DateTime.Now);
            var counter1 = await ReadFromFile1(filePath);
            Console.WriteLine("Mytask1 File Data Count:" + (counter1) + " At:" + DateTime.Now);
            Console.WriteLine("-------------------------End Mytask1 Call----------------------");
            Console.WriteLine();
        }

        static async void Mytask2(string filePath)
        {
            Console.WriteLine("-------------------------Start Mytask2 Call----------------------");
            Console.WriteLine();
            Console.WriteLine("Mytask2 Stated  At:" + DateTime.Now);
            var counter1 = await ReadFromFile2(filePath);
            Console.WriteLine("Mytask2 File Data Count:" + (counter1) + " At:" + DateTime.Now);
            Console.WriteLine("-------------------------End Mytask2 Call----------------------");
            Console.WriteLine();
        }

        static async void CallAsyncMethodTwice(string filePath)
        {
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("CallAsyncMethodTwice Started to call ReadFromFile1 At:" + DateTime.Now);
            var counter1 = await ReadFromFile1(filePath);
            //Console.WriteLine("allAsyncMethodTwice Started to call ReadFromFile1 Counter" + counter1);
            Console.WriteLine("CallAsyncMethodTwice End Call ReadFromFile1 At:" + DateTime.Now);
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("CallAsyncMethodTwice Started to call ReadFromFile2 At:" + DateTime.Now);
            var counter2 = await ReadFromFile2(filePath);
            //Console.WriteLine("allAsyncMethodTwice Started to call ReadFromFile2 Counter" + counter2);
            Console.WriteLine("CallAsyncMethodTwice End Call ReadFromFile2 At:" + DateTime.Now);

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Sum For The Two Counters At Index: " + i + "  " + (counter1 + counter2));
            }
        }

        static async void CallAsyncMethodAsAforeachForOneAsyncMethod(string filePath)
        {
            Console.WriteLine("-----------------------------------------------");
            int counter = 0;
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("CallAsyncMethodAsAforeachForOneAsyncMethod Started to call ReadFromFile1 At:" + DateTime.Now);
                counter = await ReadFromFile1(filePath);
                Console.WriteLine("CallAsyncMethodAsAforeachForOneAsyncMethod End Call ReadFromFile1 At:" + DateTime.Now);
            }
            Console.WriteLine("-----------------------------------------------");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Sum For The Two Counters At Index: " + i + "  " + (counter));
            }
        }

        static async Task<int> ReadFromFile1(string filePath)
        {
            Thread.Sleep(5000);
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

        #region Sequential 

        static void SequentialDelayApp(int i)
        {
            Thread.Sleep(1000);
            if (i > 0)
            {
                SequentialDelayApp(i - 1);
            }
        }

        #endregion

        #region Thread

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
