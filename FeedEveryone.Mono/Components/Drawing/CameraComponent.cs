using System;
using FeedEveryoneMono;
using Microsoft.Xna.Framework;

namespace FeedEveryone.Mono.Components.Drawing;

public class CameraComponent : GameComponent
{
    public FeedEveryoneGame FeedEveryoneGame => Game as FeedEveryoneGame;
    public Camera Instance { get; private set; }

    public CameraComponent(FeedEveryoneGame game, Camera camera)
        : base(game)
    {
        Instance = camera;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Instance.Update(gameTime);
    }

    public override void Initialize()
    {
        base.Initialize();
        Instance.Initialize();
    }

    public Vector2 WorldToScreen(Vector2 point)
    {
        return Instance.WorldToScreen(FeedEveryoneGame.ScreenHeight, point);
    }
    public Point WorldToScreen(Point point)
    {
        return Instance.WorldToScreen(FeedEveryoneGame.ScreenHeight, point);
    }
    public Rectangle WorldToScreen(Rectangle rectangle)
    {
        return Instance.WorldToScreen(FeedEveryoneGame.ScreenHeight, rectangle);
    }
    public int Scale(int value)
    {
        return Instance.Scale(FeedEveryoneGame.ScreenHeight, value);
    }
    public float Scale(float value)
    {
        return Instance.Scale(FeedEveryoneGame.ScreenHeight, value);
    }

    public override string ToString()
    {
        return Instance.ToString();
    }
}
