using P03.Telephony.Contracts;
using P03.Telephony.Core;
using P03.Telephony.IO;
using System;

namespace P03.Telephony
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            IEngine engine = new Engine(reader, writer);
            engine.Run();
        }
    }
}
