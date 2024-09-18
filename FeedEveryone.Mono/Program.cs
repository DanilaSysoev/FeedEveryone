using FeedEveryone.Mono.Builders;
using FeedEveryone.Mono.Components.Service;
using FeedEveryone.Mono.Components.TileProcessing;
using FeedEveryoneMono;

const int TileHeight = 384;
const int TileWidth = 256;

using var game = new FeedEveryoneGame(
    new SimpleSquareBuilder(),
    new HexTileDescriptorBuilder(TileHeight, TileWidth),
    new TileFromTextureMapSelectorBuilder(
        "Textures/tiles",
        new WorldUnitSize { Height = TileHeight, Width = TileWidth },
        new ConfigurableTileTypeQualifierBuilder(
            new FromFileJsonProvider("Content/tile_types_configs.json")
        )
    )
);
game.Run();
