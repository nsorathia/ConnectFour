using System;
using Connect4.Interfaces;

namespace Connect4
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "y";

            while (input.Equals("y", StringComparison.CurrentCultureIgnoreCase))
            {
                var game = Container.GetObject<IGame>();
                game.Play();

                Console.WriteLine("Want to start a new game? YES ('Y') or NO ('N')");
                input = Console.ReadLine();
            }
        }
    }
}