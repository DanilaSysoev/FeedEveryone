using FeedEveryone.Core.World;

namespace FeedEveryone.Core.Exceptions;

public class SettlementNotAttachedException : Exception
{
    public SettlementNotAttachedException(Tile tile)
        : base($"Tile {tile} has not attached settlement")
    {
    }
}