namespace FeedEveryoneCore.Exceptions;

public class InvalidFrameSizeException : Exception
{
    public InvalidFrameSizeException(int mapSize, int frameSize)
        : base($"map size {mapSize} isn't multiple of frame size {frameSize}")
    {
    }
}
