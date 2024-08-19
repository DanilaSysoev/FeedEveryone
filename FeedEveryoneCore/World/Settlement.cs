using FeedEveryone.Exceptions;
using FeedEveryone.Service;

namespace FeedEveryone.World;

public class Settlement
{
    public string Name { get; private set; }

    internal Settlement(string name)
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
    public bool Contains(Tile tile)
    {
        return tiles.ContainsKey(tile.Position);
    }

    public override string ToString()
    {
        return Name;
    }

    private readonly Dictionary<Position, Tile> tiles;
}