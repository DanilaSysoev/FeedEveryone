using FeedEveryone.Core.Service;
using FeedEveryone.Mono.Components.TileProcessing;
using FeedEveryoneMono;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FeedEveryone.Mono.Components;

public class TileSelector : DrawableGameComponent
{
    public FeedEveryoneGame FeedEveryoneGame => Game as FeedEveryoneGame;

    public TileSelector(
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
        var tilePos = tileDescriptor.GetTileAtPoint(
            FeedEveryoneGame.Camera.ScreenToWorld(
                Mouse.GetState().Position
            )
        );
        if (InsideTheWorld(tilePos))
        {
            tileRectangle =
                FeedEveryoneGame.World.GetLastDrawRectangleOfTile(
                    tileDescriptor.GetTileAtPoint(
                        FeedEveryoneGame.Camera.ScreenToWorld(
                            Mouse.GetState().Position
                        )
                    )
                );
        }
        else
            tileRectangle = null;
    }

    public override void Draw(GameTime gameTime)
    {
        if(tileRectangle.HasValue)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(
                selectorTexture, tileRectangle.Value, Color.White
            );
            spriteBatch.End();
        }
    }

    private bool InsideTheWorld(Position tilePos)
    {
        return tilePos.Line >= 0 &&
               tilePos.Column >= 0 &&
               tilePos.Line < FeedEveryoneGame.World.WorldMap.Height &&
               tilePos.Column < FeedEveryoneGame.World.WorldMap.Width;
    }


    private readonly ITileDescriptor tileDescriptor;

    private SpriteBatch spriteBatch;
    private Texture2D selectorTexture;
    private Rectangle? tileRectangle;
}
