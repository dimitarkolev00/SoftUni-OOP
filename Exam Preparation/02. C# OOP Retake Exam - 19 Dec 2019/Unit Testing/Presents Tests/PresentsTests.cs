using NUnit.Framework;

namespace Presents.Tests
{
    using System;
    [TestFixture]
    public class PresentsTests
    {
        private Bag bag;
        private Present present;
        
        [SetUp]
        public void SetUp()
        {
            this.bag = new Bag();
            this.present = new Present("Obuvki", 50.5);
        }
        [Test]
        public void TestIfPresentConstructorWorksCorrectly()
        {
            Present present = new Present("Obuvki", 50.5);
            var expectedName = "Obuvki";
            var expectedMagic = 50.5;

            var actualName = present.Name;
            var actualMagic = present.Magic;

            Assert.AreEqual(expectedName, actualName);
            Assert.AreEqual(expectedMagic, actualMagic);
        }

        [Test]
        public void TestIfCreateThrowsExceptionWithNullPresent()
        {
            
            Assert.Throws<ArgumentNullException>(() =>
            {
                bag.Create(null);
            });
        }

        [Test]
        public void TestCreateWithSamePersonThrowsException()
        {
            this.bag.Create(this.present);
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.bag.Create(this.present);
            });
        }

        [Test]
        public void TestCreateAddsCorrectly()
        {
            this.bag.Create(this.present);
            var expectedName = "Obuvki";
            var expectedMagic = 50.5;

            var actualName = this.present.Name;
            var actualMagic = this.present.Magic;
            Assert.AreEqual(expectedName,actualName);
            Assert.AreEqual(expectedMagic,actualMagic);
        }

        [Test]
        public void TestRemoveWorksCorrectly()
        {
            this.bag.Create(this.present);

            Assert.That(this.bag.Remove(present));
        }

        [Test]
        public void TestGetPresentWithLeastMagic()
        {
            this.bag.Create(present);
            this.bag.Create(new Present("Kolan", 100.5));

            Assert.That(this.bag.GetPresentWithLeastMagic()==this.present);
        }
        [Test]
        public void TestGetPresentReturnCorrectly()
        {
            this.bag.Create(present);
            this.bag.Create(new Present("Kolan", 100.5));

            Assert.That(this.bag.GetPresent("Obuvki") == this.present);
        }

    }
}
