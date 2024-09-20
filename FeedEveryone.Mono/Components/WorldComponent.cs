using FeedEveryone.Core.Service.WorldGeneration.WorldMapGeneration;
using FeedEveryone.Core.World;
using FeedEveryone.Mono.Components.Service;
using FeedEveryone.Mono.Components.TileProcessing;
using FeedEveryone.Mono.Extensions;
using FeedEveryoneMono;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FeedEveryone.Mono.Components;

public class WorldComponent : DrawableGameComponent
{
    public WorldMap WorldMap { get; private set; } = null!;
    public WorldUnitSize WorldSize { get; private set; } = null!;
    public FeedEveryoneGame FeedEveryoneGame => Game as FeedEveryoneGame;
    public ITileDescriptor TileDescriptor => tileDescriptor;

    public WorldComponent(
        FeedEveryoneGame game,
        IWorldMapGenerator mapGenerator,
        ITileDescriptor tileDescriptor,
        ITileTextureSelector tileTextureSelector
    )
        : base(game)
    {
        this.mapGenerator = mapGenerator;
        this.tileDescriptor = tileDescriptor;
        this.tileTextureSelector = tileTextureSelector;
    }

    protected override void LoadContent()
    {
        base.LoadContent();
        tileTextureSelector.LoadContent(Game.Content);
        spriteBatch = new SpriteBatch(Game.GraphicsDevice);
    }

    public override void Initialize()
    {
        base.Initialize();

        WorldMap = mapGenerator.Generate();
        WorldSize = new WorldUnitSize(WorldMap.Height, WorldMap.Width);

        tilesRectsAndTextures = new WorldMapTilesRectanglesAndTextures(
            WorldMap,
            tileTextureSelector
        );
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        Rectangle drawingArea = 
            new MapDrawingAreaSelector(
                FeedEveryoneGame.Camera.Instance.Position,
                FeedEveryoneGame.Camera.Instance.Height,
                FeedEveryoneGame.Camera.Instance.Width,
                new WorldUnitSize(tileDescriptor.Height, 
                                  tileDescriptor.Width),
                AdditionBorderTiles
            ).GetDrawingArea();
        

        spriteBatch.Begin();
        DrawTiles(CorrectionArea(drawingArea));
        spriteBatch.End();
    }


    private Rectangle CorrectionArea(Rectangle drawingArea)
    {
        if (drawingArea.X < 0)
            drawingArea.X = 0;
        if (drawingArea.Y < 0)
            drawingArea.Y = 0;

        if (drawingArea.X + drawingArea.Width >= WorldMap.Width)
            drawingArea.Width = WorldMap.Width - drawingArea.X - 1;
        if (drawingArea.Y + drawingArea.Height >= WorldMap.Height)
            drawingArea.Height = WorldMap.Height - drawingArea.Y - 1;

        return drawingArea;
    }

    private void DrawTiles(Rectangle area)
    {
        for(int line = area.Y; line < area.Y + area.Height; ++line)
            DrawLineOfTiles(line, area);
    }

    private void DrawLineOfTiles(int line, Rectangle area)
    {
        for (int col = area.X; col < area.X + area.Width; ++col)
            DrawTile(line, col);
    }

    private void DrawTile(int line, int col)
    {
        spriteBatch.Draw(
            tilesRectsAndTextures.GetTexture(line, col),
            GetScreenRectOfTile(line, col).Expand(1),
            tilesRectsAndTextures.GetTextureArea(line, col),
            Color.White
        );
    }

    private Rectangle GetScreenRectOfTile(int line, int col)
    {
        var tileWorldRect = tileDescriptor.GetWorldRectangle(line, col);
        var screenRect = FeedEveryoneGame.Camera.WorldToScreen(tileWorldRect);
        return screenRect;
    }

    public const string DefaultWorldName = "Feedland";
    
    private readonly IWorldMapGenerator mapGenerator;
    private readonly ITileDescriptor tileDescriptor;
    private readonly ITileTextureSelector tileTextureSelector;
    private WorldMapTilesRectanglesAndTextures tilesRectsAndTextures = null!;
    private SpriteBatch spriteBatch;

    private const int AdditionBorderTiles = 4;
}
