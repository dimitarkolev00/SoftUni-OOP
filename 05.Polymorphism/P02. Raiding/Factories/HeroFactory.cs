using Raiding.Contracts;
using Raiding.Exceptions;
using Raiding.Models;

namespace Raiding.Factories
{
    public class HeroFactory
    {
        public IBaseHero ProduceHero(string name, string type)
        {
            IBaseHero hero = null;

            if (type == "Druid")
            {
                hero = new Druid(name, type);
            }
            else if (type == "Paladin")
            {
                hero = new Paladin(name, type);
            }
            else if (type == "Rogue")
            {
                hero = new Rogue(name, type);
            }
            else if (type == "Warrior")
            {
                hero = new Warrior(name, type);
            }
            else
            {
                throw new InvalidHeroException();
            }

            return hero;
        }
    }
}
