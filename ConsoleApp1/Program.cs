using CronExpressionDescriptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        public static void Main(string[] args)
        {

            var cron = ExpressionDescriptor.GetDescription("0 30/2 10-13 ? * WEEKDAYS");

            Console.WriteLine(cron);

            StartLongRunning();

            Console.WriteLine("Done");

            Console.ReadKey();
        }

        static async void StartLongRunning()
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            var x = LongRunning(token);

            if (x.Wait(25))
            {
                Console.WriteLine(x.Result);

                Console.WriteLine("Task Completed");
            }
            else
            {
              //  source.Cancel();
                x.ContinueWith(t =>
                {
                    if (t.Exception != null)
                    {
                        Console.WriteLine(t.Exception?.InnerException);
                    }
                    {
                        Console.WriteLine("Task Completed 2");
                    }
                }

                );
                Console.WriteLine("Task Not Completed");
            }
 
          


        }

        static async Task<int> LongRunning(CancellationToken token)
        {
            await Task.Delay(10, token);
            Console.WriteLine("10.....");
            await Task.Delay(10, token);
            Console.WriteLine("20.....");
            await Task.Delay(10, token);
            Console.WriteLine("30.....");

            throw new Exception("what will happen");
            return 30;

        }
    }
}
