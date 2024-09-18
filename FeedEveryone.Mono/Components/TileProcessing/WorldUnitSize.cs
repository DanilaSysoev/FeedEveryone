using System;

namespace FeedEveryone.Mono.Components.TileProcessing;

public class WorldUnitSize
{
    public int Height { get; set; }
    public int Width { get; set; }

    public WorldUnitSize()
    {}

    public WorldUnitSize(int height, int width)
    {
        Height = height;
        Width = width;
    }
}
