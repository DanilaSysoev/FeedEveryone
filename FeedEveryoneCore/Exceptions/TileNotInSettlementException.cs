using FeedEveryone.World;

namespace FeedEveryone.Exceptions;

public class TileNotInSettlementException : Exception
{
    public TileNotInSettlementException(Settlement settlement, Tile tile)
        : base($"Tile {tile} is not in settlement {settlement}")
    {
    }
}