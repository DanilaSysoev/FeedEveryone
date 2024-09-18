using System;
using FeedEveryone.Mono.Components.TileProcessing;

namespace FeedEveryone.Mono.Builders;

public class HexTileDescriptorBuilder : IBuilder<ITileDescriptor>
{
    public HexTileDescriptorBuilder(int tileHeight, int tileWidth)
    {
        this.tileHeight = tileHeight;
        this.tileWidth = tileWidth;
    }

    public ITileDescriptor Build()
    {
        return new HexTileDescriptor()
        {
            Height = tileHeight,
            Width = tileWidth,
        };
    }

    private readonly int tileHeight;
    private readonly int tileWidth;
}
