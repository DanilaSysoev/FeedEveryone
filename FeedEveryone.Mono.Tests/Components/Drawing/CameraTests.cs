using FeedEveryone.Mono.Components.Drawing;
using Microsoft.Xna.Framework;

namespace FeedEveryone.Mono.Tests.Components.Drawing;

public class CameraTests
{
    private Camera camera = null!;

    [SetUp]
    public void Setup()
    {
        camera = new Camera();

        camera.Position = Vector2.Zero;
        camera.Height = 900;
        camera.RelativeMotionSpeed = 1;
        camera.RelativeZoomSpeed = 1;
        camera.MaxHeight = 90000;
        camera.MinHeight = 90;
        camera.MinXPosition = -16000;
        camera.MaxXPosition = 16000;
        camera.MinYPosition = -9000;
        camera.MaxYPosition = 9000;
        camera.MoveSpeedUpCoeff = 2;
        camera.ZoomSpeedUpCoeff = 2;
    }

    [Test]
    public void MoveOn_MoveOnVector_PositionChanged()
    {
        var oldPos = camera.Position;
        camera.MoveOn(new Vector2(10, 20));

        var newPos = oldPos + new Vector2(10, 20);
        Assert.That(camera.Position, Is.EqualTo(newPos));
    }
    [Test]
    public void MoveOn_MoveTooLarge_PositionChangedToMax()
    {
        camera.MoveOn(new Vector2(100000, 200000));

        var newPos = new Vector2(camera.MaxXPosition, camera.MaxYPosition);
        Assert.That(camera.Position, Is.EqualTo(newPos));
    }
    [Test]
    public void MoveOn_MoveTooLargeInNegative_PositionChangedToMin()
    {
        camera.MoveOn(new Vector2(-100000, -200000));

        var newPos = new Vector2(camera.MinXPosition, camera.MinYPosition);
        Assert.That(camera.Position, Is.EqualTo(newPos));
    }

    [Test]
    public void ZoomOn_ZoomOnPositive_HeightInreased()
    {
        var oldHeight = camera.Height;
        camera.ZoomOn(100);
        var newHeight = oldHeight + 100;

        Assert.That(camera.Height, Is.EqualTo(newHeight));
    }
    [Test]
    public void ZoomOn_ZoomOnNegative_HeightDecreased()
    {
        var oldHeight = camera.Height;
        camera.ZoomOn(-100);
        var newHeight = oldHeight - 100;
        
        Assert.That(camera.Height, Is.EqualTo(newHeight));
    }
    [Test]
    public void ZoomOn_ZoomOnPositiveTooLarge_HeightEqualsMax()
    {
        camera.ZoomOn(100000);

        Assert.That(camera.Height, Is.EqualTo(camera.MaxHeight));
    }
    [Test]
    public void ZoomOn_ZoomOnNegativeTooLarge_HeightEqualsMin()
    {
        camera.ZoomOn(-100000);
        
        Assert.That(camera.Height, Is.EqualTo(camera.MinHeight));
    }
}
