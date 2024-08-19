using FeedEveryone.Exceptions;
using FeedEveryone.Service;

namespace FeedEveryone.World;

public class Tile
{
    public Position Position { get; private set; }
    public Biome Biome { get; private set; }

    public int Line => Position.Line;
    public int Column => Position.Column;

    public override string ToString()
    {
        return $"{Position} {Biome}";
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
    internal Tile(Position position, Biome biome)
    {
        Position = position;
        Biome = biome;
    }

    private Settlement? settlement;
}