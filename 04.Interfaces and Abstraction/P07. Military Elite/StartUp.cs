using P07.MilitaryElit.Core;
using P07.MilitaryElit.Core.Contracts;
using P07.MilitaryElit.IO;
using P07.MilitaryElit.IO.Contracts;
using System;

namespace P07.MilitaryElit
{
    public class StartUp
    {
        public static void Main()
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            IEngine engine = new Engine(reader,writer);
            engine.Run();
        }
    }
}
