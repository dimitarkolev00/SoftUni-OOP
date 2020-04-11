using System;
using NUnit.Framework;
[TestFixture]
public class HeroRepositoryTests
{
    private HeroRepository data;
    [SetUp]
    public void SetUp()
    {
        this.data = new HeroRepository();
    }
    [Test]
    public void TestCreateThrowsExceptionWithNull()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            this.data.Create(null);
        });
    }

    [Test]
    public void TestCreateWithSameHeroThrowsException()
    {
        Hero hero = new Hero("Stamat", 50);

        this.data.Create(hero);

        Assert.Throws<InvalidOperationException>(() =>
        {
            this.data.Create(new Hero("Stamat", 60));
        });
    }
    [Test]
    public void TestCreateAddsCorrectly()
    {
        var expectedResult = $"Successfully added hero Jivko with level 50"; ;
        var actualResult = this.data.Create(new Hero("Jivko",50));

        Assert.AreEqual(expectedResult,actualResult);
    }

    [TestCase(null)]
    [TestCase("     ")]
    public void TestRemoveThrowsExceptionWithNullName(string name)
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            this.data.Remove(name);
        });
    }

    [Test]
    public void TestRemoveReturnsIsRemovedWhenSuccessfullyRemoved()
    {
        Hero hero = new Hero("Joro",45);
        this.data.Create(hero);

        var expectedResult = true;
        var actualResult = this.data.Remove("Joro");

        Assert.AreEqual(expectedResult,actualResult);
    }

    [Test]
    public void TestGetHeroWithHighestLevel()
    {
        Hero hero = new Hero("Joro", 45);
        Hero hero2 = new Hero("Stamen",80);
        this.data.Create(hero);
        this.data.Create(hero2);

        var expectedResult = hero2;
        var actualResult = this.data.GetHeroWithHighestLevel();

        Assert.AreEqual(expectedResult,actualResult);
    }
    [Test]
    public void TestGetHeroReturnsRightHero()
    {
        Hero hero = new Hero("Joro", 45);
        Hero hero2 = new Hero("Stamen", 80);
        this.data.Create(hero);
        this.data.Create(hero2);

        var expectedResult = hero;
        var actualResult = this.data.GetHero("Joro");

        Assert.AreEqual(expectedResult, actualResult);
    }

}