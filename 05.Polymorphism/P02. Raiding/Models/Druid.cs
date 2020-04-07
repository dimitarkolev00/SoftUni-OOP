namespace Raiding.Models
{
    public class Druid : BaseHero
    {
        private const int DRUID_POWER = 80;
        public Druid(string name, string type)
            : base(name)
        {
            this.Type = type;
        }
        public string Type { get; private set; }
        public override int Power => DRUID_POWER;
        public override string CastAbility()
        {
            return $"{this.Type} - {this.Name} healed for {this.Power}";
        }
    }
}
