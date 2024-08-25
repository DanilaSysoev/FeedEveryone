using FeedEveryoneCore.Service.WorldGeneration;

namespace FeedEveryone.Service.WorldGeneration;

public interface IHeightMapGenerator
{
    HeightMap Generate(StandartSize height, StandartSize width);
}
