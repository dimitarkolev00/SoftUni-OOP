using System.Collections.Generic;

using Raiding.Factories;
using Raiding.Contracts;
using Raiding.Core.Contracts;
using Raiding.IO.Contracts;
using Raiding.Exceptions;
using System.Linq;

namespace Raiding.Core
{
    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private ICollection<IBaseHero> heroes;
        private HeroFactory heroFactory;
        public Engine(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
            this.heroes = new List<IBaseHero>();
            this.heroFactory = new HeroFactory();
        }
        public void Run()
        {
            var n = int.Parse(reader.ReadLine());

            while (heroes.Count < n)
            {
                string heroName = reader.ReadLine();
                string heroType = reader.ReadLine();

                try
                {
                    IBaseHero hero = this.heroFactory.ProduceHero(heroName, heroType);
                    this.heroes.Add(hero);
                }
                catch (InvalidHeroException ihe)
                {
                    writer.WriteLine(ihe.Message);
                }
            }

            var bossPower = int.Parse(reader.ReadLine());

            foreach (IBaseHero hero in this.heroes)
            {
                writer.WriteLine(hero.CastAbility());
            }
            var totalHeroesPower = this.heroes.Sum(h => h.Power);

            if (totalHeroesPower >= bossPower)
            {
                writer.WriteLine("Victory!");
            }
            else
            {
                writer.WriteLine("Defeat...");
            }
        }
    }
}
