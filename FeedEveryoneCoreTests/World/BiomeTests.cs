using FeedEveryone.World;

namespace FeedEveryoneTests.World;

public class BiomeTests
{
    [Test]
    public void Equals_BiomesWithSameName_ReturnsTrue()
    {
        var biome1 = new Biome("test");
        var biome2 = new Biome("test");

        Assert.That(biome1, Is.EqualTo(biome2));
    }
    [Test]
    public void BiomesWithDifferentName_ReturnsFalse()
    {
        var biome1 = new Biome("test");
        var biome2 = new Biome("test2");

        Assert.That(biome1, Is.Not.EqualTo(biome2));
    }

    [Test]
    public void ToString_ReturnsName()
    {
        var biome = new Biome("test");

        Assert.That(biome.ToString(), Is.EqualTo("test"));
    }
}