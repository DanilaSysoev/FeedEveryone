using FeedEveryone.Mono.Control;
using FeedEveryoneMono.Control;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FeedEveryone.Mono.Tests.Control;

public class KeyControllerListener
{
    public int PressCount { get; set; } = 0;
    public int ReleaseCount { get; set; } = 0;
    public int LongPressCount { get; set; } = 0;
    public int PressedCount { get; set; } = 0;
    public int ReleasedCount { get; set; } = 0;

    public void Pressed()
    {
        PressedCount++;
    }
    public void Press()
    {
        PressCount++;
    }
    public void Released()
    {
        ReleasedCount++;
    }
    public void Release()
    {
        ReleaseCount++;
    }
    public void LongPress()
    {
        LongPressCount++;
    }
}

[TestFixture]
public class KeyControllerTests
{
    private const float LongTimePress = 1f;
    private const Keys key = Keys.A;

    private KeyController keyController;
    private IKeyboard keyboard;
    private KeyControllerListener listener;

    private const long startTime = 100 * TimeSpan.TicksPerSecond;
    private const long tickPerFrame = TimeSpan.TicksPerSecond / 100;

    private long totalTicks;

    [SetUp]
    public void Setup()
    {
        keyboard = Substitute.For<IKeyboard>();
        keyController = new KeyController(keyboard, key);
        listener = new KeyControllerListener();
        
        keyController.Pressed += listener.Pressed;
        keyController.Released += listener.Released;
        keyController.Press += listener.Press;
        keyController.Release += listener.Release;
        keyController.LongPresssed += listener.LongPress;

        keyController.LongPressTime = LongTimePress;

        totalTicks = startTime;
    }

    private void Step(long elapsedTicks, KeyState state = KeyState.Down)
    {
        keyboard.GetState(key).Returns(state);
        var gameTime =
            new GameTime(new TimeSpan(totalTicks),
                         new TimeSpan(elapsedTicks));
        keyController.Update(gameTime);
        totalTicks += elapsedTicks;
    }

    [Test]
    public void Update_FirstTimePress_PressCountEqualsOne()
    {
        Step(tickPerFrame);
        Assert.That(listener.PressCount, Is.EqualTo(1));
    }
    [Test]
    public void Update_FirstTimePress_PressedCountEqualsOne()
    {
        Step(tickPerFrame);
        Assert.That(listener.PressedCount,   Is.EqualTo(1));
    }
    [Test]
    public void Update_FirstTimePress_ReleaseCountEqualsZero()
    {
        Step(tickPerFrame);
        Assert.That(listener.ReleaseCount,   Is.EqualTo(0));
    }
    [Test]
    public void Update_FirstTimePress_ReleasedCountEqualsZero()
    {
        Step(tickPerFrame);
        Assert.That(listener.ReleasedCount,  Is.EqualTo(0));
    }
    [Test]
    public void Update_FirstTimePress_LongPressCountEqualsZero()
    {
        Step(tickPerFrame);
        Assert.That(listener.LongPressCount, Is.EqualTo(0));
    }


    [Test]
    public void Update_TwoTimesPressed_PressCountEqualsOne()
    {
        Step(tickPerFrame);
        Step(tickPerFrame);
        Assert.That(listener.PressCount, Is.EqualTo(1));
    }
    [Test]
    public void Update_TwoTimesPressed_PressedCountEqualsTwo()
    {
        Step(tickPerFrame);
        Step(tickPerFrame);
        Assert.That(listener.PressedCount,   Is.EqualTo(2));
    }
    [Test]
    public void Update_TwoTimesPressed_ReleaseCountEqualsZero()
    {
        Step(tickPerFrame);
        Step(tickPerFrame);
        Assert.That(listener.ReleaseCount,   Is.EqualTo(0));
    }
    [Test]
    public void Update_TwoTimesPressed_ReleasedCountEqualsZero()
    {
        Step(tickPerFrame);
        Step(tickPerFrame);
        Assert.That(listener.ReleasedCount,  Is.EqualTo(0));
    }
    [Test]
    public void Update_TwoTimesPressed_LongPressCountEqualsZero()
    {
        Step(tickPerFrame);
        Step(tickPerFrame);
        Assert.That(listener.LongPressCount, Is.EqualTo(0));
    }


