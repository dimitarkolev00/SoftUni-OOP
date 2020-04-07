using System;
using CarManager;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class CarTests
    {
        private Car car;
        [SetUp]
        public void Setup()
        {
            this.car = new Car("Audi", "A6", 10.5, 80.5);
        }

        [Test]
        public void TestIfConstructorWorksCorrectly()
        {
            var expectedMake = "Audi";
            var expectedModel = "A6";
            var expectedFC = 10.5;
            var expectedCapacity = 80.5;
            var expectedFuelAmount = 0;

            Assert.AreEqual(expectedMake, this.car.Make);
            Assert.AreEqual(expectedModel, this.car.Model);
            Assert.AreEqual(expectedFC, this.car.FuelConsumption);
            Assert.AreEqual(expectedCapacity, this.car.FuelCapacity);

            Assert.AreEqual(expectedFuelAmount, this.car.FuelAmount);

        }

        [TestCase(null)]
        [TestCase("")]
        public void TestIfMakePropertyThrowsExceptionWhenNullOrEmpty(string make)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                this.car = new Car(make, "M2", 20.5, 52.5);
            });
        }
        [TestCase(null)]
        [TestCase("")]
        public void TestIfModelPropertyThrowsExceptionWhenNullOrEmpty(string model)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                this.car = new Car("Audi", model, 20.5, 52.5);
            });
        }

        [TestCase(-5.5)]
        [TestCase(0)]
        public void TestIfFuelConsumptionThrowsExceptionWhenNullOrNegative(double consumption)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var car = new Car("Audi", "A6", consumption, 52.5);
            });
        }
        [TestCase(-10.5)]
        [TestCase(0)]
        public void TestIfFuelCapacityThrowsExceptionWhenNullOrNegative(double capacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var car = new Car("Audi", "A6", 10.5, capacity);
            });
        }

        [TestCase(0)]
        [TestCase(-10.5)]
        public void TestRefuelWithZeroOrNegativeNumberShouldThrowException(double fuel)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                this.car.Refuel(fuel);
            });
        }

        [Test]
        public void TestRefuelWorksCorrectly()
        {
            this.car.Refuel(50.5);
            var expectedFuelAmount = 50.5;
            var actualFuelAmount = this.car.FuelAmount;

            Assert.AreEqual(expectedFuelAmount, actualFuelAmount);
        }
        [Test]
        public void TestRefuelWithAmountBiggerThanCapacity()
        {
            this.car.Refuel(85.5);
            var expectedFuelAmount = 80.5;
            var actualFuelAmount = this.car.FuelAmount;

            Assert.AreEqual(expectedFuelAmount, actualFuelAmount);
        }

        [Test]
        public void TestDriveWithNotEnoughFuelThrowsException()
        {
            this.car.Refuel(80.9);
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.car.Drive(3000);
            });
        }

        [Test]
        public void TestDriveWorksCorrectly()
        {
            this.car = new Car("Audi", "A6", 10.5, 80.5);

            this.car.Refuel(100.1);
            this.car.Drive(500);
            var expectedFuelAmount =  this.car.FuelCapacity- ((500 / 100) * this.car.FuelConsumption);
            var actualFuelAmount = this.car.FuelAmount;

            Assert.AreEqual(expectedFuelAmount, actualFuelAmount);
        }

    }
}