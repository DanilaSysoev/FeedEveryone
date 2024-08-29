namespace FeedEveryone.World;

public class Biome
{
    public float Height { get; private set; }
    public float Temperature { get; private set; }
    public float GroundWaterLevel { get; private set; }
    public float Precipitation { get; private set; }

    public Biome(
        float height,
        float temperature,
        float groundWaterLevel,
        float precipitation
    )
    {
        Height = height;
        Temperature = temperature;
        GroundWaterLevel = groundWaterLevel;
        Precipitation = precipitation;
    }

    public override string ToString()
    {
        return $"{Height:#.##} {Temperature:#.##} " +
               $"{GroundWaterLevel:#.##} {Precipitation:#.##}";
    }
    public override bool Equals(object? obj)
    {
        return obj is Biome biome &&
               Height == biome.Height &&
               Temperature == biome.Temperature &&
               GroundWaterLevel == biome.GroundWaterLevel &&
               Precipitation == biome.Precipitation;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(
            Height, Temperature, GroundWaterLevel, Precipitation
        );
    }
}