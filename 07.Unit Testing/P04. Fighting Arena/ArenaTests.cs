using System;
using FightingArena;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ArenaTests
    {
        private Arena arena;
        private Warrior w1;
        private Warrior attacker;
        private Warrior deffender;

        [SetUp]
        public void Setup()
        {
            this.arena = new Arena();
            this.w1 = new Warrior("Pesho", 5, 50);

            this.attacker = new Warrior("Pesho", 10, 80);
            this.deffender = new Warrior("Gosho", 5, 60);
        }

        [Test]
        public void TestIfConstructorWorksCorrectly()
        {
            Assert.IsNotNull(this.arena.Warriors);
        }

        [Test]
        public void EnrollShouldPhysicallyAddTheWarriorToTheArena()
        {
            this.arena.Enroll(w1);

            Assert.That(this.arena.Warriors, Has.Member(this.w1));
        }

        [Test]
        public void EnrollShouldIncreaseCount()
        {
            this.arena.Enroll(this.w1);
            this.arena.Enroll(new Warrior("Gosho", 5, 60));

            int expectedCount = 2;
            int actualCount = this.arena.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void EnrollSameWarriorShouldThrowException()
        {
            this.arena.Enroll(this.w1);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.arena.Enroll(this.w1);
            });
        }

        [Test]
        public void EnrollTwoWarriorsWithSameNameShouldThrowException()
        {
            var w1Copy = new Warrior(w1.Name, w1.Damage, w1.HP);

            this.arena.Enroll(w1);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.arena.Enroll(w1Copy);
            });
        }

        [Test]
        public void TestFigthtingWihtMissingAttacker()
        {
            this.arena.Enroll(this.deffender);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.arena.Fight(this.attacker.Name, this.deffender.Name);
            });
        }
        [Test]
        public void TestFigthtingWihtMissingDeffender()
        {
            this.arena.Enroll(this.attacker);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.arena.Fight(this.attacker.Name, this.deffender.Name);
            });
        }

        [Test]
        public void TestFigthBetweenTwoWarrioirs()
        {
            int expectedAHP = this.attacker.HP - this.deffender.Damage;
            int expectedDHP = this.deffender.HP - this.attacker.Damage;

            this.arena.Enroll(attacker);
            this.arena.Enroll(deffender);

            this.arena.Fight(this.attacker.Name, this.deffender.Name);

            Assert.AreEqual(expectedAHP,this.attacker.HP);
            Assert.AreEqual(expectedDHP,this.deffender.HP);
        }
    }
}
