#pragma warning disable NUnit2043 // Use ComparisonConstraint for better assertion messages in case of failure
#pragma warning disable NUnit2010 // Use ComparisonConstraint for better assertion messages in case of failure

using FeedEveryone.Core.Service;

namespace FeedEveryone.Core.Tests.Service;

public class PositionTests
{
    [Test]
    public void CompareTo_CheckLessByLine_ReturnsNegative()
    {
        var p1 = new Position(1, 2);
        var p2 = new Position(2, 3);

        Assert.That(p1.CompareTo(p2), Is.LessThan(0));
    }
    [Test]
    public void CompareTo_CheckLessByColumn_ReturnsNegative()
    {
        var p1 = new Position(2, 2);
        var p2 = new Position(2, 3);

        Assert.That(p1.CompareTo(p2), Is.LessThan(0));
    }
    [Test]
    public void CompareTo_CheckGreaterByLine_ReturnsPositive()
    {
        var p1 = new Position(1, 2);
        var p2 = new Position(2, 3);

        Assert.That(p2.CompareTo(p1), Is.GreaterThan(0));
    }
    [Test]
    public void CompareTo_CheckGreaterByColumn_ReturnsPositive()
    {
        var p1 = new Position(2, 2);
        var p2 = new Position(2, 3);

        Assert.That(p2.CompareTo(p1), Is.GreaterThan(0));
    }
    [Test]
    public void CompareTo_CheckEquals_ReturnsZero()
    {
        var p1 = new Position(2, 3);
        var p2 = new Position(2, 3);

        Assert.That(p2.CompareTo(p1), Is.EqualTo(0));
    }

    [Test]
    public void OperatorLess_CheckLessByLine_ReturnsTrue()
    {
        var p1 = new Position(1, 2);
        var p2 = new Position(2, 3);

        Assert.That(p1 < p2, Is.True);
    }
    [Test]
    public void OperatorLess_CheckLessByColumn_ReturnsTrue()
    {
        var p1 = new Position(2, 2);
        var p2 = new Position(2, 3);

        Assert.That(p1 < p2, Is.True);
    }
    [Test]
    public void OperatorLess_CheckGreaterByLine_ReturnsFalse()
    {
        var p1 = new Position(1, 2);
        var p2 = new Position(2, 3);

        Assert.That(p2 < p1, Is.False);
    }
    [Test]
    public void OperatorLess_CheckGreaterByColumn_ReturnsFalse()
    {
        var p1 = new Position(2, 2);
        var p2 = new Position(2, 3);

        Assert.That(p2 < p1, Is.False);
    }
    [Test]
    public void OperatorLess_CheckEqual_ReturnsFalse()
    {
        var p1 = new Position(2, 2);
        var p2 = new Position(2, 2);

        Assert.That(p1 < p2, Is.False);
    }

    [Test]
    public void OperatorLessOrEqual_CheckLessByLine_ReturnsTrue()
    {
        var p1 = new Position(1, 2);
        var p2 = new Position(2, 3);

        Assert.That(p1 <= p2, Is.True);
    }
    [Test]
    public void OperatorLessOrEqual_CheckLessByColumn_ReturnsTrue()
    {
        var p1 = new Position(2, 2);
        var p2 = new Position(2, 3);

        Assert.That(p1 <= p2, Is.True);
    }
    [Test]
    public void OperatorLessOrEqual_CheckGreaterByLine_ReturnsFalse()
    {
        var p1 = new Position(1, 2);
        var p2 = new Position(2, 3);

        Assert.That(p2 <= p1, Is.False);
    }
    [Test]
    public void OperatorLessOrEqual_CheckGreaterByColumn_ReturnsFalse()
    {
        var p1 = new Position(2, 2);
        var p2 = new Position(2, 3);

        Assert.That(p2 <= p1, Is.False);
    }
    [Test]
    public void OperatorLessOrEqual_CheckEqual_ReturnsTrue()
    {
        var p1 = new Position(2, 2);
        var p2 = new Position(2, 2);

        Assert.That(p1 <= p2, Is.True);
    }

