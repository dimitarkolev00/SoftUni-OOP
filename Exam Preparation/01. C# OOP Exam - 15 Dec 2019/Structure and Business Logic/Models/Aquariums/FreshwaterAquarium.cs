namespace AquaShop.Models.Aquariums
{
    public class FreshwaterAquarium : Aquarium
    {
        private const int FreshWaterAquariumCapacity = 50;
        public FreshwaterAquarium(string name)
            : base(name, FreshWaterAquariumCapacity)
        {
        }
    }
}
