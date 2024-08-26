

using FeedEveryoneCore.Exceptions;

namespace FeedEveryone.Service.WorldGeneration;

public class FrameBasedDiamondSquareHeightMapGenerator
    : HeightMapGeneratorBase
{
    public FrameBasedDiamondSquareHeightMapGenerator(
        IHeightMapGenerator frameGenerator,
        StandartSize height,
        StandartSize width
    ) : base(height, width)
    {
        this.frameGenerator = frameGenerator;
        SizesValidation();
    }

    protected override void GenerateHeights(HeightMap heightMap)
    {
        CalculateFrames(heightMap);

        for(int line = 0; line < squares.GetLength(0); line++)
            for(int column = 0; column < squares.GetLength(1); column++)
                heightMap.SetSubmap(
                    squares[line, column],
                    CalculateSquarePosition(line, frameGenerator.MapHeight),
                    CalculateSquarePosition(column, frameGenerator.MapWidth)
                );
    }

    private void SizesValidation()
    {
        if((MapHeight - 1) % (frameGenerator.MapHeight - 1) != 0)
           throw new InvalidFrameSizeException(MapHeight - 1,
                                               frameGenerator.MapHeight - 1);
        if((MapWidth - 1) % (frameGenerator.MapWidth - 1) != 0)
            throw new InvalidFrameSizeException(MapWidth - 1,
                                                frameGenerator.MapWidth - 1);
    }

    private int CalculateSquarePosition(
        int squareIndex, StandartSize frameSize
    )
    {
        return (frameSize - 1) * squareIndex;
    }

    private void CalculateFrames(HeightMap heightMap)
    {
        squares = new HeightMap[
            CalculateFramesSize(MapHeight, frameGenerator.MapHeight),
            CalculateFramesSize(MapWidth, frameGenerator.MapWidth)
        ];
        for(int line = 0; line < squares.GetLength(0); line++)
            for(int column = 0; column < squares.GetLength(1); column++)
                squares[line, column] =
                    GenerateFrame(heightMap, new Position(line, column));
    }

    private int CalculateFramesSize(
        StandartSize mapSize, StandartSize frameSize
    )
    {
        int result = (mapSize - 1) / (frameSize - 1);

        if((mapSize - 1) % (frameSize - 1) != 0)
            result++;

        return result;
    }

    private HeightMap GenerateFrame(HeightMap heightMap, Position position)
    {
        var frame = new HeightMap(frameGenerator.MapHeight,
                                  frameGenerator.MapWidth);
        SetupEdges(heightMap, frame, position);
        frameGenerator.Setup(frame);
        return frameGenerator.Generate();
    }

    private void SetupEdges(
        HeightMap heightMap, HeightMap frame, Position position
    )
    {
        if(TopEdgeExists(heightMap, position))
            frame.TopEdge = CalculateTopEdge(heightMap, position);
        if(LeftEdgeExists(heightMap, position))
            frame.LeftEdge = CalculateLeftEdge(heightMap, position);
        if(BottomEdgeExists(heightMap, position))
            frame.BottomEdge = CalculateBottomEdge(heightMap, position);
        if(RightEdgeExists(heightMap, position))
            frame.RightEdge = CalculateRightEdge(heightMap, position);
    }

    private float[] CalculateTopEdge(HeightMap heightMap, Position position)
    {
        if(position.Line == 0)
            return TakeTopFromHeightMap(heightMap, position);
        return squares[position.Line - 1, position.Column].BottomEdge;
    }

    private float[] CalculateBottomEdge(HeightMap heightMap, Position position)
    {
        if(position.Line == squares.GetLength(0) - 1)
            return TakeBottomFromHeightMap(heightMap, position);
        return squares[position.Line + 1, position.Column].TopEdge;
    }

    private float[] CalculateLeftEdge(HeightMap heightMap, Position position)
    {
        if(position.Column == 0)
            return TakeLeftFromHeightMap(heightMap, position);
        return squares[position.Line, position.Column - 1].RightEdge;
    }

    private float[] CalculateRightEdge(HeightMap heightMap, Position position)
    {
        if(position.Column == squares.GetLength(1) - 1)
            return TakeRightFromHeightMap(heightMap, position);
        return squares[position.Line, position.Column + 1].LeftEdge;
    }

    private float[] TakeTopFromHeightMap(
        HeightMap heightMap, Position position
    )
    {
        return TakeSegmentFromLine(heightMap, position, 0);
    }

    private float[] TakeBottomFromHeightMap(
        HeightMap heightMap, Position position
    )
    {
        return TakeSegmentFromLine(
            heightMap, position, heightMap.Height - 1
        );
    }

    private float[] TakeLeftFromHeightMap(
        HeightMap heightMap, Position position
    )
    {
        return TakeSegmentFromColumn(heightMap, position, 0);
    }

    private float[] TakeRightFromHeightMap(
        HeightMap heightMap, Position position
    )
    {
        return TakeSegmentFromColumn(heightMap, position, heightMap.Width - 1);
    }

    private float[] TakeSegmentFromLine(
        HeightMap heightMap, Position position, int line
    )
    {
        var result = new float[frameGenerator.MapWidth];
        var start = CalculateSquarePosition(
            position.Column, frameGenerator.MapWidth
        );
        
        for(int i = 0; i < frameGenerator.MapWidth; i++)
            if(start + i < heightMap.Width)
                result[i] = heightMap[line, start + i];
        
        return result;
    }

    private float[] TakeSegmentFromColumn(
        HeightMap heightMap, Position position, int column
    )
    {
        var result = new float[frameGenerator.MapHeight];
        var start = CalculateSquarePosition(
            position.Column, frameGenerator.MapHeight
        );
        
        for(int i = 0; i < frameGenerator.MapHeight; i++)
            if(start + i < heightMap.Height)
                result[i] = heightMap[start + i, column];
        
        return result;
    }


    private bool RightEdgeExists(HeightMap heightMap, Position position)
    {
        return (position.Column == squares.GetLength(1) - 1 &&
                ExistColumnInMap(heightMap, heightMap.Width - 1)) ||
               position.Column < squares.GetLength(1) - 1 &&
               squares[position.Line, position.Column + 1] is not null;
    }

    private bool BottomEdgeExists(HeightMap heightMap, Position position)
    {
        return (position.Line == squares.GetLength(0) - 1 &&
                ExistLineInMap(heightMap, heightMap.Height - 1)) ||
               position.Line < squares.GetLength(0) - 1 &&
               squares[position.Line + 1, position.Column] is not null;
    }

    private bool LeftEdgeExists(HeightMap heightMap, Position position)
    {
        return (position.Column == 0 && ExistColumnInMap(heightMap, 0)) ||
               position.Column > 0 &&
               squares[position.Line, position.Column - 1] is not null;
    }

    private bool TopEdgeExists(HeightMap heightMap, Position position)
    {
        return (position.Line == 0 && ExistLineInMap(heightMap, 0)) ||
               position.Line > 0 &&
               squares[position.Line - 1, position.Column] is not null;
    }

    private bool ExistLineInMap(HeightMap heightMap, int line)
    {
        return heightMap[line, 0] != HeightMap.EMPTY_VALUE;
    }

    private bool ExistColumnInMap(HeightMap heightMap, int column)
    {
        return heightMap[0, column] != HeightMap.EMPTY_VALUE;
    }

    private readonly IHeightMapGenerator frameGenerator;
    private HeightMap[,] squares = null!;
}