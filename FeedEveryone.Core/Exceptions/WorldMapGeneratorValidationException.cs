namespace FeedEveryone.Core.Exceptions;

public class WorldMapGeneratorValidationException : Exception
{
    public WorldMapGeneratorValidationException(string message)
        : base(message)
    {
    }
}
