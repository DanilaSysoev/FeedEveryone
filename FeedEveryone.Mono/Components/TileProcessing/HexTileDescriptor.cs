using System;
using FeedEveryone.Core.Service;
using Microsoft.Xna.Framework;

namespace FeedEveryone.Mono.Components.TileProcessing;

public class HexTileDescriptor : WorldUnitSize, ITileDescriptor
{
    public Rectangle MoveOnNextColumn(
        Rectangle currentRectangle
    )
    {
        Rectangle drawRect = currentRectangle;
        drawRect.X += drawRect.Width;
        return drawRect;
    }

    public Rectangle MoveOnNextLine(
        Rectangle currentRectangle, int tileLine
    )
    {
        Rectangle drawRect = currentRectangle;
        if (tileLine % 2 == 0)
            drawRect.X += drawRect.Width / 2;
        else
            drawRect.X -= drawRect.Width / 2;
        drawRect.Y += drawRect.Width * 3 / 4;
        return drawRect;
    }

    public Rectangle GetWorldDrawingRectangle(int line, int column)
    {
        return new Rectangle(
            column * Width + Width * (line % 2) / 2,
            line * Width * 3 / 4,
            Width,
            Height
        );
    }
}
