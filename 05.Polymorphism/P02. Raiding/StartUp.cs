using Raiding.Core;
using Raiding.IO;
using Raiding.Core.Contracts;
using Raiding.IO.Contracts;

namespace Raiding
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
