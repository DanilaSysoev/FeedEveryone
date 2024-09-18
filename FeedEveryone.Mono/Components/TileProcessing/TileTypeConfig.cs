using System;
using FeedEveryone.Core.World;

namespace FeedEveryone.Mono.Components.TileProcessing;

public class TileTypeConfig
{
    public TileType TileType { get; set; }
    public float MinHeight { get; set; } = 0.0f;
    public float MaxHeight { get; set; } = 1.0f;
    public float MinTemperature { get; set; } = 0.0f;
    public float MaxTemperature { get; set; } = 1.0f;
    public float MinGroundWater { get; set; } = 0.0f;
    public float MaxGroundWater { get; set; } = 1.0f;
    public float MinPrecipitation { get; set; } = 0.0f;
    public float MaxPrecipitation { get; set; } = 1.0f;
    public int Priority { get; set; } = 0;

    public TileTypeConfig()
    {
        Validate();
    }

    private void Validate()
    {
        if(MaxHeight < MinHeight ||
           MaxTemperature < MinTemperature ||
           MaxGroundWater < MinGroundWater ||
           MaxPrecipitation < MinPrecipitation)
        {
            throw new ArgumentException("Invalid tile type configuration");
        }
    }

    public bool IsMatch(Tile tile)
    {
        return tile.Biome.Height >= MinHeight &&
               tile.Biome.Height < MaxHeight &&
               tile.Biome.Temperature >= MinTemperature &&
               tile.Biome.Temperature < MaxTemperature &&
               tile.Biome.GroundWaterLevel >= MinGroundWater &&
               tile.Biome.GroundWaterLevel < MaxGroundWater &&
               tile.Biome.Precipitation >= MinPrecipitation &&
               tile.Biome.Precipitation < MaxPrecipitation;
    }

}
