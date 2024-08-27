using FeedEveryone.Exceptions;
using FeedEveryone.Service.Algorithms;

namespace FeedEveryone.Service.WorldGeneration;

public class CompressHeightMapGenerator : HeightMapDecorator
{
    public override int MapHeight => mapHeight;
    public override int MapWidth => mapWidth;

    public CompressHeightMapGenerator(
        IHeightMapGenerator baseGenerator,
        int height,
        int width
    )
        : base(baseGenerator)
    {
        mapHeight = height;
        mapWidth = width;
        
        ValidateGenerator();
    }

    private void ValidateGenerator()
    {
        if(MapHeight > BaseGenerator.MapHeight ||
           MapWidth > BaseGenerator.MapWidth)
        {
            throw new HeightMapGeneratorValidationException(
                $"CompressGenerator error. Invalid compress sizes"
            );
        }
    }

    protected override HeightMap Decorate(HeightMap heightMap)
    {
        baseMap = heightMap;
        result = new HeightMap(MapHeight, MapWidth);

        new ThinningSelection(
            MapHeight,
            BaseGenerator.MapHeight,
            SelectLine
        ).Select();

        return result;
    }

    private void SelectLine(int line, int baseLine)
    {
        new ThinningSelection(
            MapWidth,
            BaseGenerator.MapWidth,
            (column, baseColumn) => {
                result[line, column] = baseMap[baseLine, baseColumn];
            }
        ).Select();
    }

    private readonly int mapHeight;
    private readonly int mapWidth;

    private HeightMap baseMap = null!;
    private HeightMap result = null!;
}
