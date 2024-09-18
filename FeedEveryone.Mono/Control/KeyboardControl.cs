using Microsoft.Xna.Framework.Input;

namespace FeedEveryone.Mono.Control;

public static class KeyboardControl
{
    public static Keys CameraUp { get; set; } = Keys.W;
    public static Keys CameraDown { get; set; } = Keys.S;
    public static Keys CameraLeft { get; set; } = Keys.A;
    public static Keys CameraRight { get; set; } = Keys.D;

    public static Keys CameraZoomIn { get; set; } = Keys.Q;
    public static Keys CameraZoomOut { get; set; } = Keys.E;
}
