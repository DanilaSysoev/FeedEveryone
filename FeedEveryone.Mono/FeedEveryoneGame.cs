using FeedEveryone.Mono.Components.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FeedEveryoneMono;

public class FeedEveryoneGame : Game
{
    private readonly GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private SpriteFont font;
    Texture2D texture;
    private readonly Camera camera;

    public FeedEveryoneGame()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        camera = new Camera();
    }

    protected override void Initialize()
    {
        graphics.IsFullScreen = false;
        graphics.PreferredBackBufferWidth = DefaultScreenWidth;
        graphics.PreferredBackBufferHeight = DefaultScreenHeight;
        graphics.ApplyChanges();

        camera.Initialize();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        texture = Content.Load<Texture2D>("coffee_bag");
        font = Content.Load<SpriteFont>("DebugFont");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        camera.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        float cameraCoeff = camera.Height / graphics.PreferredBackBufferHeight;
        spriteBatch.Begin();
        spriteBatch.Draw(
            texture,
            new Rectangle(
                -(int)(camera.Position.X / cameraCoeff),
                -(int)(camera.Position.Y / cameraCoeff),
                (int)(texture.Width / cameraCoeff),
                (int)(texture.Height / cameraCoeff)
            ),
            Color.AliceBlue);
        spriteBatch.DrawString(
            font,
            camera.ToString(),
            Vector2.Zero,
            Color.Black
        );
        spriteBatch.End();

        base.Draw(gameTime);
    }

    public const int DefaultScreenWidth = 1600;
    public const int DefaultScreenHeight = 900;
}
