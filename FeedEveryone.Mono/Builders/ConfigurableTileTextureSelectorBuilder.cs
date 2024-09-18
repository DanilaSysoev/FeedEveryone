using FeedEveryone.Mono.Components.Service;
using FeedEveryone.Mono.Components.TileProcessing;

namespace FeedEveryone.Mono.Builders;

public class ConfigurableTileTypeQualifierBuilder
    : IBuilder<ITileTypeQualifier>
{
    public ConfigurableTileTypeQualifierBuilder(IJsonProvider jsonProvider)
    {
        this.jsonProvider = jsonProvider;
    }

    public ITileTypeQualifier Build()
    {
        return new ConfigurableTileTypeQualifier(jsonProvider);
    }

    private readonly IJsonProvider jsonProvider;
}
