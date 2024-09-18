using FeedEveryone.Mono.Components.TileProcessing;

namespace FeedEveryone.Mono.Builders;

public class TileFromTextureMapSelectorBuilder
    : IBuilder<ITileTextureSelector>
{
    public TileFromTextureMapSelectorBuilder(
        string textureName,
        WorldUnitSize tileSize,
        IBuilder<ITileTypeQualifier> qualifierBuilder
    )
    {
        this.textureName = textureName;
        this.tileSize = tileSize;
        this.qualifierBuilder = qualifierBuilder;
    }

    public ITileTextureSelector Build()
    {
        return new TileFromTextureMapSelector(
            textureName,
            tileSize,
            qualifierBuilder.Build()
        );
    }

    private readonly WorldUnitSize tileSize;
    private readonly IBuilder<ITileTypeQualifier> qualifierBuilder;

    private readonly string textureName;
}
