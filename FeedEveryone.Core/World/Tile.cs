using FeedEveryone.Core.Exceptions;
using FeedEveryone.Core.Service;

namespace FeedEveryone.Core.World;

public class Tile
{
    public Position Position { get; private set; }
    public Biome Biome { get; private set; }

    public int Line => Position.Line;
    public int Column => Position.Column;
    
    public Tile(Position position, Biome biome)
    {
        Position = position;
        Biome = biome;
    }
    public Tile(int line, int column, Biome biome)
    {
        Position = new Position(line, column);
        Biome = biome;
    }
    
    public Settlement GetSettlement()
    {
        if (settlement is null)
            throw new SettlementNotAttachedException(this);
        return settlement;
    }
    public bool AttachedToSettlement()
    {
        return settlement is not null;
    }

    public override string ToString()
    {
        return $"{Position} {Biome}";
    }
    public override bool Equals(object? obj)
    {
        return obj is Tile tile &&
               Position.Equals(tile.Position) &&
               Biome.Equals(tile.Biome);
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(Position, Biome);
    }

    internal void DetachSettlement()
    {
        settlement = null;
    }
    internal void AttachSettlement(Settlement settlement)
    {
        if(AttachedToSettlement())
            throw new SettlementAlreadyAttachedException(this, GetSettlement());
        
        this.settlement = settlement;
    }


    private Settlement? settlement;
}