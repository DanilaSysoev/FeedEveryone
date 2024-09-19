using System;
using System.Collections.Generic;
using FeedEveryone.Mono.Control;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FeedEveryone.Mono.Components.Drawing;

public class Camera
{
    public Vector2 Position { get; set; } =
        new Vector2(DefaultMinLeft, DefaultMinTop);
    public float Height { get; set; } = DefaultHeight;
    public float AspectRatio { get; set; } = DefaultAspectRatio;
    public float Width => Height * AspectRatio;
    public float RelativeMotionSpeed { get; set; } = DefaultRelativeSpeed;
    public float RelativeZoomSpeed { get; set; } = DefaultRelativeZoomSpeed;
    public float MoveSpeedUpCoeff { get; set; } = DefaultMoveSpeedUpCoeff;
    public float ZoomSpeedUpCoeff { get; set; } = DefaultZoomSpeedUpCoeff;

    public float MaxRight { get; set; } = DefaultMaxRight;
    public float MaxBottom { get; set; } = DefaultMaxBottom;
    public float MinLeft { get; set; } = DefaultMinLeft;
    public float MinTop { get; set; } = DefaultMinTop;

    public float MaxHeight { get; set; } = DefaultMaxHeight;
    public float MinHeight { get; set; } = DefaultMinHeight;

    public Camera()
        : base()
    {
        keyControllers = new List<KeyController>();
    }

    public void Initialize()
    {
        SetupControllers();
    }

    public void Update(GameTime gameTime)
    {
        _gameTime = gameTime;
        foreach(var keyController in keyControllers)
            keyController.Update(gameTime);
    }

    public override string ToString()
    {
        return $"Camera: [{Position.X:0.00}, {Position.Y:0.00}]\n" +
               $"Size: [{Width:0.00}, {Height:0.00}]";
    }

    public void MoveUp()
    {
        MoveOn(0f, -CalculateMotion());
    }
    public void MoveDown()
    {
        MoveOn(0f, CalculateMotion());
    }
    public void MoveLeft()
    {
        MoveOn(-CalculateMotion(), 0f);
    }
    public void MoveRight()
    {
        MoveOn(CalculateMotion(), 0f);
    }
    public void ZoomIn()
    {
        ZoomOn(-CalculateZoom());
    }
    public void ZoomOut()
    {
        ZoomOn(CalculateZoom());
    }
    
    public void MoveUpSpeedy()
    {
        MoveOn(0f, MoveSpeedUpCoeff * -CalculateMotion());
    }
    public void MoveDownSpeedy()
    {
        MoveOn(0f, MoveSpeedUpCoeff * CalculateMotion());
    }
    public void MoveLeftSpeedy()
    {
        MoveOn(MoveSpeedUpCoeff * -CalculateMotion(), 0f);
    }
    public void MoveRightSpeedy()
    {
        MoveOn(MoveSpeedUpCoeff * CalculateMotion(), 0f);
    }
    public void ZoomInSpeedy()
    {
        ZoomOn(-ZoomSpeedUpCoeff * CalculateZoom());
    }
    public void ZoomOutSpeedy()
    {
        ZoomOn(ZoomSpeedUpCoeff * CalculateZoom());
    }
    
    public void ZoomOn(float zoomValue)
    {
        Height += zoomValue;
        HeightCorrection();
        PositionCorrection();
    }

    public void MoveOn(float motionX, float motionY)
    {
        Position += new Vector2(motionX, motionY);
        PositionCorrection();
    }

    public void MoveOn(Vector2 motion)
    {
        Position += motion;
        PositionCorrection();
    }

