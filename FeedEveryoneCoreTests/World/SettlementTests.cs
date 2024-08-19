using FeedEveryone.Exceptions;
using FeedEveryone.World;

namespace FeedEveryoneTests.World;

public class SettlementTests
{
    [Test]
    public void Equals_SettlementsWithSameName_ReturnsTrue()
    {
        var settlement1 = new Settlement("Test");
        var settlement2 = new Settlement("Test");

        Assert.That(settlement1, Is.EqualTo(settlement2));
    }
    [Test]
    public void Equals_SettlementsWithDifferentName_ReturnsFalse()
    {
        var settlement1 = new Settlement("Test1");
        var settlement2 = new Settlement("Test2");

        Assert.That(settlement1, Is.Not.EqualTo(settlement2));
    }

    [Test]
    public void AddTile_AddsTileToSettlement_DoesntThrow()
    {
        var settlement = new Settlement("Test");
        var tile = new Tile(0, 0, new Biome("b1"));

        Assert.DoesNotThrow(() => settlement.AddTile(tile));
    }
    [Test]
    public void AddTile_AddsTileSecondTimeToSettlement_ThrowError()
    {
        var settlement = new Settlement("Test");
        var tile = new Tile(0, 0, new Biome("b1"));

        settlement.AddTile(tile);
        var exc = Assert.Throws<TileAlreadyInSettlementException>(
            () => settlement.AddTile(tile)
        );
        Assert.That(
            exc!.Message,
            Contains.Substring("Tile [0, 0] b1 already in settlement Test")
        );
    }
    [Test]
    public void AddTile_TileNotAdded_SettlementForTileNotAttached()
    {
        var tile = new Tile(0, 0, new Biome("b1"));

        Assert.That(tile.AttachedToSettlement(), Is.False);
    }

    [Test]
    public void ContainsTile_TileNotAdded_ReturnsFalse()
    {
        var settlement = new Settlement("Test");
        var tile = new Tile(0, 0, new Biome("b1"));

        Assert.That(settlement.ContainsTile(tile), Is.False);
    }
    [Test]
    public void ContainsTile_TileAdded_ReturnsTrue()
    {
        var settlement = new Settlement("Test");
        var tile = new Tile(0, 0, new Biome("b1"));

        settlement.AddTile(tile);
        Assert.That(settlement.ContainsTile(tile), Is.True);
    }


    [Test]
    public void RemoveTile_RemoveNotExistsTile_ThrowError()
    {
        var settlement = new Settlement("Test");
        var tile = new Tile(0, 0, new Biome("b1"));

        var exc = Assert.Throws<TileNotInSettlementException>(
            () => settlement.RemoveTile(tile)
        );
        Assert.That(
            exc!.Message,
            Contains.Substring("Tile [0, 0] b1 is not in settlement Test")
        );
    }
    [Test]
    public void RemoveTile_RemoveAfterAdding_DoesNotThrow()
    {
        var settlement = new Settlement("Test");
        var tile = new Tile(0, 0, new Biome("b1"));

        settlement.AddTile(tile);
        Assert.DoesNotThrow(() => settlement.RemoveTile(tile));
    }
}