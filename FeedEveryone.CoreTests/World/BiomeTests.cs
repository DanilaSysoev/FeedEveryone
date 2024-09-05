using FeedEveryone.Core.World;

namespace FeedEveryoneTests.World;

public class BiomeTests
{
    [Test]
    public void Equals_BiomesWithSameName_ReturnsTrue()
    {
        var biome1 = new Biome(.5f, .5f, .5f, .5f);
        var biome2 = new Biome(.5f, .5f, .5f, .5f);

        Assert.That(biome1, Is.EqualTo(biome2));
    }
    [Test]
    public void BiomesWithDifferentName_ReturnsFalse()
    {
        var biome1 = new Biome(.5f, .5f, .5f, .5f);
        var biome2 = new Biome(.5f, .5f, .5f, .6f);

        Assert.That(biome1, Is.Not.EqualTo(biome2));
    }

    [Test]
    public void ToString_ReturnsName()
    {
        var biome = new Biome(.5f, .5f, .5f, .5f);

        Assert.That(
            biome.ToString(),
            Is.EqualTo("0.50 0.50 0.50 0.50"));
    }
}