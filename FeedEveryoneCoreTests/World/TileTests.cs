using FeedEveryone.Exceptions;
using FeedEveryone.World;

namespace FeedEveryoneTests.World;

public class TileTests
{
    [Test]
    public void Equals_TilesWithSamePositionAndBiome_ReturnsTrue()
    {
        var tile1 = new Tile(10, 20, new Biome("b1"));
        var tile2 = new Tile(10, 20, new Biome("b1"));

        Assert.That(tile1, Is.EqualTo(tile2));
    }
    [Test]
    public void Equals_TilesWithSamePositionButDifferentBiome_ReturnsFalse()
    {
        var tile1 = new Tile(10, 20, new Biome("b1"));
        var tile2 = new Tile(10, 20, new Biome("b2"));

        Assert.That(tile1, Is.Not.EqualTo(tile2));
    }
    [Test]
    public void Equals_TilesWithDifferentPositionButSameBiome_ReturnsFalse()
    {
        var tile1 = new Tile(10, 20, new Biome("b1"));
        var tile2 = new Tile(11, 21, new Biome("b1"));

        Assert.That(tile1, Is.Not.EqualTo(tile2));
    }
    [Test]
    public void Equals_TilesWithDifferentPositionAndDifferentBiome_ReturnsFalse()
    {
        var tile1 = new Tile(10, 20, new Biome("b1"));
        var tile2 = new Tile(11, 21, new Biome("b2"));

        Assert.That(tile1, Is.Not.EqualTo(tile2));
    }

    [Test]
    public void GetSettlement_TileNotAdded_ThrowsError()
    {
        var tile = new Tile(0, 0, new Biome("b1"));

        var exc = Assert.Throws<SettlementNotAttachedException>(
            () => tile.GetSettlement()
        );
        Assert.That(
            exc!.Message,
            Contains.Substring("Tile [0, 0] b1 has not attached settlement")
        );
    }
    [Test]
    public void GetSettlement_AddSomeTile_SettlementForTileSetCorrectly()
    {
        var settlement = new Settlement("Test");
        var tile = new Tile(0, 0, new Biome("b1"));
        settlement.AddTile(tile);

        Assert.That(tile.GetSettlement(), Is.SameAs(settlement));
    }
    [Test]
    public void GetSettlement_TileAddedThenRemoved_ThrowsError()
    {
        var settlement = new Settlement("Test");
        var tile = new Tile(0, 0, new Biome("b1"));
        settlement.AddTile(tile);
        settlement.RemoveTile(tile);

        var exc = Assert.Throws<SettlementNotAttachedException>(
            () => tile.GetSettlement()
        );
        Assert.That(
            exc!.Message,
            Contains.Substring("Tile [0, 0] b1 has not attached settlement")
        );
    }

    [Test]
    public void AttachedToSettlement_TileNotAdded_ReturnsFalse()
    {
        var tile = new Tile(0, 0, new Biome("b1"));

        Assert.That(tile.AttachedToSettlement(), Is.False);
    }
    [Test]
    public void AttachedToSettlement_AddSomeTile_ReturnsTrue()
    {
        var settlement = new Settlement("Test");
        var tile = new Tile(0, 0, new Biome("b1"));
        settlement.AddTile(tile);

        Assert.That(tile.AttachedToSettlement(), Is.True);
    }
    [Test]
    public void AttachedToSettlement_TileAddedThenRemoved_ReturnsFalse()
    {
        var settlement = new Settlement("Test");
        var tile = new Tile(0, 0, new Biome("b1"));
        settlement.AddTile(tile);
        settlement.RemoveTile(tile);

        Assert.That(tile.AttachedToSettlement(), Is.False);
    }
}