using FeedEveryone.Core.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FeedEveryone.Mono.Components.TileProcessing;

public class WorldMapTilesRectanglesAndTextures
{
    public WorldMapTilesRectanglesAndTextures(
        WorldMap worldMap,
        ITileTextureSelector selector
    )
    {
        rectangles = new Rectangle[worldMap.Height, worldMap.Width];
        textures = new Texture2D[worldMap.Height, worldMap.Width];

        for (var line = 0; line < worldMap.Height; line++)
        {
            for (var column = 0; column < worldMap.Width; column++)
            {
                var tile = worldMap[line, column];
                textures[line, column] = selector.GetTexture(tile);
                rectangles[line, column] = selector.GetTextureArea(tile);
            }
        }
    }

    public Rectangle GetTextureArea(int line, int column)
    {
        return rectangles[line, column];
    }
    
    public Texture2D GetTexture(int line, int column)
    {
        return textures[line, column];
    }

    private readonly Rectangle[,] rectangles;
    private readonly Texture2D[,] textures;
}
