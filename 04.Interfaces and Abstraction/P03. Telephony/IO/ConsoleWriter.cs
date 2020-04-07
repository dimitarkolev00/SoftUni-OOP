using P03.Telephony.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace P03.Telephony.IO
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
