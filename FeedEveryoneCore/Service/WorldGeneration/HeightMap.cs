

namespace FeedEveryoneCore.Service.WorldGeneration;

public class HeightMap
{
    public HeightMap(int height, int width, float value = DEFAULT_INIT_VALUE)
    {
        data = new float[height, width];
        Init(value);
    }

    public int Height => data.GetLength(0);
    public int Width => data.GetLength(1);
    public float[] TopEdge {
        get => GetLine(0);
        set => SetLine(0, value);
    }
    public float[] BottomEdge {
        get => GetLine(Height - 1);
        set => SetLine(Height - 1, value);
    }
    public float[] LeftEdge {
        get => GetColumn(0);
        set => SetColumn(0, value);
    }
    public float[] RightEdge {
        get => GetColumn(Width - 1);
        set => SetColumn(Width - 1, value);
    }

    public float this[int line, int column]
    {
        get => data[line, column];
        set => data[line, column] = value;
    }

    public void Init(float value)
    {
        for (int line = 0; line < Height; line++)
            for (int column = 0; column < Width; column++)
                data[line, column] = value;
    }



    private void SetLine(int line, float[] value)
    {
        for (int column = 0; column < Width; column++)
            data[line, column] = value[column];
    }
    private float[] GetLine(int line)
    {
        float[] result = new float[Width];
        for (int column = 0; column < Width; column++)
            result[column] = data[line, column];
        return result;
    }
    private void SetColumn(int column, float[] value)
    {
        for (int line = 0; line < Height; line++)
            data[line, column] = value[line];
    }
    private float[] GetColumn(int column)
    {
        float[] result = new float[Height];
        for (int line = 0; line < Height; line++)
            result[line] = data[line, column];
        return result;
    }

    public void SetSubmap(
        HeightMap submap, int startLine, int startColumn
    )
    {
        int endLine = Math.Min(startLine + submap.Height, Height);
        int endColumn = Math.Min(startColumn + submap.Width, Width);

        for(int line = startLine; line < endLine; line++)
            for (int column = startColumn; column < endColumn; column++)
                data[line, column] =
                    submap.data[line - startLine, column - startColumn];
    }
    public void Rescale(float newMin, float newMax)
    {
        float curMin = float.PositiveInfinity;
        float curMax = float.NegativeInfinity;
        for (int line = 0; line < Height; line++)
        {
            for (int column = 0; column < Width; column++)
            {
                curMin = Math.Min(curMin, data[line, column]);
                curMax = Math.Max(curMax, data[line, column]);
            }
        }

        for (int line = 0; line < Height; line++)
        {
            for (int column = 0; column < Width; column++)
            {
                data[line, column] =
                    (data[line, column] - curMin) / (curMax - curMin) *
                    (newMax - newMin) + newMin;
            }
        }
    }

    private readonly float[,] data;
    public const float DEFAULT_INIT_VALUE = 0;
}
