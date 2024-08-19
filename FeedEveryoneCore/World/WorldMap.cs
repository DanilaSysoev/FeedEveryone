using FeedEveryone.Service;

namespace FeedEveryone.World;

public class WorldMap
{


    public Tile this[int line, int column]
    {
        get => tiles[line, column];
        internal set => tiles[line, column] = value;
    }
    public Tile this[Position position]
    {
        get => tiles[position.Line, position.Column];
        internal set => tiles[position.Line, position.Column] = value;
    }


    internal WorldMap(int height, int width)
    {
        tiles = new Tile[height, width];
    }


    private readonly Tile[,] tiles;
}