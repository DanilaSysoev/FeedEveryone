using System;
using FeedEveryone.Mono.Components.TileProcessing;

namespace FeedEveryone.Mono.Exceptions;

public class UndefinedTileTextureException : Exception
{
    public UndefinedTileTextureException(TileType tileType, string message)
        : base($"{tileType}: {message}")
    {}
}
