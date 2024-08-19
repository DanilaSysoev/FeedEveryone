namespace FeedEveryone.World;

public class Biome
{
    public string Name { get; private set; }

    public override string ToString()
    {
        return Name;
    }


    internal Biome(string name)
    {
        Name = name;
    }
}