using System;
using FeedEveryone.Mono.Components.TileProcessing;
using Microsoft.Xna.Framework.Content;

namespace FeedEveryone.Mono.Builders;

public interface ITileTextureSelectorBuilder : IBuilder<ITileTextureSelector>
{
    void LoadContent(ContentManager content);
}
