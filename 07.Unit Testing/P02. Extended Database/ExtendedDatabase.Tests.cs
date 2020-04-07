using System;
using ExtendedDatabase;
using NUnit.Framework;


namespace Tests
{
    [TestFixture]
    public class ExtendedDatabaseTests
    {
        private ExtendedDatabase.ExtendedDatabase extendedDatabase;
        private Person person = new Person(20, "Pesho");

        [SetUp]
        public void Setup()
        {
            this.extendedDatabase = new ExtendedDatabase.ExtendedDatabase(person);
        }

        [Test]
        public void TestIfPersonConstructorWorksCorrectly()
        {
            //Arrange

            //Act
            var expectedId = 20;
            var actualId = this.person.Id;

            var expectedName = "Pesho";
            var actualName = this.person.UserName;

            //Assert
            Assert.AreEqual(expectedId, actualId);
            Assert.AreEqual(expectedName, actualName);

        }

        [Test]
        public void TestIfConstructorWorksCorrectly()
        {
            this.extendedDatabase.Add(new Person(40, "Stamat"));
            int actualCount = this.extendedDatabase.Count;
            int expectedCount = 2;

            var extendedEmptyDatabase = new ExtendedDatabase.ExtendedDatabase();
            var expectedEmpty = 0;
            var actualEmpty = extendedEmptyDatabase.Count;

            Assert.AreEqual(expectedCount, actualCount);
            Assert.AreEqual(expectedEmpty, actualEmpty);
        }

        [Test]
        public void AddingPeronWithSameNameShouldThrowException()
        {
            var newPerson = new Person(10, "Pesho");

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.extendedDatabase.Add(newPerson);
            });
        }

        [Test]
        public void AddingPersonWithSameIdShouldThrowException()
        {
            var newPerson = new Person(20, "Grigor");
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.extendedDatabase.Add(newPerson);
            });
        }

        [Test]
        public void AddingMorePeopleShouldThrowException()
        {
            for (int i = 2; i <= 16; i++)
            {
                this.extendedDatabase.Add(new Person(i, "p" + i));
            }

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.extendedDatabase.Add(new Person(30, "Shrisho"));
            });

        }

        [Test]
        public void RemoveShouldDecreaseCount()
        {
            //Arrange
            var newPerson = new Person(25, "Stamen");
            this.extendedDatabase.Add(newPerson);
            int expectedCount = 1;
            //Act
            this.extendedDatabase.Remove();
            int actualCount = this.extendedDatabase.Count;
            //Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void RemoveShouldThrowExceptionWhenThereIsNoPerson()
        {
            this.extendedDatabase.Remove();
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.extendedDatabase.Remove();
            });
        }

        [TestCase(null)]
        [TestCase("")]
        public void TestFindByUsernameShouldThrowExceptionWhenNameIsNullOrEmpty(string name)
        {

            Assert.Throws<ArgumentNullException>(() =>
            {
                this.extendedDatabase.FindByUsername(name);
            });
        }

        [Test]
        public void TestFindByUsernameIfThereIsNoUserPresentedWithThisNameShouldThrowException()
        {
            var secondPerson = new Person(50, "Grigor");
            string name = "doesNotContainThisUser";
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.extendedDatabase.FindByUsername(name);
            });
        }

        [Test]
        public void TestIfFindByUsernameIsCaseSensitive()
        {
            var secondPerson = new Person(50, "Grigor");
            this.extendedDatabase.Add(secondPerson);
            string name = "grigor";
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.extendedDatabase.FindByUsername(name);
            });
        }

        [Test]
        public void TestFoundByUsernameShouldReturnSamePerson()
        {
            var secondPerson = new Person(9,"Iskren");
            this.extendedDatabase.Add(secondPerson);
            var expectedPerson = secondPerson;
            var actualPerson = this.extendedDatabase.FindByUsername(secondPerson.UserName);

            Assert.AreEqual(expectedPerson,actualPerson);
        }

        [Test]
        public void TestIfFindByIDThrowsExceptionIfThereIsNoPersonFoundWithThisID()
        {
            var secondPerson = new Person(50, "Grigor");
            this.extendedDatabase.Add(secondPerson);
            var noIdFound = 83;
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.extendedDatabase.FindById(noIdFound);
            });
        }

        [Test]
        public void TestFoundByIDShouldThrowExceptionWhenIDIsNegative()
        {
            var newPerson = new Person(-2, "Kris");
            this.extendedDatabase.Add(newPerson);
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                this.extendedDatabase.FindById(newPerson.Id);
            });
        }

        [Test]
        public void TestFoundByIdShouldReturnSamePerson()
        {
            var secondPerson = new Person(66, "Koko");
            this.extendedDatabase.Add(secondPerson);
            var expectedPerson = secondPerson;
            var actualPerson = this.extendedDatabase.FindById(secondPerson.Id);
            Assert.AreEqual(expectedPerson, actualPerson);
        }
    }
}