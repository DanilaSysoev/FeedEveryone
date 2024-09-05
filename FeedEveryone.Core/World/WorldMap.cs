using FeedEveryone.Core.Service;

namespace FeedEveryone.Core.World;

public class WorldMap
{
    public string Name { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }

    public Tile this[int line, int column]
    {
        get => tiles[line, column];
        set => tiles[line, column] = value;
    }
    public Tile this[Position position]
    {
        get => tiles[position.Line, position.Column];
        set => tiles[position.Line, position.Column] = value;
    }
    
    public override bool Equals(object? obj)
    {
        return obj is WorldMap map &&
               Name == map.Name;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(Name);
    }
    public override string ToString()
    {
        return Name;
    }


    public WorldMap(string name, int height, int width)
    {
        Name = name;
        Height = height;
        Width = width;
        tiles = new Tile[height, width];
    }


    private readonly Tile[,] tiles;
}