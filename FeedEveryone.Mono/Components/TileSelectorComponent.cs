using FeedEveryone.Mono.Components.TileProcessing;
using FeedEveryoneMono;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FeedEveryone.Mono.Components;

public class TileSelectorComponent : DrawableGameComponent
{
    public FeedEveryoneGame FeedEveryoneGame => Game as FeedEveryoneGame;

    public TileSelectorComponent(
        FeedEveryoneGame game,
        ITileDescriptor tileDescriptor
    ) : base(game)
    {
        this.tileDescriptor = tileDescriptor;
    }

    protected override void LoadContent()
    {
        base.LoadContent();
        selectorTexture = FeedEveryoneGame.Content.Load<Texture2D>("cursor");
        spriteBatch = new SpriteBatch(Game.GraphicsDevice);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        tileRectangle =
            FeedEveryoneGame.Camera.WorldToScreen(
                tileDescriptor.GetWorldRectangle(
                    tileDescriptor.GetTileAtPoint(
                        FeedEveryoneGame.Camera.ScreenToWorld(
                            Mouse.GetState().Position
                        )
                    )
                )
            );
    }

    public override void Draw(GameTime gameTime)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(
            selectorTexture, tileRectangle, Color.White
        );
        spriteBatch.End();
    }


    private readonly ITileDescriptor tileDescriptor;

    private SpriteBatch spriteBatch;
    private Texture2D selectorTexture;
    private Rectangle tileRectangle;
}
