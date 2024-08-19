using FeedEveryone.World;

namespace FeedEveryoneTests.World;

public class WorldMapTests
{
    [Test]
    public void Equals_WorldsWithSameName_ReturnsTrue()
    {
        var world1 = new WorldMap("world1", 10, 10);
        var world2 = new WorldMap("world1", 10, 10);

        Assert.That(world1, Is.EqualTo(world2));
    }
    [Test]
    public void Equals_WorldsWithDifferentName_ReturnsFalse()
    {
        var world1 = new WorldMap("world1", 10, 10);
        var world2 = new WorldMap("world2", 10, 10);

        Assert.That(world1, Is.Not.EqualTo(world2));
    }

    [Test]
    public void ToString_ReturnsWorldName()
    {
        var world = new WorldMap("world1", 10, 10);

        Assert.That(world.ToString(), Is.EqualTo("world1"));
    }
}