    public Vector2 ScreenToWorld(int screenHeight, Vector2 point)
    {
        float camCoeff = screenHeight / Height;
        return (point / camCoeff) + Position;
    }
    public Point ScreenToWorld(int screenHeight, Point point)
    {
        float camCoeff = screenHeight / Height;
        return new Point(
            (int)MathF.Round(point.X / camCoeff + Position.X),
            (int)MathF.Round(point.Y / camCoeff + Position.Y)
        );
    }
    public Rectangle ScreenToWorld(int screenHeight, Rectangle rectangle)
    {
        float camCoeff = screenHeight / Height;
        return new Rectangle(
            ScreenToWorld(screenHeight, rectangle.Location),
            new Point(
                (int)MathF.Round(rectangle.Width / camCoeff),
                (int)MathF.Round(rectangle.Height / camCoeff)
            )
        );
    }

    public Vector2 WorldToScreen(int screenHeight, Vector2 point)
    {
        float camCoeff = screenHeight / Height;
        return (point - Position) * camCoeff;
    }
    public Point WorldToScreen(int screenHeight, Point point)
    {
        float camCoeff = screenHeight / Height;
        return new Point(
            (int)MathF.Round((point.X - Position.X) * camCoeff),
            (int)MathF.Round((point.Y - Position.Y) * camCoeff)
        );
    }
    public Rectangle WorldToScreen(int screenHeight, Rectangle rectangle)
    {
        float camCoeff = screenHeight / Height;
        return new Rectangle(
            WorldToScreen(screenHeight, rectangle.Location),
            new Point(
                (int)MathF.Round(rectangle.Width * camCoeff),
                (int)MathF.Round(rectangle.Height * camCoeff)
            )
        );
    }
    
    public float Scale(int screenHeight, float value)
    {
        return value * screenHeight / Height;
    }
    public int Scale(int screenHeight, int value)
    {
        return (int)MathF.Round(value * screenHeight / Height);
    }

    public const float DefaultAspectRatio = 16f / 9f;
    public const float DefaultHeight = 900;
    public const float DefaultRelativeSpeed = 0.3f;
    public const float DefaultRelativeZoomSpeed = 0.3f;
    public const float DefaultMoveSpeedUpCoeff = 3f;
    public const float DefaultZoomSpeedUpCoeff = 3f;

    public const float DefaultMaxRight = 256 * 256;
    public const float DefaultMaxBottom = 256 * 384f;
    public const float DefaultMinLeft = 0f;
    public const float DefaultMinTop = 0f;

    public const float DefaultMaxHeight = 5400f;
    public const float DefaultMinHeight = 320f;


    private float CalculateMotion()
    {
        return RelativeMotionSpeed *
               Height *
               (float)_gameTime.ElapsedGameTime.TotalSeconds;
    }

    private float CalculateZoom()
    {
        return RelativeZoomSpeed *
               Height *
               (float)_gameTime.ElapsedGameTime.TotalSeconds;
    }

    private void PositionCorrection()
    {
        float x = MathF.Max(MathF.Min(MaxRight - Width, Position.X),
                            MinLeft);
        float y = MathF.Max(MathF.Min(MaxBottom - Height, Position.Y),
                            MinTop);
        Position = new Vector2(x, y);
    }

    private void HeightCorrection()
    {
        Height = MathF.Min(Height, MaxHeight);
        Height = MathF.Max(Height, MinHeight);
    }

    private void SetupController(Keys key, Action pressed, Action longPresssed)
    {
        var keyController = new KeyController(Control.Keyboard.Instance, key);
        keyController.Pressed += pressed;
        keyController.LongPresssed += longPresssed;
        keyControllers.Add(keyController);
    }

    private void SetupControllers()
    {
        SetupController(KeyboardControl.CameraUp, MoveUp, MoveUpSpeedy);
        SetupController(KeyboardControl.CameraDown, MoveDown, MoveDownSpeedy);
        SetupController(KeyboardControl.CameraLeft, MoveLeft, MoveLeftSpeedy);
        SetupController(KeyboardControl.CameraRight, MoveRight, MoveRightSpeedy);

        SetupController(KeyboardControl.CameraZoomIn, ZoomIn, ZoomInSpeedy);
        SetupController(KeyboardControl.CameraZoomOut, ZoomOut, ZoomOutSpeedy);
    }

    private readonly List<KeyController> keyControllers;
    private GameTime _gameTime;
}
