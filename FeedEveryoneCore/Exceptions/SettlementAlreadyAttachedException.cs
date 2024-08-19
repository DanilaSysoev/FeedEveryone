using FeedEveryone.World;

namespace FeedEveryone.Exceptions;

public class SettlementAlreadyAttachedException : Exception
{
    public SettlementAlreadyAttachedException(Tile tile, Settlement settlement)
        : base($"Tile {tile} already has attached settlement: {settlement}")
    {
    }
}