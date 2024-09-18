using System;
using FeedEveryone.Core.World;

namespace FeedEveryone.Mono.Components.TileProcessing;

public interface ITileTypeQualifier
{
    TileType Qualify(Tile tile);
}
