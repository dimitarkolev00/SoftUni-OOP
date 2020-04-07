namespace Raiding.Models
{
    public class Warrior : BaseHero
    {
        private const int WARRIOR_POWER = 100;
        public Warrior(string name, string type)
            : base(name)
        {
            this.Type = type;
        }
        public string Type { get; private set; }
        public override int Power => WARRIOR_POWER;
        public override string CastAbility()
        {
            return $"{this.Type} - {this.Name} hit for {this.Power} damage";
        }
    }
}
