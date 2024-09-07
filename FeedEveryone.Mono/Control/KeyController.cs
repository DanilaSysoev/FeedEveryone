using System;
using FeedEveryone.Mono.Control;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FeedEveryoneMono.Control;

public class KeyController
{
    public Keys Key { get; }
    public float LongPressTime { get; set; } = DefaultLongPressTime;

    public bool IsPressed => currentFrameState == KeyState.Down;
    public bool IsReleased => currentFrameState == KeyState.Up;
    public bool IsPressedOn => currentFrameState == KeyState.Down &&
                               (previousFrameState == KeyState.Up || previousFrameState is null);
    public bool IsReleasedOn => currentFrameState == KeyState.Up &&
                                previousFrameState == KeyState.Down;
    public bool IsLongPressed => IsPressed &&
                                 LongPressTimeElapsed();

    public event Action Pressed;
    public event Action Released;
    public event Action Press;
    public event Action Release;
    public event Action LongPresssed;

    public KeyController(IKeyboard keyboard, Keys key)
    {
        this.keyboard = keyboard;
        Key = key;
    }

    public void Update(GameTime gameTime)
    {
        _gameTime = gameTime;

        previousFrameState = currentFrameState;
        currentFrameState = keyboard.GetState(Key);

        if (IsPressedOn) pressTime = gameTime.TotalGameTime;

        InvokeEvents();
    }

    public const float DefaultLongPressTime = 1f;


    private void InvokeEvents()
    {
        if (IsPressedOn) Press?.Invoke();
        if (IsReleasedOn) Release?.Invoke();
        if (IsPressed)
        {
            if (IsLongPressed && LongPresssed is not null)
                LongPresssed.Invoke();
            else
                Pressed?.Invoke();
        }
        if (IsReleased) Released?.Invoke();
    }

    private bool LongPressTimeElapsed()
    {
        return (_gameTime.TotalGameTime - 
                pressTime).TotalSeconds > LongPressTime;
    }

    private KeyState? previousFrameState = null;
    private KeyState? currentFrameState;
    private TimeSpan pressTime;
    private GameTime _gameTime;

    private readonly IKeyboard keyboard;
}
