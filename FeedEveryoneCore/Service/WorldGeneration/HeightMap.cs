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

    private readonly float[,] data;
    public const float DEFAULT_INIT_VALUE = 0;
}
