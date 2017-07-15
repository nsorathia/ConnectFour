using System;
using System.Collections.Generic;
using System.Text;

namespace Connect4.Interfaces
{
    public interface IDataDevice
    {
        string ReadData();
        void WriteData(string input);
    }
}
