namespace SantaWorkshop.Models.Dwarfs
{
    public class HappyDwarf : Dwarf
    {
        public const int HappyDwarfEnergy = 100;

        public HappyDwarf(string name)
            : base(name, HappyDwarfEnergy)
        {

        }

        public override void Work()
        {
            this.Energy -= 10;
        }
    }
}