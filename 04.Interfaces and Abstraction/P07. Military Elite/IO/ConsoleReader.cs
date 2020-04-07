using P07.MilitaryElit.IO.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace P07.MilitaryElit.IO
{
    public class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
