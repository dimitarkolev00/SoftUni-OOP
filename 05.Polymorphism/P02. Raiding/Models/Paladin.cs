namespace Raiding.Models
{
    public class Paladin : BaseHero
    {
        private const int PALADIN_POWER = 100;
        public Paladin(string name, string type)
            : base(name)
        {
            this.Type = type;
        }
        public string Type { get; private set; }
        public override int Power => PALADIN_POWER;
        public override string CastAbility()
        {
            return $"{this.Type} - {this.Name} healed for {this.Power}";
        }
    }
}
