using P04.PizzaCalories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace P04.PizzaCalories.Core.IO
{
    public class ConsoleWriter : IWriter
    {
        public static void WriteLine(string textLine)
        {
            Console.WriteLine(textLine);
        }

        void IWriter.WriteLine(string textLine)
        {
            WriteLine(textLine);
        }
    }
}
