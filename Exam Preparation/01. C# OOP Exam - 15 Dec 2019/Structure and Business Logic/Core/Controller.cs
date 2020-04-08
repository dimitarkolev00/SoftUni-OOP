using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using AquaShop.Core.Contracts;
using AquaShop.Models.Aquariums;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Repositories;
using AquaShop.Repositories.Contracts;
using AquaShop.Utilities.Messages;

namespace AquaShop.Core
{
    public class Controller : IController
    {
        private readonly IRepository<IDecoration> decorations;
        private readonly ICollection<IAquarium> aquariums;

        public Controller()
        {
            this.decorations = new DecorationRepository();
            this.aquariums = new List<IAquarium>();
        }
        public string AddAquarium(string aquariumType, string aquariumName)
        {
            IAquarium aquarium;

            if (aquariumType == nameof(FreshwaterAquarium))
            {
                aquarium = new FreshwaterAquarium(aquariumName);
            }
            else if (aquariumType == nameof(SaltwaterAquarium))
            {
                aquarium = new SaltwaterAquarium(aquariumName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAquariumType);
            }

            this.aquariums.Add(aquarium);

            string result = String.Format(OutputMessages.SuccessfullyAdded, aquariumType);

            return result;
        }

        public string AddDecoration(string decorationType)
        {
            IDecoration decoration;

            if (decorationType == nameof(Ornament))
            {
                decoration = new Ornament();
            }
            else if (decorationType == nameof(Plant))
            {
                decoration = new Plant();
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidDecorationType);
            }

            this.decorations.Add(decoration);

            string result = String.Format(OutputMessages.SuccessfullyAdded, decorationType);

            return result;
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            var decoration = this.decorations.FindByType(decorationType);
            var aquarium = this.aquariums.FirstOrDefault(n => n.Name == aquariumName);

            if (decoration == null)
            {
                throw new InvalidOperationException(String.Format
                    (ExceptionMessages.InexistentDecoration, decorationType));
            }

            aquarium.AddDecoration(decoration);
            this.decorations.Remove(decoration);

            string result = String.Format
                (OutputMessages.EntityAddedToAquarium, decorationType, aquariumName);

            return result;
        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            IFish fish;

            if (fishType == nameof(FreshwaterFish))
            {
                fish = new FreshwaterFish(fishName, fishSpecies, price);
            }
            else if (fishType == nameof(SaltwaterFish))
            {
                fish = new SaltwaterFish(fishName, fishSpecies, price);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidFishType);
            }

            var aquarium = this.aquariums.FirstOrDefault(x => x.Name == aquariumName);

            if (fish.GetType() == typeof(SaltwaterFish)
                && aquarium.GetType() == typeof(FreshwaterAquarium))
            {
                return OutputMessages.UnsuitableWater;
            }

            if (fish.GetType() == typeof(FreshwaterFish)
                && aquarium.GetType() == typeof(SaltwaterAquarium))
            {
                return OutputMessages.UnsuitableWater;
            }

            aquarium.AddFish(fish);

            string message = String.Format(OutputMessages.EntityAddedToAquarium, fishType, aquariumName);
            return message;
        }

        public string FeedFish(string aquariumName)
        {
            var aquarium = this.aquariums.FirstOrDefault(x => x.Name == aquariumName);

            foreach (var fish in aquarium.Fish)
            {
                fish.Eat();
            }
            string message = String.Format(OutputMessages.FishFed, aquarium.Fish.Count);
            return message;
        }

        public string CalculateValue(string aquariumName)
        {
            var aquarium = this.aquariums.FirstOrDefault(x => x.Name == aquariumName);
            var totalPrice = aquarium.Fish.Sum(p => p.Price) +
                             aquarium.Decorations.Sum(p => p.Price);

            string result = String.Format(OutputMessages.AquariumValue, aquariumName, totalPrice);
            return result;
        }

        public string Report()
        {
           StringBuilder sb = new StringBuilder();

           foreach (var aquarium in this.aquariums)
           {
               sb.AppendLine(aquarium.GetInfo());
           }

           return sb.ToString().TrimEnd();
        }
    }
}
