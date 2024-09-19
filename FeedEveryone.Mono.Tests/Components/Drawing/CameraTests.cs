using FeedEveryone.Core.Service;
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
        camera.MinLeft = -16000;
        camera.MaxRight = 16000;
        camera.MinTop = -9000;
        camera.MaxBottom = 9000;
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

        var newPos = new Vector2(camera.MaxRight - camera.Width,
                                 camera.MaxBottom - camera.Height);
        Assert.That(camera.Position, Is.EqualTo(newPos));
    }
    [Test]
    public void MoveOn_MoveTooLargeInNegative_PositionChangedToMin()
    {
        camera.MoveOn(new Vector2(-100000, -200000));

        var newPos = new Vector2(camera.MinLeft, camera.MinTop);
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

    [Test]
    public void WorldToScreen_ScaleEqualsOnePosZero_ReturnsSame()
    {
        Point point = new Point(10, 20);
        Vector2 pos = new Vector2(1, 2);
        Rectangle rect = new Rectangle(4, 5, 6, 7);
        camera.Position = Vector2.Zero;

        Assert.That(camera.WorldToScreen(900, point), Is.EqualTo(point));
        Assert.That(camera.WorldToScreen(900, pos), Is.EqualTo(pos));
        Assert.That(camera.WorldToScreen(900, rect), Is.EqualTo(rect));
    }
    [Test]
    public void WorldToScreen_ScaleEqualsOnePosPosXPosY_ReturnsCorrect()
    {
        Point point = new Point(10, 20);
        Vector2 pos = new Vector2(1, 2);
        Rectangle rect = new Rectangle(4, 5, 6, 7);

        camera.Position = new Vector2(10, 20);

        Assert.That(camera.WorldToScreen(900, point), Is.EqualTo(new Point(0, 0)));
        Assert.That(camera.WorldToScreen(900, pos), Is.EqualTo(new Vector2(-9, -18)));
        Assert.That(camera.WorldToScreen(900, rect), Is.EqualTo(new Rectangle(-6, -15, 6, 7)));
    }
    [Test]
    public void WorldToScreen_ScaleEqualsOnePosPosXNegY_ReturnsCorrect()
    {
        Point point = new Point(10, 20);
        Vector2 pos = new Vector2(1, 2);
        Rectangle rect = new Rectangle(4, 5, 6, 7);

        camera.Position = new Vector2(10, -20);

        Assert.That(camera.WorldToScreen(900, point), Is.EqualTo(new Point(0, 40)));
        Assert.That(camera.WorldToScreen(900, pos), Is.EqualTo(new Vector2(-9, 22)));
        Assert.That(camera.WorldToScreen(900, rect), Is.EqualTo(new Rectangle(-6, 25, 6, 7)));
    }
    [Test]
    public void WorldToScreen_ScaleEqualsOnePosNegXNegY_ReturnsCorrect()
    {
        Point point = new Point(10, 20);
        Vector2 pos = new Vector2(1, 2);
        Rectangle rect = new Rectangle(4, 5, 6, 7);

        camera.Position = new Vector2(-10, -20);

        Assert.That(camera.WorldToScreen(900, point), Is.EqualTo(new Point(20, 40)));
        Assert.That(camera.WorldToScreen(900, pos), Is.EqualTo(new Vector2(11, 22)));
        Assert.That(camera.WorldToScreen(900, rect), Is.EqualTo(new Rectangle(14, 25, 6, 7)));
    }
    [Test]
    public void WorldToScreen_ScaleEqualsOnePosNegXPosY_ReturnsCorrect()
    {
        Point point = new Point(10, 20);
        Vector2 pos = new Vector2(1, 2);
        Rectangle rect = new Rectangle(4, 5, 6, 7);

        camera.Position = new Vector2(-10, 20);

        Assert.That(camera.WorldToScreen(900, point), Is.EqualTo(new Point(20, 0)));
        Assert.That(camera.WorldToScreen(900, pos), Is.EqualTo(new Vector2(11, -18)));
        Assert.That(camera.WorldToScreen(900, rect), Is.EqualTo(new Rectangle(14, -15, 6, 7)));
    }


    [Test]
    public void WorldToScreen_ScaleLessThanOnePosZero_ReturnsCorrect()
    {
        Point point = new Point(10, 20);
        Rectangle rect = new Rectangle(4, 5, 6, 7);
        camera.Position = Vector2.Zero;

        Assert.That(camera.WorldToScreen(450, point), Is.EqualTo(new Point(5, 10)));
        Assert.That(camera.WorldToScreen(450, rect), Is.EqualTo(new Rectangle(2, 2, 3, 4)));
    }
    [Test]
    public void WorldToScreen_ScaleLessThanOnePosPosXPosY_ReturnsCorrect()
    {
        Point point = new Point(10, 20);
        Rectangle rect = new Rectangle(4, 5, 6, 7);

        camera.Position = new Vector2(10, 20);

        Assert.That(camera.WorldToScreen(450, point), Is.EqualTo(new Point(0, 0)));
        Assert.That(camera.WorldToScreen(450, rect), Is.EqualTo(new Rectangle(-3, -8, 3, 4)));
    }
    [Test]
    public void WorldToScreen_ScaleLessThanOnePosPosXNegY_ReturnsCorrect()
    {
        Point point = new Point(10, 20);
        Rectangle rect = new Rectangle(4, 5, 6, 7);

        camera.Position = new Vector2(10, -20);

        Assert.That(camera.WorldToScreen(450, point), Is.EqualTo(new Point(0, 20)));
        Assert.That(camera.WorldToScreen(450, rect), Is.EqualTo(new Rectangle(-3, 12, 3, 4)));
    }
    [Test]
    public void WorldToScreen_ScaleLessThanOnePosNegXNegY_ReturnsCorrect()
    {
        Point point = new Point(10, 20);
        Rectangle rect = new Rectangle(4, 5, 6, 7);

        camera.Position = new Vector2(-10, -20);

        Assert.That(camera.WorldToScreen(450, point), Is.EqualTo(new Point(10, 20)));
        Assert.That(camera.WorldToScreen(450, rect), Is.EqualTo(new Rectangle(7, 12, 3, 4)));
    }
    [Test]
    public void WorldToScreen_ScaleLessThanOnePosNegXPosY_ReturnsCorrect()
    {
        Point point = new Point(10, 20);
        Rectangle rect = new Rectangle(4, 5, 6, 7);

        camera.Position = new Vector2(-10, 20);

        Assert.That(camera.WorldToScreen(450, point), Is.EqualTo(new Point(10, 0)));
        Assert.That(camera.WorldToScreen(450, rect), Is.EqualTo(new Rectangle(7, -8, 3, 4)));
    }


    [Test]
    public void WorldToScreen_ScaleScaleGreaterThanOnePosZero_ReturnsCorrect()
    {
        Point point = new Point(10, 20);
        Rectangle rect = new Rectangle(4, 5, 6, 7);
        camera.Position = Vector2.Zero;

        Assert.That(camera.WorldToScreen(1200, point), Is.EqualTo(new Point(13, 27)));
        Assert.That(camera.WorldToScreen(1200, rect), Is.EqualTo(new Rectangle(5, 7, 8, 9)));
    }
    [Test]
    public void WorldToScreen_ScaleScaleGreaterThanOnePosPosXPosY_ReturnsCorrect()
    {
        Point point = new Point(10, 20);
        Rectangle rect = new Rectangle(4, 5, 6, 7);

        camera.Position = new Vector2(10, 20);

        Assert.That(camera.WorldToScreen(1200, point), Is.EqualTo(new Point(0, 0)));
        Assert.That(camera.WorldToScreen(1200, rect), Is.EqualTo(new Rectangle(-8, -20, 8, 9)));
    }
    [Test]
    public void WorldToScreen_ScaleScaleGreaterThanOnePosPosXNegY_ReturnsCorrect()
    {
        Point point = new Point(10, 20);
        Rectangle rect = new Rectangle(4, 5, 6, 7);

        camera.Position = new Vector2(10, -20);

        Assert.That(camera.WorldToScreen(1200, point), Is.EqualTo(new Point(0, 53)));
        Assert.That(camera.WorldToScreen(1200, rect), Is.EqualTo(new Rectangle(-8, 33, 8, 9)));
    }
    [Test]
    public void WorldToScreen_ScaleScaleGreaterThanOnePosNegXNegY_ReturnsCorrect()
    {
        Point point = new Point(10, 20);
        Rectangle rect = new Rectangle(4, 5, 6, 7);

        camera.Position = new Vector2(-10, -20);

        Assert.That(camera.WorldToScreen(1200, point), Is.EqualTo(new Point(27, 53)));
        Assert.That(camera.WorldToScreen(1200, rect), Is.EqualTo(new Rectangle(19, 33, 8, 9)));
    }
    [Test]
    public void WorldToScreen_ScaleScaleGreaterThanOnePosNegXPosY_ReturnsCorrect()
    {
        Point point = new Point(10, 20);
        Rectangle rect = new Rectangle(4, 5, 6, 7);

        camera.Position = new Vector2(-10, 20);

        Assert.That(camera.WorldToScreen(1200, point), Is.EqualTo(new Point(27, 0)));
        Assert.That(camera.WorldToScreen(1200, rect), Is.EqualTo(new Rectangle(19, -20, 8, 9)));
    }

    [Test]
    public void ZoomOut_ScaleWithOutOfRightBoundsY_PositionChanged()
    {
        camera.Position = new Vector2(0, camera.MaxBottom - camera.Height);
        GameTime gameTime = new GameTime(
            TimeSpan.Zero,
            TimeSpan.FromSeconds(1)
        );
        camera.Update(gameTime);
        camera.ZoomOut();
        Assert.That(camera.Position,
                    Is.EqualTo(new Vector2(0, camera.MaxBottom - camera.Height)));
    }

    [Test]
    public void ZoomOut_ScaleWithOutOfRightBoundsX_PositionChanged()
    {
        camera.Position = new Vector2(camera.MaxRight - camera.Width, 0);
        GameTime gameTime = new GameTime(
            TimeSpan.Zero,
            TimeSpan.FromSeconds(1)
        );
        camera.Update(gameTime);
        camera.ZoomOut();
        Assert.That(camera.Position,
                    Is.EqualTo(new Vector2(camera.MaxRight - camera.Width, 0)));
    }

   
   
    [Test]
    public void ScreenToWorld_ScaleEqualsOnePosZero_ReturnsSame()
    {
        Point point = new Point(10, 20);
        Vector2 pos = new Vector2(1, 2);
        Rectangle rect = new Rectangle(4, 5, 6, 7);
        camera.Position = Vector2.Zero;

        Assert.That(camera.ScreenToWorld(900, point), Is.EqualTo(point));
        Assert.That(camera.ScreenToWorld(900, pos), Is.EqualTo(pos));
        Assert.That(camera.ScreenToWorld(900, rect), Is.EqualTo(rect));
    }
    [Test]
    public void ScreenToWorld_ScaleEqualsOnePosPosXPosY_ReturnsCorrect()
    {
        Point point = new Point(10, 20);
        Vector2 pos = new Vector2(1, 2);
        Rectangle rect = new Rectangle(4, 5, 6, 7);

        camera.Position = new Vector2(10, 20);

        Assert.That(camera.ScreenToWorld(900, point), Is.EqualTo(new Point(20, 40)));
        Assert.That(camera.ScreenToWorld(900, pos), Is.EqualTo(new Vector2(11, 22)));
        Assert.That(camera.ScreenToWorld(900, rect), Is.EqualTo(new Rectangle(14, 25, 6, 7)));
    }
    [Test]
    public void ScreenToWorld_ScaleEqualsOnePosPosXNegY_ReturnsCorrect()
    {
        Point point = new Point(10, 20);
        Vector2 pos = new Vector2(1, 2);
        Rectangle rect = new Rectangle(4, 5, 6, 7);

        camera.Position = new Vector2(10, -20);

        Assert.That(camera.ScreenToWorld(900, point), Is.EqualTo(new Point(20, 0)));
        Assert.That(camera.ScreenToWorld(900, pos), Is.EqualTo(new Vector2(11, -18)));
        Assert.That(camera.ScreenToWorld(900, rect), Is.EqualTo(new Rectangle(14, -15, 6, 7)));
    }
    [Test]
    public void ScreenToWorld_ScaleEqualsOnePosNegXNegY_ReturnsCorrect()
    {
        Point point = new Point(10, 20);
        Vector2 pos = new Vector2(1, 2);
        Rectangle rect = new Rectangle(4, 5, 6, 7);

        camera.Position = new Vector2(-10, -20);

        Assert.That(camera.ScreenToWorld(900, point), Is.EqualTo(new Point(0, 0)));
        Assert.That(camera.ScreenToWorld(900, pos), Is.EqualTo(new Vector2(-9, -18)));
        Assert.That(camera.ScreenToWorld(900, rect), Is.EqualTo(new Rectangle(-6, -15, 6, 7)));
    }
    [Test]
    public void ScreenToWorld_ScaleEqualsOnePosNegXPosY_ReturnsCorrect()
    {
        Point point = new Point(10, 20);
        Vector2 pos = new Vector2(1, 2);
        Rectangle rect = new Rectangle(4, 5, 6, 7);

        camera.Position = new Vector2(-10, 20);

        Assert.That(camera.ScreenToWorld(900, point), Is.EqualTo(new Point(0, 40)));
        Assert.That(camera.ScreenToWorld(900, pos), Is.EqualTo(new Vector2(-9, 22)));
        Assert.That(camera.ScreenToWorld(900, rect), Is.EqualTo(new Rectangle(-6, 25, 6, 7)));
    }


    [Test]
    public void ScreenToWorld_ScaleLessThanOnePosZero_ReturnsCorrecr()
    {
        Point point = new Point(10, 20);
        Rectangle rect = new Rectangle(4, 5, 6, 7);
        camera.Position = Vector2.Zero;

        Assert.That(camera.ScreenToWorld(450, point), Is.EqualTo(new Point(20, 40)));
        Assert.That(camera.ScreenToWorld(450, rect), Is.EqualTo(new Rectangle(8, 10, 12, 14)));
    }
    [Test]
    public void ScreenToWorld_ScaleLessThanOnePosPosXPosY_ReturnsCorrect()
    {
        Point point = new Point(10, 20);
        Rectangle rect = new Rectangle(4, 5, 6, 7);

        camera.Position = new Vector2(10, 20);

        Assert.That(camera.ScreenToWorld(450, point), Is.EqualTo(new Point(30, 60)));
        Assert.That(camera.ScreenToWorld(450, rect), Is.EqualTo(new Rectangle(18, 30, 12, 14)));
    }
    [Test]
    public void ScreenToWorld_ScaleLessThanOnePosPosXNegY_ReturnsCorrect()
    {
        Point point = new Point(10, 20);
        Rectangle rect = new Rectangle(4, 5, 6, 7);

        camera.Position = new Vector2(10, -20);

        Assert.That(camera.ScreenToWorld(450, point), Is.EqualTo(new Point(30, 20)));
        Assert.That(camera.ScreenToWorld(450, rect), Is.EqualTo(new Rectangle(18, -10, 12, 14)));
    }
    [Test]
    public void ScreenToWorld_ScaleLessThanOnePosNegXNegY_ReturnsCorrect()
    {
        Point point = new Point(10, 20);
        Rectangle rect = new Rectangle(4, 5, 6, 7);

        camera.Position = new Vector2(-10, -20);

        Assert.That(camera.ScreenToWorld(450, point), Is.EqualTo(new Point(10, 20)));
        Assert.That(camera.ScreenToWorld(450, rect), Is.EqualTo(new Rectangle(-2, -10, 12, 14)));
    }
    [Test]
    public void ScreenToWorld_ScaleLessThanOnePosNegXPosY_ReturnsCorrect()
    {
        Point point = new Point(10, 20);
        Rectangle rect = new Rectangle(4, 5, 6, 7);

        camera.Position = new Vector2(-10, 20);

        Assert.That(camera.ScreenToWorld(450, point), Is.EqualTo(new Point(10, 60)));
        Assert.That(camera.ScreenToWorld(450, rect), Is.EqualTo(new Rectangle(-2, 30, 12, 14)));
    }


    [Test]
    public void ScreenToWorld_ScaleScaleGreaterThanOnePosZero_ReturnsSame()
    {
        Point point = new Point(10, 20);
        Rectangle rect = new Rectangle(4, 5, 6, 7);
        camera.Position = Vector2.Zero;

        Assert.That(camera.ScreenToWorld(1200, point), Is.EqualTo(new Point(8, 15)));
        Assert.That(camera.ScreenToWorld(1200, rect), Is.EqualTo(new Rectangle(3, 4, 4, 5)));
    }
    [Test]
    public void ScreenToWorld_ScaleScaleGreaterThanOnePosPosXPosY_ReturnsCorrect()
    {
        Point point = new Point(10, 20);
        Rectangle rect = new Rectangle(4, 5, 6, 7);

        camera.Position = new Vector2(10, 20);

        Assert.That(camera.ScreenToWorld(1200, point), Is.EqualTo(new Point(18, 35)));
        Assert.That(camera.ScreenToWorld(1200, rect), Is.EqualTo(new Rectangle(13, 24, 4, 5)));
    }
    [Test]
    public void ScreenToWorld_ScaleScaleGreaterThanOnePosPosXNegY_ReturnsCorrect()
    {
        Point point = new Point(10, 20);
        Rectangle rect = new Rectangle(4, 5, 6, 7);

        camera.Position = new Vector2(10, -20);

        Assert.That(camera.ScreenToWorld(1200, point), Is.EqualTo(new Point(18, -5)));
        Assert.That(camera.ScreenToWorld(1200, rect), Is.EqualTo(new Rectangle(13, -16, 4, 5)));
    }
    [Test]
    public void ScreenToWorld_ScaleScaleGreaterThanOnePosNegXNegY_ReturnsCorrect()
    {
        Point point = new Point(10, 20);
        Rectangle rect = new Rectangle(4, 5, 6, 7);

        camera.Position = new Vector2(-10, -20);

        Assert.That(camera.ScreenToWorld(1200, point), Is.EqualTo(new Point(-2, -5)));
        Assert.That(camera.ScreenToWorld(1200, rect), Is.EqualTo(new Rectangle(-7, -16, 4, 5)));
    }
    [Test]
    public void ScreenToWorld_ScaleScaleGreaterThanOnePosNegXPosY_ReturnsCorrect()
    {
        Point point = new Point(10, 20);
        Rectangle rect = new Rectangle(4, 5, 6, 7);

        camera.Position = new Vector2(-10, 20);

        Assert.That(camera.ScreenToWorld(1200, point), Is.EqualTo(new Point(-2, 35)));
        Assert.That(camera.ScreenToWorld(1200, rect), Is.EqualTo(new Rectangle(-7, 24, 4, 5)));
    }
}
