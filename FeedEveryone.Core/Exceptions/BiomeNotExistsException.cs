namespace FeedEveryone.Core.Exceptions;

public class BiomeNotExistsException : Exception
{
    public BiomeNotExistsException(string biomeName)
        : base($"Biome {biomeName} does not exist")
    {
    }
}
