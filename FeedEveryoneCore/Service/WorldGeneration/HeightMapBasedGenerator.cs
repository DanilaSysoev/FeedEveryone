using FeedEveryone.Exceptions;
using FeedEveryone.World;

namespace FeedEveryone.Service.WorldGeneration;

public class HeightMapBasedGenerator : IWorldMapGenerator
{
    public HeightMapBasedGenerator(
        IHeightMapGenerator heightsGenerator,
        IHeightMapGenerator temperatureGenerator,
        IHeightMapGenerator underWaterLevelGenerator
    )
    {
        this.heightsGenerator = heightsGenerator;
        this.temperatureGenerator = temperatureGenerator;
        this.underWaterLevelGenerator = underWaterLevelGenerator;

        ValidateGenerators();
    }

    private void ValidateGenerators()
    {
        if(heightsGenerator.MapHeight == temperatureGenerator.MapHeight &&
           temperatureGenerator.MapHeight == underWaterLevelGenerator.MapHeight &&
           heightsGenerator.MapWidth == temperatureGenerator.MapWidth &&
           temperatureGenerator.MapWidth == underWaterLevelGenerator.MapWidth)
           return;

        throw new WorldMapGeneratorValidationException(
            "Invalid height map generator sizes: " +
            $"{heightsGenerator.MapWidth}x{heightsGenerator.MapHeight}, " +
            $"{temperatureGenerator.MapWidth}x{temperatureGenerator.MapHeight}, " +
            $"{underWaterLevelGenerator.MapWidth}x{underWaterLevelGenerator.MapHeight}"
        );
    }

    public WorldMap Generate()
    {
        throw new NotImplementedException();
    }

    private readonly IHeightMapGenerator heightsGenerator;
    private readonly IHeightMapGenerator temperatureGenerator;
    private readonly IHeightMapGenerator underWaterLevelGenerator;
}
