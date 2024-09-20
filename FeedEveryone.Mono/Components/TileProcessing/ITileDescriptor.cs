using FeedEveryone.Core.Service;
using Microsoft.Xna.Framework;

namespace FeedEveryone.Mono.Components.TileProcessing;

public interface ITileDescriptor
{
    int Height { get; }
    int Width { get; }

    Rectangle GetWorldRectangle(int line, int column);
    Rectangle GetWorldRectangle(Position pos)
    {
        return GetWorldRectangle(pos.Line, pos.Column);
    }
    Rectangle GetWorldDrawingRectangle(Point pos)
    {
        return GetWorldRectangle(pos.Y, pos.X);
    }

    Position GetTileAtPoint(Point worldPoint)
    {
        return GetTileAtPoint(worldPoint.X, worldPoint.Y);
    }
    Position GetTileAtPoint(int x, int y);
}
