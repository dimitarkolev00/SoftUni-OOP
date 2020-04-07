using P07.MilitaryElit.Contracts;
using P07.MilitaryElit.IO.Contracts;
using System;

namespace P07.MilitaryElit.IO
{
    public class ConsoleWriter : IWriter
    {
        public void Write(string text)
        {
            Console.Write(text);
        }

        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }

        
    }
}
