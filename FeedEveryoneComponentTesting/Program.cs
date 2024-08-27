using FeedEveryone.Service.Randomizing;
using FeedEveryone.Service.WorldGeneration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;


for(int i = 0; i < 5; ++i)
{
    var first = new float[1025];
    var last = new float[1025];
    Array.Fill(first, 1025);
    Array.Fill(last, -1025);
    HeightMap pattern = new HeightMap(1025, 1025);
    pattern.TopEdge = first;
    pattern.BottomEdge = last;

    FrameBasedDiamondSquareHeightMapGenerator generator =
    new FrameBasedDiamondSquareHeightMapGenerator(
        new SquareHeightMapDiamondSquareGenerator(new SystemRandomProvider(), 1024, 0.5f),
        1024,
        1024
    );
    generator.Setup(pattern);

    var heightMap = generator.Generate();
    heightMap.Rescale(0.001f, 0.999f);

    using Image<Rgba32> image = new(heightMap.Width, heightMap.Height);
    for (int x = 0; x < heightMap.Width; x++)
    {
        for (int y = 0; y < heightMap.Height; y++)
        {
            image[x, y] = new Rgba32((byte)(256 * heightMap[y, x]),
                                    (byte)(256 * heightMap[y, x]),
                                    (byte)(256 * heightMap[y, x]),
                                    255);
        }
    }
    image.SaveAsPng($"output_temp{i:0#}.png");
}