using FeedEveryone.Mono.Components.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FeedEveryoneMono;

public class FeedEveryoneGame : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    Texture2D texture;
    private readonly Camera camera;

    public FeedEveryoneGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        camera = new Camera(this);
    }

    protected override void Initialize()
    {
        _graphics.IsFullScreen = false;
        _graphics.PreferredBackBufferWidth = DefaultScreenWidth;
        _graphics.PreferredBackBufferHeight = DefaultScreenHeight;
        _graphics.ApplyChanges();

        camera.Initialize();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        texture = Content.Load<Texture2D>("coffee_bag");
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
        float cameraCoeff = camera.Height / _graphics.PreferredBackBufferHeight;
        _spriteBatch.Begin();
        _spriteBatch.Draw(
            texture,
            new Rectangle(
                -(int)(camera.Position.X / cameraCoeff),
                -(int)(camera.Position.Y / cameraCoeff),
                (int)(texture.Width / cameraCoeff),
                (int)(texture.Height / cameraCoeff)
            ),
            Color.AliceBlue);
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    public const int DefaultScreenWidth = 1600;
    public const int DefaultScreenHeight = 900;
}
