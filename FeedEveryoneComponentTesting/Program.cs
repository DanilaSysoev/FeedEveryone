using FeedEveryone.Service.Randomizing;
using FeedEveryone.Service.WorldGeneration.HeightMapGeneration;
using FeedEveryone.World;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace FeedEveryoneComponentTesting;

static internal class Program
{
    const float Cold = 0.2f;
    const float Hot = 0.8f;
    const float Dry = 0.25f;
    const float Wet = 0.75f;
    const float Ocean = 0.15f;

    private static void Main(string[] args)
    {

        for (int i = 0; i < 5; ++i)
        {
            var first = new float[1025];
            var last = new float[1025];
            Array.Fill(first, 1025);
            Array.Fill(last, -1025);
            HeightMap pattern = new HeightMap(1025, 1025);
            pattern.TopEdge = first;
            pattern.BottomEdge = last;

            FrameBasedDiamondSquareHeightMapGenerator temperature =
            new FrameBasedDiamondSquareHeightMapGenerator(
                new SquareHeightMapDiamondSquareGenerator(new SystemRandomProvider(), 1024, 0.5f),
                1024,
                1024
            );
            temperature.Setup(pattern);
            FrameBasedDiamondSquareHeightMapGenerator height =
            new FrameBasedDiamondSquareHeightMapGenerator(
                new SquareHeightMapDiamondSquareGenerator(new SystemRandomProvider(), 256, 0.5f),
                1024,
                1024
            );
            FrameBasedDiamondSquareHeightMapGenerator groundWater =
            new FrameBasedDiamondSquareHeightMapGenerator(
                new SquareHeightMapDiamondSquareGenerator(new SystemRandomProvider(), 1024, 0.5f),
                1024,
                1024
            );
            FrameBasedDiamondSquareHeightMapGenerator precipitation =
            new FrameBasedDiamondSquareHeightMapGenerator(
                new SquareHeightMapDiamondSquareGenerator(new SystemRandomProvider(), 1024, 0.5f),
                1024,
                1024
            );
            var worldgen = new HeightMapBasedGenerator(
                new DummyNameGenerator(),
                height,
                temperature,
                groundWater,
                precipitation
            );
            var world = worldgen.Generate();
            using Image<Rgba32> image = GenerateImage(i, world);
        }
    }

    private static Image<Rgba32> GenerateImage(int i, WorldMap world)
    {
        Image<Rgba32> image = new(world.Width, world.Height);
        for (int x = 0; x < world.Width; x++)
        {
            for (int y = 0; y < world.Height; y++)
            {
                if (IsOcean(world, x, y))
                    image[x, y] = Color.DarkBlue;
                else if (IsIceland(world, x, y))
                    image[x, y] = Color.LightBlue;
                else if (IsTropics(world, x, y))
                    image[x, y] = Color.DarkGreen;
                else if (IsDesert(world, x, y))
                    image[x, y] = Color.Yellow;
                else if (IsSwamp(world, x, y))
                    image[x, y] = Color.Brown;
                else if (IsForest(world, x, y))
                    image[x, y] = Color.Green;
                else
                    image[x, y] = Color.LightGreen;

            }
        }
        image.SaveAsPng($"output_world{i:0#}.png");
        return image;
    }

    static bool IsOcean(WorldMap world, int x, int y)
    {
        return world[y, x].Biome.Height <= Ocean;
    }

    static bool IsIceland(WorldMap world, int x, int y)
    {
        return world[y, x].Biome.Temperature <= Cold;
    }

    static bool IsTropics(WorldMap world, int x, int y)
    {
        return world[y, x].Biome.Temperature > Hot &&
                (world[y, x].Biome.GroundWaterLevel > Wet ||
                 world[y, x].Biome.Precipitation > Wet) &&
                 world[y, x].Biome.GroundWaterLevel > Dry &&
                 world[y, x].Biome.Precipitation > Dry;
    }

    static bool IsDesert(WorldMap world, int x, int y)
    {
        return world[y, x].Biome.Temperature > Hot &&
                (world[y, x].Biome.GroundWaterLevel <= Dry ||
                 world[y, x].Biome.Precipitation <= Dry) &&
                 world[y, x].Biome.GroundWaterLevel <= Wet &&
                 world[y, x].Biome.Precipitation <= Wet;
    }

    static bool IsForest(WorldMap world, int x, int y)
    {
        return world[y, x].Biome.Temperature <= Hot &&
                (world[y, x].Biome.GroundWaterLevel > Wet ||
                world[y, x].Biome.Precipitation > Wet);
    }
    static bool IsSwamp(WorldMap world, int x, int y)
    {
        return world[y, x].Biome.Temperature <= Hot &&
                world[y, x].Biome.GroundWaterLevel > Wet &&
                world[y, x].Biome.Precipitation > Wet;
    }
}