using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceStation.Core.Contracts;
using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission;
using SpaceStation.Models.Planets;
using SpaceStation.Repositories;
using SpaceStation.Repositories.Contracts;
using SpaceStation.Utilities.Messages;

namespace SpaceStation.Core
{
    public class Controller : IController
    {
        private AstronautRepository astronautRepository;
        private PlanetRepository planetRepository;
        private List<IPlanet> exploredPlanets;

        public Controller()
        {
            this.astronautRepository = new AstronautRepository();
            this.planetRepository = new PlanetRepository();
            this.exploredPlanets = new List<IPlanet>();
        }
        public string AddAstronaut(string type, string astronautName)
        {
            IAstronaut astronaut;

            if (type == "Biologist")
            {
                astronaut = new Biologist(astronautName);
            }
            else if (type == "Geodesist")
            {
                astronaut = new Geodesist(astronautName);
            }
            else if (type == "Meteorologist")
            {
                astronaut = new Meteorologist(astronautName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautType);
            }

            this.astronautRepository.Add(astronaut);

            string result = String.Format(OutputMessages.AstronautAdded, type, astronautName);
            return result;
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            var planet = new Planet(planetName);

            foreach (var item in items)
            {
                planet.Items.Add(item);
            }
            this.planetRepository.Add(planet);

            string result = String.Format(String.Format(OutputMessages.PlanetAdded, planetName));
            return result;
        }

        public string RetireAstronaut(string astronautName)
        {

            var astronautsCollection = astronautRepository.Models
                .FirstOrDefault(n => n.Name == astronautName);

            if (astronautsCollection == null)
            {
                throw new InvalidOperationException(String.Format
                    (ExceptionMessages.InvalidRetiredAstronaut, astronautName));
            }

            this.astronautRepository.Remove(astronautsCollection);
            string result = String.Format(OutputMessages.AstronautRetired, astronautName);
            return result;
        }

        public string ExplorePlanet(string planetName)
        {
            IMission mission = new Mission();
            List<IAstronaut> suitableAstronauts = this.astronautRepository.Models
                .Where(a => a.Oxygen > 60).ToList();

            var planet = this.planetRepository.FindByName(planetName);

            if (suitableAstronauts.Count <= 0)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautCount);
            }

            mission.Explore(planet, suitableAstronauts);
            this.exploredPlanets.Add(planet);

            string result = String.Format(OutputMessages.PlanetExplored, planetName, suitableAstronauts.Where(a => a.Oxygen <= 0).Count());
            return result;

        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            sb
                .AppendLine($"{this.exploredPlanets.Count} planets were explored!")
                .AppendLine("Astronauts info:");
            foreach (var astronaut in this.astronautRepository.Models)
            {
                sb
               .AppendLine($"Name: {astronaut.Name}")
               .AppendLine($"Oxygen: {astronaut.Oxygen}")
               .AppendLine($"Bag items: {(astronaut.Bag.Items.Any() ? string.Join(", ", astronaut.Bag.Items) : "none")}");
            }



            return sb.ToString().TrimEnd();
        }
    }
}
