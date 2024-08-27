using System;
using FeedEveryone.Exceptions;

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

        CompressSelection(
            MapHeight,
            BaseGenerator.MapHeight,
            SelectLine
        );

        return result;
    }

    private void SelectLine(int line, int baseLine)
    {
        CompressSelection(
            MapWidth,
            BaseGenerator.MapWidth,
            (column, baseColumn) => {
                result[line, column] = baseMap[baseLine, baseColumn];
            }
        );
    }


    private void CompressSelection(
        int size,
        int baseSize,
        Action<int, int> selection
    )
    {
        int rest = (baseSize - 1) % (size - 1);
        int accum = baseSize;
        for (int index = 0, baseIndex = 0; index < size; baseIndex++)
        {
            if(accum >= baseSize)
            {
                selection(index, baseIndex);

                index++;
                if (rest > 0)
                {
                    baseIndex++;
                    rest--;
                }
                accum -= baseSize;
            }
            accum += size;
        }
    }

    private readonly int mapHeight;
    private readonly int mapWidth;

    private HeightMap baseMap = null!;
    private HeightMap result = null!;
}
