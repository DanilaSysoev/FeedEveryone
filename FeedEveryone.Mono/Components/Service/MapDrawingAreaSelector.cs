using FeedEveryone.Mono.Components.TileProcessing;
using Microsoft.Xna.Framework;

namespace FeedEveryone.Mono.Components.Service;

public class MapDrawingAreaSelector
{
    public MapDrawingAreaSelector(
        Vector2 cameraPosition,
        float cameraHeight,
        float cameraWidth,
        WorldUnitSize tileSize,
        int additionalTiles
    )
    {
        this.cameraPosition = cameraPosition;
        this.cameraHeight = cameraHeight;
        this.cameraWidth = cameraWidth;
        this.tileSize = tileSize;
        this.additionalTiles = additionalTiles;
    }

    public Rectangle GetDrawingArea()
    {
        var area = new Rectangle(
            CalculateStartDrawingCol(),
            CalculateStartDrawingLine(),
            CalculateDrawingWidth(),
            CalculateDrawingHeight()
        );

        return area;
    }

    private int CalculateDrawingHeight()
    {
        int step = tileSize.Width * 3 / 4;
        int offset = tileSize.Height - tileSize.Width;

        return ((int)cameraHeight - offset) / step + additionalTiles;
    }
    private int CalculateDrawingWidth()
    {
        return (int)(cameraWidth / tileSize.Width) + additionalTiles;
    }

    private int CalculateStartDrawingLine()
    {
        int camPos = (int)cameraPosition.Y;
        int step = tileSize.Width * 3 / 4;
        int res = camPos / step;
        if(camPos >= 0 && camPos % step != 0)
            res++;
        return res - additionalTiles / 2;
    }

    private int CalculateStartDrawingCol()
    {
        var cutted = (int)cameraPosition.X / tileSize.Width;
        if ((int)cameraPosition.X % tileSize.Width > 0)
            cutted += 1;
        return cutted - additionalTiles / 2;
    }


    private readonly Vector2 cameraPosition;
    private readonly float cameraHeight;
    private readonly float cameraWidth;
    private readonly WorldUnitSize tileSize;

    public readonly int additionalTiles;
}
