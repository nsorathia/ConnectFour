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

                Console.WriteLine("Would you like to play again? YES ('Y') or NO ('N')");
                input = Console.ReadLine();

            }
        }
    }
}