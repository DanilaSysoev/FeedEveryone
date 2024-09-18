using FeedEveryone.Core.Exceptions;
using FeedEveryone.Core.Service.WorldGeneration.HeightMapGeneration;
using FeedEveryone.Core.World;

namespace FeedEveryone.Core.Service.WorldGeneration.WorldMapGeneration;

public class HeightMapBasedGenerator : IWorldMapGenerator
{
    public int MapHeight => heightsGenerator.MapHeight;
    public int MapWidth => heightsGenerator.MapWidth;

    public HeightMapBasedGenerator(
        INameGenerator nameGenerator,
        IHeightMapGenerator heightsGenerator,
        IHeightMapGenerator temperatureGenerator,
        IHeightMapGenerator groundWaterLevelGenerator,
        IHeightMapGenerator precipitationGenerator
    )
    {
        this.nameGenerator = nameGenerator;
        this.heightsGenerator = heightsGenerator;
        this.temperatureGenerator = temperatureGenerator;
        this.groundWaterLevelGenerator = groundWaterLevelGenerator;
        this.precipitationGenerator = precipitationGenerator;

        ValidateGenerators();
    }

    private void ValidateGenerators()
    {
        if(heightsGenerator.MapHeight == temperatureGenerator.MapHeight &&
           temperatureGenerator.MapHeight == groundWaterLevelGenerator.MapHeight &&
           groundWaterLevelGenerator.MapHeight == precipitationGenerator.MapHeight &&
           heightsGenerator.MapWidth == temperatureGenerator.MapWidth &&
           temperatureGenerator.MapWidth == groundWaterLevelGenerator.MapWidth &&
           groundWaterLevelGenerator.MapWidth == precipitationGenerator.MapWidth)
           return;

        throw new WorldMapGeneratorValidationException(
            "Invalid height map generator sizes: " +
            $"{heightsGenerator.MapWidth}x{heightsGenerator.MapHeight}, " +
            $"{temperatureGenerator.MapWidth}x{temperatureGenerator.MapHeight}, " +
            $"{groundWaterLevelGenerator.MapWidth}x{groundWaterLevelGenerator.MapHeight}, " +
            $"{precipitationGenerator.MapWidth}x{precipitationGenerator.MapHeight}"
        );
    }

    public WorldMap Generate()
    {
        map = new WorldMap(
            nameGenerator.Generate(), MapHeight, MapWidth
        );

        var heightMap = heightsGenerator.Generate();
        var temperatureMap = temperatureGenerator.Generate();
        var groundWaterLevelMap = groundWaterLevelGenerator.Generate();
        var precipitationMap = precipitationGenerator.Generate();

        heightMap.Rescale(MIN_HEIGHT, MAX_HEIGHT);
        temperatureMap.Rescale(MIN_TEMPERATURE, MAX_TEMPERATURE);
        groundWaterLevelMap.Rescale(MIN_GROUND_WATER_LEVEL,
                                    MAX_GROUND_WATER_LEVEL);
        precipitationMap.Rescale(MIN_PRECIPITATION, MAX_PRECIPITATION);

        CreateTiles(
            heightMap, temperatureMap, groundWaterLevelMap, precipitationMap
        );

        return map;
    }

    private void CreateTiles(
        HeightMap heightMap,
        HeightMap temperatureMap,
        HeightMap underWaterLevelMap,
        HeightMap precipitationMap
    )
    {
        for (int line = 0; line < map.Height; line++)
        {
            for (int column = 0; column < map.Width; column++)
            {
                map[line, column] =
                    new Tile(
                        line,
                        column,
                        new Biome(
                            heightMap[line, column],
                            temperatureMap[line, column],
                            underWaterLevelMap[line, column],
                            precipitationMap[line, column]
                        )
                    );
            }
        }
    }

    private WorldMap map = null!;

    private readonly INameGenerator nameGenerator;
    private readonly IHeightMapGenerator heightsGenerator;
    private readonly IHeightMapGenerator temperatureGenerator;
    private readonly IHeightMapGenerator groundWaterLevelGenerator;
    private readonly IHeightMapGenerator precipitationGenerator;

    private const float MIN_HEIGHT = 0.001f;
    private const float MAX_HEIGHT = 0.999f;
    private const float MIN_TEMPERATURE = 0.001f;
    private const float MAX_TEMPERATURE = 0.999f;
    private const float MIN_GROUND_WATER_LEVEL = 0.001f;
    private const float MAX_GROUND_WATER_LEVEL = 0.999f;
    private const float MIN_PRECIPITATION = 0.001f;
    private const float MAX_PRECIPITATION = 0.999f;
}
