using Microsoft.Xna.Framework.Input;

namespace FeedEveryone.Mono.Control;

public interface IKeyboard
{
    KeyState GetState(Keys key);
}
