using System.Collections.Generic;
using System.Linq;
using SpaceStation.Models.Planets;
using SpaceStation.Repositories.Contracts;

namespace SpaceStation.Repositories
{
    public class PlanetRepository : IRepository<IPlanet>
    {
        private readonly ICollection<IPlanet> planets;

        public PlanetRepository()
        {
            this.planets = new List<IPlanet>();
        }

        public IReadOnlyCollection<IPlanet> Models =>
                 (IReadOnlyCollection<IPlanet>)this.planets;
        public void Add(IPlanet model)
        {
            this.planets.Add(model);
        }

        public bool Remove(IPlanet model)
        {
            return this.planets.Remove(model);
        }

        public IPlanet FindByName(string name)
        {
            return this.planets.FirstOrDefault(n => n.Name == name);
        }
    }
}
