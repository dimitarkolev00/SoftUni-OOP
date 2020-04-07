using P04.WildFarm.Models.Foods.Contracts;

namespace P04.WildFarm.Models.Animals.Contracts
{
    public interface IAnimal
    {
        string Name { get; }
        double Weight { get; }
        int FoodEaten { get; }
        string ProduceSound();
        void Feed(IFood food);

    }
}
