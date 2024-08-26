namespace FeedEveryone.Service.WorldGeneration;

public interface IHeightMapGenerator
{
    void Setup(HeightMap preset);
    HeightMap Generate();

    int MapHeight { get; }
    int MapWidth { get;}
}
