namespace FeedEveryone.Service.Randomizing;

public class SystemRandomProvider : IRandomProvider
{
    public SystemRandomProvider()
    {
        random = new Random();
    }
    public SystemRandomProvider(int seed)
    {
        random = new Random(seed);
    }

    public float GetFloat()
    {
        return random.NextSingle();
    }

    public float GetFloat(float min, float max)
    {
        return random.NextSingle() * (max - min) + min;
    }

    private readonly Random random;
}