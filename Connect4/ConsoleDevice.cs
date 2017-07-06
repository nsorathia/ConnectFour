using System;
using System.Text;
using Connect4.Interfaces;

namespace Connect4
{
    public class ConsoleDevice : IDataDevice
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string input)
        {
            Console.WriteLine(input);
        }
    }
}
