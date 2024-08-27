using FeedEveryone.Exceptions;

namespace FeedEveryone.Service.WorldGeneration;

public class CuttingHeightMapGenerator : HeightMapDecorator
{
    public override int MapHeight => mapHeight;
    public override int MapWidth => mapWidth;
    public int StartLine { get; private set; }
    public int StartColumn { get; private set; }

    public CuttingHeightMapGenerator(
        IHeightMapGenerator baseGenerator,
        int height,
        int width,
        int startLine = 0,
        int startColumn = 0
    ) : base(baseGenerator)
    {
        mapHeight = height;
        mapWidth = width;
        StartLine = startLine;
        StartColumn = startColumn;
        
        ValidateGenerator();
    }

    private void ValidateGenerator()
    {
        if(StartLine + MapHeight <= BaseGenerator.MapHeight &&
           StartColumn + MapWidth <= BaseGenerator.MapWidth)
           return;
        
        throw new HeightMapGeneratorValidationException(
            $"CuttingGenerator error. Invalid cutting segment data"
        );
    }

    protected override HeightMap Decorate(HeightMap heightMap)
    {
        var result = new HeightMap(MapHeight, MapWidth);
        for (var line = 0; line < MapHeight; line++)
            for (var column = 0; column < MapWidth; column++)
                result[line, column] =
                    heightMap[StartLine + line, StartColumn + column];

        return result;
    }

    private readonly int mapHeight;
    private readonly int mapWidth;
}
