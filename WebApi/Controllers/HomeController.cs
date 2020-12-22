using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace WebApi.Controllers
{
    public class HomeController : Controller
    {
        bool asyncVoidTaskVar = bool.Parse(ConfigurationManager.AppSettings["AsyncVoidTask"]);
        bool IsExceptionPath = bool.Parse(ConfigurationManager.AppSettings["IsExceptionPath"]);

        public ActionResult Index()
        {

            try
            {
                Console.WriteLine("Hello World! - Test Async Cases");
                // Call Task -- used in case no response from the task.
                if (asyncVoidTaskVar)
                {
                    Task task = new Task(CallVoidTask);
                    task.Start();
                    //task.Wait();
                    for (int i = 0; i < 10; i++)
                    {
                        Console.WriteLine("Complete the login of the main senario : " + i);
                    }
                }
            }
            catch (Exception ex)
            {
                System.IO.File.WriteAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\LogFile.txt", ex.Message);
            }

            return View();
        }


        void CallVoidTask()
        {
            if (IsExceptionPath)
            {
                throw new Exception("Error Async Exception");
            }
            else
            {
                string valueVar = DateTime.Now.ToString() + "\n";
                for (int i = 0; i < 1000; i++)
                {
                    valueVar += "Write the task number: " + i + " \n";
                    //Thread.Sleep(500);
                }
                System.IO.File.WriteAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\LogFile.txt", valueVar);
            }
        }


        //private static void FileLogger(NLog.LogType logType, string logValue)
        //{
        //    NlogManager.Log(logType, logValue);
        //}

    }
}
