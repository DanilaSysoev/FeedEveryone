namespace FeedEveryone.World;

public class Biome
{
    public string Name { get; private set; }

    public override string ToString()
    {
        return Name;
    }
    public override bool Equals(object? obj)
    {
        return obj is Biome biome &&
               Name == biome.Name;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(Name);
    }

    internal Biome(string name)
    {
        Name = name;
    }
}