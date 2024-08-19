using FeedEveryone.Exceptions;
using FeedEveryone.Service;

namespace FeedEveryone.World;

public class Settlement
{
    public string Name { get; private set; }

    public Settlement(string name)
    {
        Name = name;
        tiles = new Dictionary<Position, Tile>();
    }

    public void AddTile(Tile tile)
    {
        if (tiles.ContainsKey(tile.Position))
            throw new TileAlreadyInSettlementException(this, tile);

        tiles.Add(tile.Position, tile);
        tile.AttachSettlement(this);
    }
    public void RemoveTile(Tile tile)
    {
        if (!tiles.ContainsKey(tile.Position))
            throw new TileNotInSettlementException(this, tile);

        tiles.Remove(tile.Position);
        tile.DetachSettlement();
    }
    public bool ContainsTile(Tile tile)
    {
        return tiles.ContainsKey(tile.Position);
    }

    public override string ToString()
    {
        return Name;
    }
    public override bool Equals(object? obj)
    {
        return obj is Settlement settlement &&
               Name == settlement.Name;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(Name);
    }

    private readonly Dictionary<Position, Tile> tiles;
}