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

        cameraComponent = new CameraComponent(
            this,
            new Camera()
        );
        world = new WorldComponent(
            this,
            worldGeneratorBuilder.Build(),
            tileDescriptorBuilder.Build(),
            tileTextureSelectorBuilder.Build()
        );
        
        Components.Add(cameraComponent);
        Components.Add(world);
    }

    protected override void Initialize()
    {
        base.Initialize();

        graphics.IsFullScreen = false;
        graphics.PreferredBackBufferWidth = DefaultScreenWidth;
        graphics.PreferredBackBufferHeight = DefaultScreenHeight;
        graphics.ApplyChanges();

        Camera.Instance.MaxXPosition =
            world.TileDescriptor.Width * world.WorldMap.Width;
        Camera.Instance.MaxYPosition =
            world.TileDescriptor.Height * world.WorldMap.Height * 3.0f / 4;
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

    public const int DefaultScreenWidth = 1600;
    public const int DefaultScreenHeight = 900;

    private readonly CameraComponent cameraComponent;
    private readonly WorldComponent world;
}
