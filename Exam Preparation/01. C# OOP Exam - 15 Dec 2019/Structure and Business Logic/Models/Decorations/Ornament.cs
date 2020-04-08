
namespace AquaShop.Models.Decorations
{
    public class Ornament : Decoration
    {
        private const int ComfortValue = 1;
        private const int PriceValue = 5;
        public Ornament() 
            : base(ComfortValue, PriceValue)
        {

        }
    }
}
