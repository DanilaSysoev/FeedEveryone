using Microsoft.Xna.Framework.Input;

namespace FeedEveryone.Mono.Control;

public class Keyboard : IKeyboard
{
    public KeyState GetState(Keys key)
    {
        return Microsoft.Xna.Framework.Input.Keyboard.GetState()[key];
    }

    public static readonly IKeyboard Instance = new Keyboard();
}
