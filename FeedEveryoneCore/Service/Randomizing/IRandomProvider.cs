namespace FeedEveryone.Service.Randomizing;

public interface IRandomProvider
{
    /// <summary>
    /// Returns a random float between 0 and 1.
    /// </summary>
    float GetFloat();
}
