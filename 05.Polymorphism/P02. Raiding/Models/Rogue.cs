namespace Raiding.Models
{
    public class Rogue : BaseHero
    {
        private const int ROGUE_POWER = 80;
        public Rogue(string name, string type)
            : base(name)
        {
            this.Type = type;
        }
        public string Type { get; private set; }
        public override int Power => ROGUE_POWER;
        public override string CastAbility()
        {
            return $"{this.Type} - {this.Name} hit for {this.Power} damage";
        }
    }
}