    [Test]
    public void OperatorGreater_CheckLessByLine_ReturnsFalse()
    {
        var p1 = new Position(1, 2);
        var p2 = new Position(2, 3);

        Assert.That(p1 > p2, Is.False);
    }
    [Test]
    public void OperatorGreater_CheckLessByColumn_ReturnsFalse()
    {
        var p1 = new Position(2, 2);
        var p2 = new Position(2, 3);

        Assert.That(p1 > p2, Is.False);
    }
    [Test]
    public void OperatorGreater_CheckGreaterByLine_ReturnsTrue()
    {
        var p1 = new Position(1, 2);
        var p2 = new Position(2, 3);

        Assert.That(p2 > p1, Is.True);
    }
    [Test]
    public void OperatorGreater_CheckGreaterByColumn_ReturnsTrue()
    {
        var p1 = new Position(2, 2);
        var p2 = new Position(2, 3);

        Assert.That(p2 > p1, Is.True);
    }
    [Test]
    public void OperatorGreater_CheckEqual_ReturnsFalse()
    {
        var p1 = new Position(2, 2);
        var p2 = new Position(2, 2);

        Assert.That(p1 > p2, Is.False);
    }

    [Test]
    public void OperatorGreaterOrEqual_CheckLessByLine_ReturnsFalse()
    {
        var p1 = new Position(1, 2);
        var p2 = new Position(2, 3);

        Assert.That(p1 >= p2, Is.False);
    }
    [Test]
    public void OperatorGreaterOrEqual_CheckLessByColumn_ReturnsFalse()
    {
        var p1 = new Position(2, 2);
        var p2 = new Position(2, 3);

        Assert.That(p1 >= p2, Is.False);
    }
    [Test]
    public void OperatorGreaterOrEqual_CheckGreaterByLine_ReturnsTrue()
    {
        var p1 = new Position(1, 2);
        var p2 = new Position(2, 3);

        Assert.That(p2 >= p1, Is.True);
    }
    [Test]
    public void OperatorGreaterOrEqual_CheckGreaterByColumn_ReturnsTrue()
    {
        var p1 = new Position(2, 2);
        var p2 = new Position(2, 3);

        Assert.That(p2 >= p1, Is.True);
    }
    [Test]
    public void OperatorGreaterOrEqual_CheckEqual_ReturnsTrue()
    {
        var p1 = new Position(2, 2);
        var p2 = new Position(2, 2);

        Assert.That(p1 >= p2, Is.True);
    }

    [Test]
    public void OperatorEqual_CheckLessByLine_ReturnsFalse()
    {
        var p1 = new Position(1, 2);
        var p2 = new Position(2, 3);

        Assert.That(p1 == p2, Is.False);
    }
    [Test]
    public void OperatorEqual_CheckLessByColumn_ReturnsFalse()
    {
        var p1 = new Position(2, 2);
        var p2 = new Position(2, 3);

        Assert.That(p1 == p2, Is.False);
    }
    [Test]
    public void OperatorEqual_CheckGreaterByLine_ReturnsFalse()
    {
        var p1 = new Position(1, 2);
        var p2 = new Position(2, 3);

        Assert.That(p2 == p1, Is.False);
    }
    [Test]
    public void OperatorEqual_CheckGreaterByColumn_ReturnsFalse()
    {
        var p1 = new Position(2, 2);
        var p2 = new Position(2, 3);

        Assert.That(p2 == p1, Is.False);
    }
    [Test]
    public void OperatorEqual_CheckEqual_ReturnsTrue()
    {
        var p1 = new Position(2, 2);
        var p2 = new Position(2, 2);

        Assert.That(p1 == p2, Is.True);
    }

    [Test]
    public void OperatorNotEqual_CheckLessByLine_ReturnsTrue()
    {
        var p1 = new Position(1, 2);
        var p2 = new Position(2, 3);

        Assert.That(p1 != p2, Is.True);
    }
    [Test]
    public void OperatorNotEqual_CheckLessByColumn_ReturnsTrue()
    {
        var p1 = new Position(2, 2);
        var p2 = new Position(2, 3);

        Assert.That(p1 != p2, Is.True);
    }
    [Test]
    public void OperatorNotEqual_CheckGreaterByLine_ReturnsTrue()
    {
        var p1 = new Position(1, 2);
        var p2 = new Position(2, 3);

        Assert.That(p2 != p1, Is.True);
    }
    [Test]
    public void OperatorNotEqual_CheckGreaterByColumn_ReturnsTrue()
    {
        var p1 = new Position(2, 2);
        var p2 = new Position(2, 3);

        Assert.That(p2 != p1, Is.True);
    }
    [Test]
    public void OperatorNotEqual_CheckEqual_ReturnsFalse()
    {
        var p1 = new Position(2, 2);
        var p2 = new Position(2, 2);

        Assert.That(p1 != p2, Is.False);
    }
}