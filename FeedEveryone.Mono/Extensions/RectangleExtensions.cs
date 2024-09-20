using Microsoft.Xna.Framework;

namespace FeedEveryone.Mono.Extensions;

public static class RectangleExtensions
{
    public static Rectangle Expand(this Rectangle rect, int value)
    {
        return new Rectangle(
            rect.X - value,
            rect.Y - value,
            rect.Width + value + value,
            rect.Height + value + value
        );
    }
    public static Rectangle Expand(this Rectangle rect, int x, int y)
    {
        return new Rectangle(
            rect.X - x,
            rect.Y - y,
            rect.Width + x + x,
            rect.Height + y + y
        );
    }
}
