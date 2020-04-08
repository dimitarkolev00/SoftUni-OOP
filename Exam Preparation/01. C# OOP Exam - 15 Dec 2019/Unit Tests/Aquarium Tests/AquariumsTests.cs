using NUnit.Framework;

namespace Aquariums.Tests
{
    using System;
    [TestFixture]
    public class AquariumsTests
    {
        private Aquarium myAquarium;
        [SetUp]
        public void SetUp()
        {
            this.myAquarium = new Aquarium("Akvarium", 50);
        }

        [Test]
        public void TestIfFishConstructorWorksCorrectly()
        {
            Fish currentFish = new Fish("Pesho");
            string expectedName = "Pesho";
            string actualName = currentFish.Name;

            Assert.AreEqual(expectedName, actualName);
        }

        [Test]
        public void TestIfAquariumConstructorWorksCorrectly()
        {
            string expectedName = "Akvarium";
            int expectedCapacity = 50;

            string actualName = myAquarium.Name;
            int actualCap = myAquarium.Capacity;
            Assert.AreEqual(expectedName, actualName);
            Assert.AreEqual(expectedCapacity, actualCap);
        }

        [TestCase(null)]
        [TestCase("")]
        public void TestIfNullOrEmptyNameThrowsException(string name)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                this.myAquarium = new Aquarium(name, 55);
            });
        }

        [Test]
        public void TestIfNegativeCapacityThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                myAquarium = new Aquarium("Atanas", -10);
            });
        }
        [Test]
        public void TestIfCountReturnsCorrectly()
        {
            var fish = new Fish("Pesho");
            var anotherFIsh = new Fish("Nasko");
            myAquarium.Add(fish);
            myAquarium.Add(anotherFIsh);

            int expectedCount = 2;
            int actualCount = this.myAquarium.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void TestIfAddThrowsExceptionWhenFull()
        {
            for (int i = 1; i <= 50; i++)
            {
                string myFish = "Pesh" + i;
                this.myAquarium.Add(new Fish(myFish));
            }
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.myAquarium.Add(new Fish("Kiro"));
            });

        }

        [Test]
        public void TestIfRemoveFishThrowsExceptionIfNameDoesNotExists()
        {

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.myAquarium.RemoveFish("None");
            });
        }

        [Test]
        public void TestIfRemoveRemovesCorrectly()
        {
            var fish = new Fish("Pesho");
            var anotherFIsh = new Fish("Nasko");
            myAquarium.Add(fish);
            myAquarium.Add(anotherFIsh);

            myAquarium.RemoveFish(fish.Name);

            var expectedCount = 1;
            var actualCount = this.myAquarium.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }
        [Test]
        public void TestIfSellFishThrowsExceptionIfNameDoesNotExists()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.myAquarium.SellFish("Stamat");
            });
        }

        [Test]
        public void TestIfSellRightFish()
        {
            var fish = new Fish("Pesho");
            var anotherFIsh = new Fish("Nasko");
            myAquarium.Add(fish);
            myAquarium.Add(anotherFIsh);

            Fish soldFish = myAquarium.SellFish("Pesho");

            var expectedResult = false;
            var actualResult = fish.Available;

            Assert.AreEqual(expectedResult, actualResult);

        }

        [Test]
        public void TestIfSellReturnsSameFish()
        {
            var fish = new Fish("Pesho");
            var anotherFIsh = new Fish("Nasko");

            myAquarium.Add(fish);
            myAquarium.Add(anotherFIsh);

            var expectedName = anotherFIsh;
            var actualName = this.myAquarium.SellFish("Nasko");

            Assert.AreEqual(expectedName, actualName);
        }

        [Test]
        public void TestIfReportWorksCorrectly()
        {
            var fish = new Fish("Pesho");
            var anotherFIsh = new Fish("Nasko");

            myAquarium.Add(fish);
            myAquarium.Add(anotherFIsh);

            var expectedResult = "Fish available at Akvarium: Pesho, Nasko";

            var actualResult = myAquarium.Report();

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
