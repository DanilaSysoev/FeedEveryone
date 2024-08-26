using System;
using FeedEveryone.Service.Randomizing;

namespace FeedEveryone.Service.WorldGeneration;

public class SquareHeightMapDiamondSquareGenerator : IHeightMapGenerator
{
public SquareHeightMapDiamondSquareGenerator(
        IRandomProvider random,
        StandartSize size,
        float roughness = DEFAULT_ROUGHNESS
    )
    {
        this.random = random;
        this.size = size;
        this.roughness = roughness;
    }

    public HeightMap Generate()
    {
        empty = -size * 2 * roughness;
        var square = new HeightMap(size, size, empty);
        SetupCorners(square);
        int side = size - 1;
        while(side > 1)
        {
            DiamondSquareIteration(square, side);
            side /= 2;
        }
        return square;
    }

    private void SetupCorners(HeightMap square)
    {
        square[0, 0] = random.GetFloat(-size * roughness / 2,
                                        size * roughness / 2);
        square[0, square.Width - 1] =
            random.GetFloat(-size * roughness / 2,
                             size * roughness / 2);
        square[square.Height - 1, 0] =
            random.GetFloat(-size * roughness / 2,
                             size * roughness / 2);
        square[square.Height - 1, square.Width - 1] =
            random.GetFloat(-size * roughness / 2,
                             size * roughness / 2);
    }
    private void DiamondSquareIteration(HeightMap square, int side)
    {
        SquareSteps(square, side);
        DiamondSteps(square, side);
    }

    private void DiamondSteps(HeightMap square, int side)
    {
        int step = side / 2;
        for(int line = 0, line_number = 0;
            line < square.Height;
            line += step, line_number++)
        {
            for(int col = (1 - line_number % 2) * step;
                col < square.Width;
                col += side)
            {
                square[line, col] =
                    CalculateDiamondPoint(line, col, square, step);
            }
        }
    }

    private float CalculateDiamondPoint(
        int line, int col, HeightMap square, int step
    )
    {
        if(square[line, col] != empty)
            return square[line, col];

        float sum = CountNeighborsSum(line, col, square, step);
        int count = CountNeighborsCount(line, col, square);

        return sum / count +
               random.GetFloat(-step * roughness, step * roughness);
    }

    private float CountNeighborsSum(
        int line, int col, HeightMap square, int step
    )
    {
        float sum = 0;

        if(line > 0)
            sum += square[line - step, col];
        if(line < square.Height - 1)
            sum += square[line + step, col];
        if(col > 0)
            sum += square[line, col - step];
        if(col < square.Width - 1)
            sum += square[line, col + step];

        return sum;
    }

    private int CountNeighborsCount(int line, int col, HeightMap square)
    {
        int cnt = 0;

        if(line > 0)
            cnt++;
        if(line < square.Height - 1)
            cnt++;
        if(col > 0)
            cnt++;
        if(col < square.Width - 1)
            cnt++;

        return cnt;
    }

    private void SquareSteps(HeightMap square, int side)
    {
        int step = side / 2;
        for(int line = step; line < square.Height; line += side)
            for(int col = step; col < square.Width; col += side)
                square[line, col] =
                    CalculateSquarePoint(line, col, square, step);
    }

    private float CalculateSquarePoint(
        int line, int col, HeightMap square, int step
    )
    {
        if(square[line, col] != empty)
            return square[line, col];

        float sum = square[line - step, col - step]
                  + square[line - step, col + step]
                  + square[line + step, col - step]
                  + square[line + step, col + step];
        return sum / 4 + random.GetFloat(-step * roughness, step * roughness);
    }

    private readonly IRandomProvider random;
    private readonly int size;
    private readonly float roughness;
    private const float DEFAULT_ROUGHNESS = 1.0f;

    private float empty;
}
