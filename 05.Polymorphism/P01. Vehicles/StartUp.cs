using System;
using P01.Vehicles.Core;
using P01.Vehicles.Core.Contracts;

namespace P01.Vehicles
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            IEngine engine = new Engine();
            engine.Run();
        }
    }
}
