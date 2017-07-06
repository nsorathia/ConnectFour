using System;
using System.Collections.Generic;
using System.Text;

namespace Connect4.Interfaces
{
    public interface IDataDevice
    {
        string ReadLine();
        void WriteLine(string input);
    }
}
