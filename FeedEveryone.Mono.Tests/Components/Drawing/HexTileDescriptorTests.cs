using System;
using FeedEveryone.Mono.Components.Drawing;
using FeedEveryone.Mono.Components.TileProcessing;
using Microsoft.Xna.Framework;

namespace FeedEveryone.Mono.Tests.Components.Drawing;

public class HexTileDescriptorTests
{
    [Test]
    public void GetWorldDrawingRectangle_ZeroPos_ReturnsCorrect()
    {
        HexTileDescriptor td = new HexTileDescriptor
        {
            Height = 24,
            Width = 16
        };

        Rectangle dr = td.GetWorldDrawingRectangle(0, 0);

        Assert.That(dr, Is.EqualTo(new Rectangle(0, 0, 16, 24)));
    }
    [Test]
    public void GetWorldDrawingRectangle_EvenYnotZeroXPos_ReturnsCorrect()
    {
        HexTileDescriptor td = new HexTileDescriptor
        {
            Height = 24,
            Width = 16
        };

        Rectangle dr = td.GetWorldDrawingRectangle(6, 3);

        Assert.That(dr, Is.EqualTo(new Rectangle(48, 72, 16, 24)));
    }
    [Test]
    public void GetWorldDrawingRectangle_OddYnotZeroXPos_ReturnsCorrect()
    {
        HexTileDescriptor td = new HexTileDescriptor
        {
            Height = 24,
            Width = 16
        };

        Rectangle dr = td.GetWorldDrawingRectangle(3, 2);

        Assert.That(dr, Is.EqualTo(new Rectangle(40, 36, 16, 24)));
    }
    [Test]
    public void GetWorldDrawingRectangle_EvenYnotZeroXPos2_ReturnsCorrect()
    {
        HexTileDescriptor td = new HexTileDescriptor
        {
            Height = 24,
            Width = 16
        };

        Rectangle dr = td.GetWorldDrawingRectangle(4, 1);

        Assert.That(dr, Is.EqualTo(new Rectangle(16, 48, 16, 24)));
    }
    [Test]
    public void GetWorldDrawingRectangle_OddYnotZeroXPos2_ReturnsCorrect()
    {
        HexTileDescriptor td = new HexTileDescriptor
        {
            Height = 24,
            Width = 16
        };

        Rectangle dr = td.GetWorldDrawingRectangle(5, 3);

        Assert.That(dr, Is.EqualTo(new Rectangle(56, 60, 16, 24)));
    }
}
