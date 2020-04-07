using System;
using FightingArena;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class WarriorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestIfConstructorWorksCorrectly()
        {
            string expectedName = "Pesho";
            int expectedDmg = 50;
            int expectedHP = 100;

            var warrior = new Warrior(expectedName, expectedDmg, expectedHP);

            string actualName = warrior.Name;
            int actualDmg = warrior.Damage;
            int actualHP = warrior.HP;

            Assert.AreEqual(expectedName, actualName);
            Assert.AreEqual(expectedDmg, actualDmg);
            Assert.AreEqual(expectedHP, actualHP);
        }

        [Test]
        public void TestWithLikeNullName()
        {
            string name = null;
            int dmg = 50;
            int hp = 100;

            Assert.Throws<ArgumentException>(() =>
            {
                var warrior = new Warrior(name, dmg, hp);
            });
        }

        [Test]
        public void TestWithLikeEmptyName()
        {
            string name = String.Empty;
            int dmg = 50;
            int hp = 100;

            Assert.Throws<ArgumentException>(() =>
            {
                var warrior = new Warrior(name, dmg, hp);
            });
        }

        [Test]
        public void TestWithLikeWhitespaceName()
        {
            string name = "            ";
            int dmg = 50;
            int hp = 100;

            Assert.Throws<ArgumentException>(() =>
            {
                var warrior = new Warrior(name, dmg, hp);
            });
        }

        [Test]
        public void TestWithLikeZeroDamage()
        {
            string name = "Pesho";
            int dmg = 0;
            int hp = 100;

            Assert.Throws<ArgumentException>(() =>
            {
                var warrior = new Warrior(name, dmg, hp);
            });
        }
        [Test]
        public void TestWithLikeNegativeDamage()
        {
            string name = "Pesho";
            int dmg = -5;
            int hp = 100;

            Assert.Throws<ArgumentException>(() =>
            {
                var warrior = new Warrior(name, dmg, hp);
            });
        }

        [Test]
        public void TestWithLikeNegativeHP()
        {
            string name = "Pesho";
            int dmg = 50;
            int hp = -10;

            Assert.Throws<ArgumentException>(() =>
            {
                var warrior = new Warrior(name, dmg, hp);
            });
        }

        [TestCase(25)]
        [TestCase(30)]
        public void AttackingEnemyWhenLowHpShouldThrowException(int attackerHP)
        {
            string attackerName = "Pesho";
            int attackerDmg = 50;
            //int attackerHP = 10;
            var attackerWarrior = new Warrior(attackerName, attackerDmg, attackerHP);

            string enemyName = "Gosho";
            int enemyDmg = 50;
            int enemyHp = 100;
            var enemyWarrior = new Warrior(enemyName, enemyDmg, enemyHp);

            Assert.Throws<InvalidOperationException>(() =>
            {
                attackerWarrior.Attack(enemyWarrior);
            });
        }

        [TestCase(25)]
        [TestCase(30)]
        public void AttackingEnemyWithLowHpShouldThrowException(int defenderHp)
        {
            string attackerName = "Pesho";
            int attackerDmg = 50;
            int attackerHp = 100;

            var attacker = new Warrior(attackerName, attackerDmg, attackerHp);

            string defenderName = "Gosho";
            int defederDmg = 50;

            var defender = new Warrior(defenderName, defederDmg, defenderHp);

            Assert.Throws<InvalidOperationException>(() =>
            {
                attacker.Attack(defender);
            });
        }

        [Test]
        public void AttackingStrongerEnemyShouldThrowException()
        {
            string attackerName = "Pesho";
            int attackerDmg = 50;
            int attackerHp = 35;

            var attacker = new Warrior(attackerName, attackerDmg, attackerHp);

            string enemyName = "Gosho";
            int enemyDmg = 40;
            int enemyHp = 35;

            var enemy = new Warrior(enemyName, enemyDmg, enemyHp);

            Assert.Throws<InvalidOperationException>(() =>
            {
                attacker.Attack(enemy);
            });
        }

        [Test]
        public void AttackShouldDecreaseHpWhenSuccessfull()
        {
            //Arrange
            string attackerName = "Pesho";
            int attackerDmg = 10;
            int attackerHp = 40;

            var attacker = new Warrior(attackerName, attackerDmg, attackerHp);

            string enemyName = "Gosho";
            int enemyDmg = 5;
            int enemyHp = 50;

            var enemy = new Warrior(enemyName, enemyDmg, enemyHp);

            int expectedAttackerHp = attackerHp - enemyDmg;
            int expectedEnemyHp = enemyHp - attackerDmg;

            //Act
            attacker.Attack(enemy);

            //Assert
            Assert.AreEqual(expectedAttackerHp, attacker.HP);
            Assert.AreEqual(expectedEnemyHp, enemy.HP);

        }

        [Test]
        public void KillingEnemyWithAttack()
        {
            string attackerName = "Pesho";
            int attackerDmg = 80;
            int attackerHp = 100;

            string enemyName = "Gosho";
            int enemyDmg = 10;
            int enemyHp = 60;

            var attacker = new Warrior(attackerName, attackerDmg, attackerHp);
            var enemy = new Warrior(enemyName, enemyDmg, enemyHp);

            int expectedAttackerHp = attackerHp - enemyDmg; //90
            int expectedEnemyHp = 0;

            attacker.Attack(enemy);

            Assert.AreEqual(expectedEnemyHp, enemy.HP);
            Assert.AreEqual(expectedAttackerHp,attacker.HP);
        }
    }
}