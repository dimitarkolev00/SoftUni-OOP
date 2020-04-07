using System;
using System.Collections.Generic;
using System.Text;

namespace P01.Vehicles.Models
{
    public class Truck : Vehicle
    {
        private const double FUEL_CONSUMPTION_INCREMENT = 1.6;
        private const double refuelingCoefficient = 0.95;
        public Truck(double fuelQuantity, double fuelConsumption, double tankCapacity)
            : base(fuelQuantity, fuelConsumption, tankCapacity)
        {

        }
        protected override double AdditionalConsumption => FUEL_CONSUMPTION_INCREMENT;

        public override void Refuel(double fuel)
        {
            base.Refuel(fuel);
            this.FuelQuantity -= (1 - refuelingCoefficient) * fuel;
        }
    }

}
