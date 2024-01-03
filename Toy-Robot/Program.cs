using System;
using System.Threading;
using Toy_Robot;

namespace Toy_Robot
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cts = new();
            CancellationToken cancellationToken = cts.Token;

            Console.WriteLine("Welcome to Toy Robot Simulator");
            Console.WriteLine("");
            Console.WriteLine("Please Enter your command below.");
            Console.WriteLine("");

            CommandProcessor commandProcessor = new(cts);

            while (!cancellationToken.IsCancellationRequested)
            {
                var enteredCommand = Console.ReadLine();

                try
                {
                    var msg = string.Empty;
                    commandProcessor.ProcessCommand(enteredCommand);

                    if (!string.IsNullOrWhiteSpace(msg))
                        Console.WriteLine(msg);
                }
                catch (Exception ex)
                {
                   Console.WriteLine(ex.Message);
                }
               
            }

            Console.WriteLine("Toy Robot Simulator Exiting");
        }
     
    }
}
