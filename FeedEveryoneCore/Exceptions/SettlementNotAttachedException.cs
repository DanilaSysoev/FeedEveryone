using FeedEveryone.World;

namespace FeedEveryone.Exceptions;

public class SettlementNotAttachedException : Exception
{
    public SettlementNotAttachedException(Tile tile)
        : base($"Tile {tile} has not attached settlement")
    {
    }
}