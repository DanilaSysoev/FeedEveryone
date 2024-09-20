using System;
using FeedEveryone.Core.Service;
using Microsoft.Xna.Framework;

namespace FeedEveryone.Mono.Components.TileProcessing;

public class HexTileDescriptor : WorldUnitSize, ITileDescriptor
{
    public Rectangle GetWorldRectangle(int line, int column)
    {
        int xPos = column * Width;
        if(line % 2 != 0)
            xPos += Width / 2;

        return new Rectangle(
            xPos,
            line * Width * 3 / 4,
            Width,
            Height
        );
    }

    public Position GetTileAtPoint(int x, int y)
    {
        int line = CalculateLine(x, y);
        int column = CalculateColumn(x, line);
        return new Position(line, column);
    }

    private int CalculateLine(int x, int y)
    {
        int lineSize = Width * 3 / 4;
        int posY = y - Height + Width;
        int line = posY / lineSize;
        if(posY < 0)
            --line;
        if ((posY - line * lineSize) * 3 >= lineSize)
            return line;

        int posX = CorrectionPosX(x, line);
        int column = posX / Width;

        if (LeftTopAngle(x, posY, line, column) ||
            RightTopAngle(x, posY, line, column))
        {
            return line - 1;
        }

        return line;
    }

    private int CorrectionPosX(int x, int line)
    {
        if (line % 2 != 0)
            return x - Width / 2;
        return x;
    }

    private bool LeftTopAngle(int x, int y, int line, int column)
    {
        int lineSize = Width * 3 / 4;
        int posX = CorrectionPosX(x, line);
        int relX = posX - column * Width;
        int relY = y - line * lineSize;

        return relX * 2 < Width &&
               relY * 4 < -2 * relX + Width;
    }
    private bool RightTopAngle(int x, int y, int line, int column)
    {
        int lineSize = Width * 3 / 4;
        int posX = CorrectionPosX(x, line);

        int relX = posX - column * Width;
        int relY = y - line * lineSize;

        return (posX - column * Width) * 2 > Width &&
               4 * relY < 2 * relX - Width;
    }

    private int CalculateColumn(int x, int line)
    {
        var cp = CorrectionPosX(x, line);
        var posPosition =  cp / Width;

        if(cp < 0)
            return posPosition - 1;
        return posPosition;
    }
}
