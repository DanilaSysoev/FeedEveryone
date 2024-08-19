using FeedEveryone.Service;

namespace FeedEveryone.World;

public class WorldMap
{
    public string Name { get; private set; }

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


    internal WorldMap(string name, int height, int width)
    {
        Name = name;
        tiles = new Tile[height, width];
    }


    private readonly Tile[,] tiles;
}