namespace FeedEveryone.Exceptions;

public class WorldMapGeneratorValidationException : Exception
{
    public WorldMapGeneratorValidationException(string message)
        : base(message)
    {
    }
}