    [Test]
    public void Update_ThreeTimesLongTimePressed_PressCountEqualsOne()
    {
        Step(tickPerFrame);
        Step(TimeSpan.TicksPerSecond);
        Step(tickPerFrame);
        Assert.That(listener.PressCount, Is.EqualTo(1));
    }
    [Test]
    public void Update_ThreeTimesLongTimePressed_PressedCountEqualsTwo()
    {
        Step(tickPerFrame);
        Step(TimeSpan.TicksPerSecond);
        Step(tickPerFrame);
        Assert.That(listener.PressedCount,   Is.EqualTo(2));
    }
    [Test]
    public void Update_ThreeTimesLongTimePressed_ReleaseCountEqualsZero()
    {
        Step(tickPerFrame);
        Step(TimeSpan.TicksPerSecond);
        Step(tickPerFrame);
        Assert.That(listener.ReleaseCount,   Is.EqualTo(0));
    }
    [Test]
    public void Update_ThreeTimesLongTimePressed_ReleasedCountEqualsZero()
    {
        Step(tickPerFrame);
        Step(TimeSpan.TicksPerSecond);
        Step(tickPerFrame);
        Assert.That(listener.ReleasedCount,  Is.EqualTo(0));
    }
    [Test]
    public void Update_ThreeTimesLongTimePressed_LongPressCountEqualsOne()
    {
        Step(tickPerFrame);
        Step(TimeSpan.TicksPerSecond);
        Step(tickPerFrame);
        Assert.That(listener.LongPressCount, Is.EqualTo(1));
    }


    [Test]
    public void Update_ThreeTimesPressedThenReleased_PressCountEqualsOne()
    {
        Step(tickPerFrame);
        Step(tickPerFrame);
        Step(tickPerFrame, KeyState.Up);
        Assert.That(listener.PressCount, Is.EqualTo(1));
    }
    [Test]
    public void Update_ThreeTimesPressedThenReleased_PressedCountEqualsTwo()
    {
        Step(tickPerFrame);
        Step(tickPerFrame);
        Step(tickPerFrame, KeyState.Up);
        Assert.That(listener.PressedCount,   Is.EqualTo(2));
    }
    [Test]
    public void Update_ThreeTimesPressedThenReleased_ReleaseCountEqualsOne()
    {
        Step(tickPerFrame);
        Step(tickPerFrame);
        Step(tickPerFrame, KeyState.Up);
        Assert.That(listener.ReleaseCount,   Is.EqualTo(1));
    }
    [Test]
    public void Update_ThreeTimesPressedThenReleased_ReleasedCountEqualsOne()
    {
        Step(tickPerFrame);
        Step(tickPerFrame);
        Step(tickPerFrame, KeyState.Up);
        Assert.That(listener.ReleasedCount,  Is.EqualTo(1));
    }
    [Test]
    public void Update_ThreeTimesPressedThenReleased_LongPressCountEqualsZero()
    {
        Step(tickPerFrame);
        Step(tickPerFrame);
        Step(tickPerFrame, KeyState.Up);
        Assert.That(listener.LongPressCount, Is.EqualTo(0));
    }


    [Test]
    public void Update_ThreeTimesPressedThenTwoReleased_PressCountEqualsOne()
    {
        Step(tickPerFrame);
        Step(tickPerFrame);
        Step(tickPerFrame, KeyState.Up);
        Step(tickPerFrame, KeyState.Up);
        Assert.That(listener.PressCount, Is.EqualTo(1));
    }
    [Test]
    public void Update_ThreeTimesPressedThenTwoReleased_PressedCountEqualsTwo()
    {
        Step(tickPerFrame);
        Step(tickPerFrame);
        Step(tickPerFrame, KeyState.Up);
        Step(tickPerFrame, KeyState.Up);
        Assert.That(listener.PressedCount,   Is.EqualTo(2));
    }
    [Test]
    public void Update_ThreeTimesPressedThenTwoReleased_ReleaseCountEqualsOne()
    {
        Step(tickPerFrame);
        Step(tickPerFrame);
        Step(tickPerFrame, KeyState.Up);
        Step(tickPerFrame, KeyState.Up);
        Assert.That(listener.ReleaseCount,   Is.EqualTo(1));
    }
    [Test]
    public void Update_ThreeTimesPressedThenTwoReleased_ReleasedCountEqualsTwo()
    {
        Step(tickPerFrame);
        Step(tickPerFrame);
        Step(tickPerFrame, KeyState.Up);
        Step(tickPerFrame, KeyState.Up);
        Assert.That(listener.ReleasedCount,  Is.EqualTo(2));
    }
    [Test]
    public void Update_ThreeTimesPressedThenTwoReleased_LongPressCountEqualsZero()
    {
        Step(tickPerFrame);
        Step(tickPerFrame);
        Step(tickPerFrame, KeyState.Up);
        Step(tickPerFrame, KeyState.Up);
        Assert.That(listener.LongPressCount, Is.EqualTo(0));
    }


