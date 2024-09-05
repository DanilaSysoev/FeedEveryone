using FeedEveryone.Core.World;

namespace FeedEveryone.Core.Service.WorldGeneration.HeightMapGeneration;

public interface IWorldMapGenerator
{
    public WorldMap Generate();
}
