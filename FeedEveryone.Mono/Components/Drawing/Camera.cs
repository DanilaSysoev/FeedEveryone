using System;
using System.Collections.Generic;
using FeedEveryoneMono.Control;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FeedEveryone.Mono.Components.Drawing;

public class Camera
{
    public Vector2 Position { get; set; }
    public float Height { get; set; } = DefaultHeight;
    public float AspectRatio { get; set; } = DefaultAspectRatio;
    public float Width => Height * AspectRatio;
    public float RelativeMotionSpeed { get; set; } = DefaultRelativeSpeed;
    public float RelativeZoomSpeed { get; set; } = DefaultRelativeZoomSpeed;
    public float MoveSpeedUpCoeff { get; set; } = DefaultMoveSpeedUpCoeff;
    public float ZoomSpeedUpCoeff { get; set; } = DefaultZoomSpeedUpCoeff;

    public float MaxXPosition { get; set; } = DefaultMaxXPosition;
    public float MaxYPosition { get; set; } = DefaultMaxYPosition;
    public float MinXPosition { get; set; } = DefaultMinXPosition;
    public float MinYPosition { get; set; } = DefaultMinYPosition;

    public float MaxHeight { get; set; } = DefaultMaxHeight;
    public float MinHeight { get; set; } = DefaultMinHeight;

    public Camera()
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

    public const float DefaultAspectRatio = 16f / 9f;
    public const float DefaultHeight = 900;
    public const float DefaultRelativeSpeed = 0.3f;
    public const float DefaultRelativeZoomSpeed = 0.3f;
    public const float DefaultMoveSpeedUpCoeff = 3f;
    public const float DefaultZoomSpeedUpCoeff = 3f;

    public const float DefaultMaxXPosition = 1600f;
    public const float DefaultMaxYPosition = 900f;
    public const float DefaultMinXPosition = -1600f;
    public const float DefaultMinYPosition = -900f;

    public const float DefaultMaxHeight = 4800f;
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
        float x = MathF.Max(MathF.Min(MaxXPosition, Position.X),
                            MinXPosition);
        float y = MathF.Max(MathF.Min(MaxYPosition, Position.Y),
                            MinYPosition);
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
