using SpaceStation.Core.Contracts;
using SpaceStation.IO;
using SpaceStation.IO.Contracts;
using System;
using System.Linq;
using System.Threading;

namespace SpaceStation.Core
{
    public class Engine : IEngine
    {
        private IWriter writer;
        private IReader reader;
        private IController controller;

        public Engine()
        {
            this.writer = new Writer();
            this.reader = new Reader();
            this.controller = new Controller();
        }
        public void Run()
        {
            while (true)
            {
                var input = reader.ReadLine().Split();
                if (input[0] == "Exit")
                {
                    Environment.Exit(0);
                }
                try
                {
                    string result = string.Empty;

                    if (input[0] == "AddAstronaut")
                    {
                        string astronautType = input[1];
                        string astronautName = input[2];

                        result = controller.AddAstronaut(astronautType, astronautName);
                    }
                    else if (input[0] == "AddPlanet")
                    {
                        string[] items = input.Skip(2).ToArray();
                        result = this.controller.AddPlanet(input[1], items);
                    }
                    else if (input[0] == "RetireAstronaut")
                    {
                        string astrname = input[1];
                        result = controller.RetireAstronaut(astrname);
                    }
                    else if (input[0] == "ExplorePlanet")
                    {
                        string planetName = input[1];
                        result = controller.ExplorePlanet(planetName);

                    }
                    else if (input[0] == "Report")
                    {
                        result = controller.Report();
                    }
                    writer.WriteLine(result);
                }
                catch (Exception ex)
                {
                    writer.WriteLine(ex.Message);
                }
            }
        }
    }
}
