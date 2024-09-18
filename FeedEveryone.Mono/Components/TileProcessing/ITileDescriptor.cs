using FeedEveryone.Core.Service;
using Microsoft.Xna.Framework;

namespace FeedEveryone.Mono.Components.TileProcessing;

public interface ITileDescriptor
{
    int Height { get; }
    int Width { get; }

    Rectangle GetWorldDrawingRectangle(int line, int column);
    Rectangle GetWorldDrawingRectangle(Position pos)
    {
        return GetWorldDrawingRectangle(pos.Line, pos.Column);
    }
    Rectangle GetWorldDrawingRectangle(Point pos)
    {
        return GetWorldDrawingRectangle(pos.Y, pos.X);
    }

    Rectangle MoveOnNextLine(Rectangle currentRectangle, int tileLine);
    Rectangle MoveOnNextColumn(Rectangle currentRectangle);

}
