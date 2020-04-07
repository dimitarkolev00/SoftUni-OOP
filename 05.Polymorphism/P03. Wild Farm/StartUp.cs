using System;
using P04.WildFarm.Core;
using P04.WildFarm.Core.Contracts;

namespace P04.WildFarm
{
    public class StartUp
    {
        static void Main()
        {
           IEngine engine = new Engine();
           engine.Run();
        }
    }
}
