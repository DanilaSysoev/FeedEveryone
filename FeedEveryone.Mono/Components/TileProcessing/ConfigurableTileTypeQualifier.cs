using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using FeedEveryone.Core.World;
using FeedEveryone.Mono.Components.Service;

namespace FeedEveryone.Mono.Components.TileProcessing;

public class ConfigurableTileTypeQualifier : ITileTypeQualifier
{
    public ConfigurableTileTypeQualifier(IJsonProvider configProvider)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            }
        };

        configs = JsonSerializer.Deserialize<List<TileTypeConfig>>(
            configProvider.Json, options
        );
    }

    public TileType Qualify(Tile tile)
    {
        return configs.Where(config => config.IsMatch(tile))
                      .OrderBy(config => config.Priority)
                      .LastOrDefault()
                      ?.TileType
                      ?? TileType.Void;
    }

    private readonly List<TileTypeConfig> configs;
}