    [Test]
    public void Update_PressReleasePressRelease_PressCountEqualsTwo()
    {
        Step(tickPerFrame);
        Step(tickPerFrame, KeyState.Up);
        Step(tickPerFrame);
        Step(tickPerFrame, KeyState.Up);
        Assert.That(listener.PressCount, Is.EqualTo(2));
    }
    [Test]
    public void Update_PressReleasePressRelease_PressedCountEqualsTwo()
    {
        Step(tickPerFrame);
        Step(tickPerFrame, KeyState.Up);
        Step(tickPerFrame);
        Step(tickPerFrame, KeyState.Up);
        Assert.That(listener.PressedCount,   Is.EqualTo(2));
    }
    [Test]
    public void Update_PressReleasePressRelease_ReleaseCountEqualsTwo()
    {
        Step(tickPerFrame);
        Step(tickPerFrame, KeyState.Up);
        Step(tickPerFrame);
        Step(tickPerFrame, KeyState.Up);
        Assert.That(listener.ReleaseCount,   Is.EqualTo(2));
    }
    [Test]
    public void Update_PressReleasePressRelease_ReleasedCountEqualsTwo()
    {
        Step(tickPerFrame);
        Step(tickPerFrame, KeyState.Up);
        Step(tickPerFrame);
        Step(tickPerFrame, KeyState.Up);
        Assert.That(listener.ReleasedCount,  Is.EqualTo(2));
    }
    [Test]
    public void Update_PressReleasePressRelease_LongPressCountEqualsZero()
    {
        Step(tickPerFrame);
        Step(tickPerFrame, KeyState.Up);
        Step(tickPerFrame);
        Step(tickPerFrame, KeyState.Up);
        Assert.That(listener.LongPressCount, Is.EqualTo(0));
    }


    [Test]
    public void Update_PressReleasePressReleaseRelease_PressCountEqualsTwo()
    {
        Step(tickPerFrame);
        Step(tickPerFrame, KeyState.Up);
        Step(tickPerFrame);
        Step(tickPerFrame, KeyState.Up);
        Step(tickPerFrame, KeyState.Up);
        Assert.That(listener.PressCount, Is.EqualTo(2));
    }
    [Test]
    public void Update_PressReleasePressReleaseRelease_PressedCountEqualsTwo()
    {
        Step(tickPerFrame);
        Step(tickPerFrame, KeyState.Up);
        Step(tickPerFrame);
        Step(tickPerFrame, KeyState.Up);
        Step(tickPerFrame, KeyState.Up);
        Assert.That(listener.PressedCount,   Is.EqualTo(2));
    }
    [Test]
    public void Update_PressReleasePressReleaseRelease_ReleaseCountEqualsTwo()
    {
        Step(tickPerFrame);
        Step(tickPerFrame, KeyState.Up);
        Step(tickPerFrame);
        Step(tickPerFrame, KeyState.Up);
        Step(tickPerFrame, KeyState.Up);
        Assert.That(listener.ReleaseCount,   Is.EqualTo(2));
    }
    [Test]
    public void Update_PressReleasePressReleaseRelease_ReleasedCountEqualsThree()
    {
        Step(tickPerFrame);
        Step(tickPerFrame, KeyState.Up);
        Step(tickPerFrame);
        Step(tickPerFrame, KeyState.Up);
        Step(tickPerFrame, KeyState.Up);
        Assert.That(listener.ReleasedCount,  Is.EqualTo(3));
    }
    [Test]
    public void Update_PressReleasePressReleaseRelease_LongPressCountEqualsZero()
    {
        Step(tickPerFrame);
        Step(tickPerFrame, KeyState.Up);
        Step(tickPerFrame);
        Step(tickPerFrame, KeyState.Up);
        Step(tickPerFrame, KeyState.Up);
        Assert.That(listener.LongPressCount, Is.EqualTo(0));
    }
}
