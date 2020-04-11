using System.Collections.Generic;
using System.Linq;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Planets;

namespace SpaceStation.Models.Mission
{
    public class Mission : IMission
    {
        public void Explore(IPlanet planet, ICollection<IAstronaut> astronauts)
        {

            while (true)
            {
                if (astronauts.Sum(a => a.Oxygen) <= 0)
                {
                    break;
                }

                var astronaut = astronauts.FirstOrDefault(a => a.CanBreath);

                foreach (var item in planet.Items.ToList())
                {
                    astronaut.Bag.Items.Add(item);
                    astronaut.Breath();
                    planet.Items.Remove(item);

                    if (astronaut.Oxygen <= 0)
                    {
                        break;
                    }
                }

                if (planet.Items.Count == 0)
                {
                    break;
                }
            }
        }
    }
}
