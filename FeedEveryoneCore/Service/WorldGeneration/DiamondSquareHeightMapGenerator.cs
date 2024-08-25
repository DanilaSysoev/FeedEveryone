using FeedEveryone.Service.Randomizing;
using FeedEveryoneCore.Service.WorldGeneration;

namespace FeedEveryone.Service.WorldGeneration;

public class DiamondSquareHeightMapGenerator : IHeightMapGenerator
{
    public DiamondSquareHeightMapGenerator(
        IRandomProvider random,
        StandartSize squareSize,
        float roughness = DEFAULT_ROUGHNESS
    )
    {
        this.random = random;
        this.squareSize = squareSize;
        this.roughness = roughness;
    }

    public HeightMap Generate(StandartSize height, StandartSize width)
    {
        CalculateSquares(height, width);

        HeightMap result = new HeightMap(height, width);
        for(int line = 0; line < squares.GetLength(0); line++)
            for(int column = 0; column < squares.GetLength(1); column++)
                result.SetSubmap(squares[line, column],
                                 CalculateSquarePosition(line),
                                 CalculateSquarePosition(column));
        return result;
    }

    private int CalculateSquarePosition(int squareIndex)
    {
        return (squareSize - 1) * squareIndex;
    }

    private void CalculateSquares(StandartSize height, StandartSize width)
    {
        squares = new HeightMap[
            CalculateSquaresSize(height),
            CalculateSquaresSize(width)
        ];
        for(int line = 0; line < squares.GetLength(0); line++)
            for(int column = 0; column < squares.GetLength(1); column++)
                squares[line, column] = GenerateSqaure(line, column);
    }

    private int CalculateSquaresSize(StandartSize size)
    {
        int result = (size - 1) / (squareSize - 1);

        if((size - 1) % (squareSize - 1) != 0)
            result++;

        return result;
    }

    private HeightMap GenerateSqaure(int line, int column)
    {
        empty = -squareSize * 2 * roughness;
        var square = new HeightMap(squareSize, squareSize, empty);
        SetupEdges(square, line, column);
        SetupRemainingCorners(square);
        int side = squareSize - 1;
        while(side > 1)
        {
            DiamondSquareIteration(square, side);
            side /= 2;
        }
        return square;
    }

    private void SetupRemainingCorners(HeightMap square)
    {
        if(square[0, 0] == empty)
            square[0, 0] = random.GetFloat(-squareSize * roughness / 2,
                                            squareSize * roughness / 2);
        if(square[0, square.Width - 1] == empty)
            square[0, square.Width - 1] =
                random.GetFloat(-squareSize * roughness / 2,
                                 squareSize * roughness / 2);
        if(square[square.Height - 1, 0] == empty)
            square[square.Height - 1, 0] =
                random.GetFloat(-squareSize * roughness / 2,
                                 squareSize * roughness / 2);
        if(square[square.Height - 1, square.Width - 1] == empty)
            square[square.Height - 1, square.Width - 1] =
                random.GetFloat(-squareSize * roughness / 2,
                                 squareSize * roughness / 2);
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

    private void SetupEdges(HeightMap square, int line, int column)
    {
        if(line > 0 && squares[line - 1, column] is not null)
            square.TopEdge = squares[line - 1, column].BottomEdge;
        if(column > 0 && squares[line, column - 1] is not null)
            square.LeftEdge = squares[line, column - 1].RightEdge;
        if(line < squares.GetLength(0) - 1 && squares[line + 1, column] is not null)
            square.BottomEdge = squares[line + 1, column].TopEdge;
        if(column < squares.GetLength(1) - 1 && squares[line, column + 1] is not null)
            square.RightEdge = squares[line, column + 1].LeftEdge;
    }

    private readonly IRandomProvider random;
    private readonly int squareSize;
    private HeightMap[,] squares = null!;
    private float empty;
    private readonly float roughness;
    private const float DEFAULT_ROUGHNESS = 1.0f;
}