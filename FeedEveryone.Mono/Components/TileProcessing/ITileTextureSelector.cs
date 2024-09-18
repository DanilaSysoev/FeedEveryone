using FeedEveryone.Core.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FeedEveryone.Mono.Components.TileProcessing;

public interface ITileTextureSelector
{
    Texture2D GetTexture(Tile tile);
    Rectangle GetTextureArea(Tile tile);

    void LoadContent(ContentManager content);
}
