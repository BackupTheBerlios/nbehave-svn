using System;
using NBehave.Framework.Story;

namespace Examples.CS
{

    class Program
    {
        static void Main(string[] args)
        {
            ConsoleRunner me = new ConsoleRunner();
            //Use the line below for more sparse output.
            //ConsoleRunner me = new ConsoleRunner(typeof (Program).Assembly,ConsoleRunner.ConsoleOutput.Simple);
            me.Run();

            Console.WriteLine();
            Console.ReadLine();
        }
    }

}