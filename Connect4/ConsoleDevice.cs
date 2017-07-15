using System;
using System.Text;
using Connect4.Interfaces;

namespace Connect4
{
    public class ConsoleDevice : IDataDevice
    {
        public string ReadData()
        {
            return Console.ReadLine();
        }

        public void WriteData(string input)
        {
            Console.WriteLine(input);
        }
    }
}
