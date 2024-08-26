namespace FeedEveryone.Service.WorldGeneration;

public abstract class HeightMapGeneratorBase : IHeightMapGenerator
{
    protected HeightMapGeneratorBase(
        int mapHeight,
        int mapWidth
    )
    {
        MapHeight = mapHeight;
        MapWidth = mapWidth;
    }

    public int MapHeight { get; private set; }
    public int MapWidth { get; private set; }

    public HeightMap Generate()
    {
        HeightMap? heightMap;
        if(preset is not null)
            heightMap = new HeightMap(preset);
        else
            heightMap = CreateEmptyMap();
        GenerateHeights(heightMap);
        return heightMap;
    }
    protected virtual HeightMap CreateEmptyMap()
    {
        return new HeightMap(MapWidth, MapHeight);
    }

    protected abstract void GenerateHeights(HeightMap heightMap);

    public void Setup(HeightMap preset)
    {
        if(preset.Height == MapHeight && preset.Width == MapWidth)
            this.preset = preset;
    }

    protected HeightMap? preset;
}
