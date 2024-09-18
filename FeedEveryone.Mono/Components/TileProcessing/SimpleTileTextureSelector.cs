using FeedEveryone.Core.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FeedEveryone.Mono.Components.TileProcessing;

public class SimpleTileTextureSelector : ITileTextureSelector
{
    public SimpleTileTextureSelector(string textureName)
    {
        this.textureName = textureName;
    }

    public Texture2D GetTexture(Tile tile)
    {
        return texture;
    }

    public Rectangle GetTextureArea(Tile tile)
    {
        return texture.Bounds;
    }

    public void LoadContent(ContentManager content)
    {
        texture = content.Load<Texture2D>(textureName);
    }

    private Texture2D texture;
    private readonly string textureName;
}
