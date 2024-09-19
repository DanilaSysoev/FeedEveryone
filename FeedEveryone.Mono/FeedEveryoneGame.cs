using FeedEveryone.Core.Service.WorldGeneration.WorldMapGeneration;
using FeedEveryone.Mono.Builders;
using FeedEveryone.Mono.Components;
using FeedEveryone.Mono.Components.Drawing;
using FeedEveryone.Mono.Components.TileProcessing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FeedEveryoneMono;

public class FeedEveryoneGame : Game
{
    public CameraComponent Camera => cameraComponent;
    public int ScreenHeight => graphics.PreferredBackBufferHeight;
    public int ScreenWidth => graphics.PreferredBackBufferWidth;
    public WorldComponent World => world;
    public TileSelector TileSelector => tileSelector;

    private readonly GraphicsDeviceManager graphics;

    public FeedEveryoneGame(
        IBuilder<IWorldMapGenerator> worldGeneratorBuilder,
        IBuilder<ITileDescriptor> tileDescriptorBuilder,
        IBuilder<ITileTextureSelector> tileTextureSelectorBuilder
    )
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        CreateCameraComponent();
        CreateWorldComponent(worldGeneratorBuilder,
                             tileDescriptorBuilder,
                             tileTextureSelectorBuilder);
        CreateTileSelector(tileDescriptorBuilder);

        Components.Add(cameraComponent);
        Components.Add(world);
        Components.Add(tileSelector);
    }

    

    protected override void Initialize()
    {
        base.Initialize();

        graphics.IsFullScreen = false;
        graphics.PreferredBackBufferWidth = DefaultScreenWidth;
        graphics.PreferredBackBufferHeight = DefaultScreenHeight;
        graphics.ApplyChanges();

        Camera.Instance.MaxRight =
            world.TileDescriptor.Width * world.WorldMap.Width;
        Camera.Instance.MaxBottom =
            world.TileDescriptor.Width * world.WorldMap.Height * 3.0f / 4;
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        base.Draw(gameTime);
    }

    private void CreateTileSelector(
        IBuilder<ITileDescriptor> tileDescriptorBuilder
    )
    {
        tileSelector = new TileSelector(
            this,
            tileDescriptorBuilder.Build()
        )
        {
            DrawOrder = TileSelectorDrawingOrder,
            UpdateOrder = TileSelectorUpdateOrder
        };
    }

    private void CreateWorldComponent(
        IBuilder<IWorldMapGenerator> worldGeneratorBuilder,
        IBuilder<ITileDescriptor> tileDescriptorBuilder,
        IBuilder<ITileTextureSelector> tileTextureSelectorBuilder
    )
    {
        world = new WorldComponent(
            this,
            worldGeneratorBuilder.Build(),
            tileDescriptorBuilder.Build(),
            tileTextureSelectorBuilder.Build()
        )
        {
            DrawOrder = MapDrawingOrder,
            UpdateOrder = MapUpdateOrder
        };
    }

    private void CreateCameraComponent()
    {
        cameraComponent = new CameraComponent(
            this,
            new Camera()
        );
    }

    public const int DefaultScreenWidth = 1600;
    public const int DefaultScreenHeight = 900;

    private CameraComponent cameraComponent;
    private WorldComponent world;
    private TileSelector tileSelector;

    private const int MapDrawingOrder = 0;
    private const int TileSelectorDrawingOrder = 64;

    private const int MapUpdateOrder = 0;
    private const int TileSelectorUpdateOrder = 64;
}
