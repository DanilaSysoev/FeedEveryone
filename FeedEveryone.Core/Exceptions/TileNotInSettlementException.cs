using FeedEveryone.Core.World;

namespace FeedEveryone.Core.Exceptions;

public class TileNotInSettlementException : Exception
{
    public TileNotInSettlementException(Settlement settlement, Tile tile)
        : base($"Tile {tile} is not in settlement {settlement}")
    {
    }
}