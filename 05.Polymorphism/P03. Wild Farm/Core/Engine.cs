using System;
using System.Collections.Generic;
using System.Linq;

using P04.WildFarm.Core.Contracts;
using P04.WildFarm.Exceptions;
using P04.WildFarm.Factories;
using P04.WildFarm.Models.Animals;
using P04.WildFarm.Models.Animals.Contracts;
using P04.WildFarm.Models.Foods.Contracts;

namespace P04.WildFarm.Core
{
    public class Engine : IEngine
    {
        private ICollection<IAnimal> animals;
        private FoodFactories foodFactories;
        public Engine()
        {
            this.animals = new List<IAnimal>();
            this.foodFactories = new FoodFactories();
        }
        public void Run()
        {
            string command;
            while ((command = Console.ReadLine()) != "End")
            {
                string[] animalArgs = command
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();
                string[] foodArgs = Console.ReadLine()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                var animal = ProduceAnimal(animalArgs);

                IFood food = this.foodFactories.ProduceFood(foodArgs[0], int.Parse(foodArgs[1]));

                this.animals.Add(animal);

                Console.WriteLine(animal.ProduceSound());

                try
                {
                    animal.Feed(food);
                }
                catch (UneatableFoodException ufe)
                {
                    Console.WriteLine(ufe.Message);
                }
            }

            foreach (var animal in this.animals)
            {
                Console.WriteLine(animal);
            }
        }

        private static IAnimal ProduceAnimal(string[] animalArgs)
        {
            IAnimal animal = null;

            string animalType = animalArgs[0];
            string name = animalArgs[1];
            double weight = double.Parse((animalArgs[2]));

            if (animalType == "Owl")
            {
                double wingSize = double.Parse(animalArgs[3]);
                animal = new Owl(name, weight, wingSize);
            }
            else if (animalType == "Hen")
            {
                double wingSize = double.Parse(animalArgs[3]);
                animal = new Hen(name, weight, wingSize);
            }
            else
            {
                string livingRegion = animalArgs[3];

                if (animalType == "Mouse")
                {
                    animal = new Mouse(name, weight, livingRegion);
                }
                else if (animalType == "Dog")
                {
                    animal = new Dog(name, weight, livingRegion);
                }
                else
                {
                    string breed = animalArgs[4];

                    if (animalType == "Cat")
                    {
                        animal = new Cat(name, weight, livingRegion, breed);
                    }
                    else if (animalType == "Tiger")
                    {
                        animal = new Tiger(name, weight, livingRegion, breed);
                    }
                }
            }

            return animal;
        }
    }
}
