using FeedEveryone.Core.World;

namespace FeedEveryone.Core.Exceptions;

public class SettlementAlreadyAttachedException : Exception
{
    public SettlementAlreadyAttachedException(Tile tile, Settlement settlement)
        : base($"Tile {tile} already has attached settlement: {settlement}")
    {
    }
}