using System;
using FeedEveryone.Core.Service.Randomizing;
using FeedEveryone.Core.Service.WorldGeneration;
using FeedEveryone.Core.Service.WorldGeneration.HeightMapGeneration;
using FeedEveryone.Core.Service.WorldGeneration.WorldMapGeneration;
using FeedEveryone.Mono.Components;

namespace FeedEveryone.Mono.Builders;

public class SimpleSquareBuilder : IBuilder<IWorldMapGenerator>
{
    public int MapSize
    {
        get { return mapSize; }
        set { mapSize = value; }
    }
    public int MapSquareSize
    {
        get { return mapSquareSize; }
        set { mapSquareSize = value; }
    }

    public float HeightRoughness { get; set; }
        = DefaultHeightRoughness;

    public float TemperatureRoughness { get; set; }
        = DefaultTemperatureRoughness;

    public float PrecipitationRoughness { get; set; }
        = DefaultPrecipitationRoughness;

    public float GroundWaterRoughness { get; set; }
        = DefaultGroundWterRoughness;

    public IWorldMapGenerator Build()
    {
        var first = new float[MapSize];
        var last = new float[MapSize];
        Array.Fill(first, MapSize);
        Array.Fill(last, -MapSize);
        HeightMap pattern = new HeightMap(MapSize, MapSize)
        {
            BottomEdge = first,
            TopEdge = last
        };

        FrameBasedHeightMapGenerator temperature =
        new FrameBasedHeightMapGenerator(
            new SquareHeightMapDiamondSquareGenerator(
                new SystemRandomProvider(),
                Math.Min(MapSize, MapSize),
                TemperatureRoughness
            ),
            MapSize,
            MapSize
        );
        temperature.Setup(pattern);
        FrameBasedHeightMapGenerator height =
        new FrameBasedHeightMapGenerator(
            new SquareHeightMapDiamondSquareGenerator(
                new SystemRandomProvider(), MapSquareSize, HeightRoughness
            ),
            MapSize,
            MapSize
        );
        FrameBasedHeightMapGenerator groundWater =
        new FrameBasedHeightMapGenerator(
            new SquareHeightMapDiamondSquareGenerator(
                new SystemRandomProvider(), MapSize, GroundWaterRoughness
            ),
            MapSize,
            MapSize
        );
        FrameBasedHeightMapGenerator precipitation =
        new FrameBasedHeightMapGenerator(
            new SquareHeightMapDiamondSquareGenerator(
                new SystemRandomProvider(),
                Math.Min(MapSize, MapSize),
                PrecipitationRoughness
            ),
            MapSize,
            MapSize
        );
        return new HeightMapBasedGenerator(
            new OneNameGenerator(WorldComponent.DefaultWorldName),
            height,
            temperature,
            groundWater,
            precipitation
        );
    }

    public const int DefaultMapSize = 64;
    public const int DefaultMapSquareSize = 16;

    public const float DefaultHeightRoughness = 0.5f;
    public const float DefaultTemperatureRoughness = 0.5f;
    public const float DefaultPrecipitationRoughness = 0.25f;
    public const float DefaultGroundWterRoughness = 0.25f;

    private StandartSize mapSize = DefaultMapSize;
    private StandartSize mapSquareSize = DefaultMapSquareSize;
}
