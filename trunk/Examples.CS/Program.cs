using System;
using NBehave.Framework.Story;

namespace Examples.CS
{

    class Program
    {
        static void Main(string[] args)
        {
            ConsoleRunner me = new ConsoleRunner();
            me.Run();

            Console.WriteLine();
            Console.ReadLine();
        }
    }

}