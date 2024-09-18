using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using FeedEveryone.Core.World;
using FeedEveryone.Mono.Exceptions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FeedEveryone.Mono.Components.TileProcessing;

public class TileFromTextureMapSelector : ITileTextureSelector
{
    public TileFromTextureMapSelector(
        string textureName,
        WorldUnitSize tileSize,
        ITileTypeQualifier qualifier
    )
    {
        this.textureName = textureName;
        this.tileSize = tileSize;
        this.qualifier = qualifier;

        positions.Load();
    }
    
    public Texture2D GetTexture(Tile tile)
    {
        return texture;
    }

    public Rectangle GetTextureArea(Tile tile)
    {
        var tileType = qualifier.Qualify(tile);
        var position = positions.GetPosition(tileType);
        if(tileType == TileType.Void)
            Console.WriteLine(tile.Position);

        return new Rectangle(position.X * tileSize.Width,
                             position.Y * tileSize.Height,
                             tileSize.Width,
                             tileSize.Height);
    }

    public void LoadContent(ContentManager content)
    {
        texture = content.Load<Texture2D>(textureName);
    }

    private Texture2D texture;
    private readonly string textureName;
    private readonly WorldUnitSize tileSize;
    private readonly ITileTypeQualifier qualifier;
    private readonly TilePositions positions = new();

    private sealed class TilePositions
    {
        public Point GetPosition(TileType tileType)
        {
            if (positions.ContainsKey(tileType))
            {
                return new Point(positions[tileType][0],
                                 positions[tileType][1]);
            }
            throw new UndefinedTileTextureException(
                tileType,
                "Cant't find position for tile type on texture map." +
                $"Check \"{PositionsFileName}\" file."
            );
        }

        public void Load()
        {
            var json = JsonSerializer.Deserialize<Dictionary<string, int[]>>(
                File.ReadAllText(PositionsFileName)
            );
            foreach (var (key, value) in json)
            {
                var tileType = Enum.Parse<TileType>(key);
                positions.Add(tileType, value);
            }
        }

        private readonly Dictionary<TileType, int[]> positions = new ();

        private const string PositionsFileName = "Content/positions.json";
    }
}
