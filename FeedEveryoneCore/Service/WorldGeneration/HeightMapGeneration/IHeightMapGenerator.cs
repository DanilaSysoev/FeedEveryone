namespace FeedEveryone.Service.WorldGeneration.HeightMapGeneration;

public interface IHeightMapGenerator
{
    void Setup(HeightMap preset);
    HeightMap Generate();

    int MapHeight { get; }
    int MapWidth { get;}
}
