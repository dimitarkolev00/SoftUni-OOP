﻿
namespace AquaShop.Models.Aquariums
{
    public class SaltwaterAquarium:Aquarium
    {
        private const int SalterWaterCapacity = 25;
        public SaltwaterAquarium(string name) 
            : base(name, SalterWaterCapacity)
        {
        }
    }
}
