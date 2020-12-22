using System;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace WebApi.Controllers
{
    public class AsyncController : ApiController
    {
        bool asyncVoidTaskVar = bool.Parse(ConfigurationManager.AppSettings["AsyncVoidTask"]);
        bool IsExceptionPath = bool.Parse(ConfigurationManager.AppSettings["IsExceptionPath"]);

        public async Task<IHttpActionResult> Index()
        {
            try
            {
                Console.WriteLine("Hello World! - Test Async Cases");
                // Call Task -- used in case no response from the task.
                if (asyncVoidTaskVar)
                {
                    //Task task = new Task(CallVoidTask);
                    //task.Start();
                    //task.Wait();

                    await CallVoidTask(1);

                    for (int i = 0; i < 10; i++)
                    {
                        //Console.WriteLine("Complete the login of the main senario : " + i);
                    }
                }
                await CallVoidTask(1);
                return Content(HttpStatusCode.OK, "Complete the login of the main senario :");
            }
            catch (Exception ex)
            {
                System.IO.File.WriteAllText("D:\\Courses\\Async\\AsyncSolution\\AsyncCallConsole\\Logs\\LogFile.txt", ex.Message);
                return Content(HttpStatusCode.OK, "Complete the login of the main senario :");
            }


        }


        private async Task<int> CallVoidTask(int value)
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
            return 1;
        }


        //private static void FileLogger(NLog.LogType logType, string logValue)
        //{
        //    NlogManager.Log(logType, logValue);
        //}
    }
}
