namespace P01_RawData
{
    using System.Collections.Generic;

    public class Car
    {
        private readonly Tire[] tires;
        public Car(string model, Engine engine, Cargo cargo, Tire[] tires)
        {
            this.Model = model;
            this.Engine = engine;
            this.Cargo = cargo;
            this.tires = tires;
        }
        public string Model { get;}
        public Engine Engine { get;}
        public Cargo Cargo { get; }

        public IReadOnlyCollection<Tire> Tires 
        {
            get
            {
                return this.tires;
            }
        }

    }
}
