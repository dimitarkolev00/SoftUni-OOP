using P04.PizzaCalories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace P04.PizzaCalories.Core.IO
{
    public class ConsoleReader : IReader
    {
        public static string ReadLine()
        {
            return Console.ReadLine();
        }

        string IReader.ReadLine()
        {
            return ReadLine();
        }
    }
}
