namespace FeedEveryone.Service.WorldGeneration.HeightMapGeneration;

public abstract class HeightMapDecorator : IHeightMapGenerator
{
    public abstract int MapHeight { get; }
    public abstract int MapWidth { get; }

    protected HeightMapDecorator(
        IHeightMapGenerator baseGenerator
    )
    {
        this.baseGenerator = baseGenerator;
    }

    public HeightMap Generate()
    {
        var heightMap = baseGenerator.Generate();
        return Decorate(heightMap);
    }

    public void Setup(HeightMap preset)
    {
        baseGenerator.Setup(preset);
    }


    protected IHeightMapGenerator BaseGenerator => baseGenerator;

    protected abstract HeightMap Decorate(HeightMap heightMap);

    private readonly IHeightMapGenerator baseGenerator;
}
