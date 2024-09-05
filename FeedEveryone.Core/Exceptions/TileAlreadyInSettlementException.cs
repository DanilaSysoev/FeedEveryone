using FeedEveryone.Core.World;

namespace FeedEveryone.Core.Exceptions;

public class TileAlreadyInSettlementException : Exception
{
    public TileAlreadyInSettlementException(Settlement settlement, Tile tile)
        : base($"Tile {tile} already in settlement {settlement}")
    {
    }
}