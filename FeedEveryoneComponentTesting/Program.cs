using FeedEveryone.Service.Randomizing;
using FeedEveryone.Service.WorldGeneration;
using FeedEveryoneCore.Service.WorldGeneration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;


for(int i = 0; i < 10; ++i)
{
    DiamondSquareHeightMapGenerator generator =
        new DiamondSquareHeightMapGenerator(
            new SystemRandomProvider(),
            new StandartSize(128)
        );

    var heightMap = generator.Generate(1024, 1024);
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
    image.SaveAsPng($"output{i:0#}.png");
}