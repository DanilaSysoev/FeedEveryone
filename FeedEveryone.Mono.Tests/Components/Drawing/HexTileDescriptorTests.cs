using System;
using FeedEveryone.Core.Service;
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

        Rectangle dr = td.GetWorldRectangle(0, 0);

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

        Rectangle dr = td.GetWorldRectangle(6, 3);

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

        Rectangle dr = td.GetWorldRectangle(3, 2);

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

        Rectangle dr = td.GetWorldRectangle(4, 1);

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

        Rectangle dr = td.GetWorldRectangle(5, 3);

        Assert.That(dr, Is.EqualTo(new Rectangle(56, 60, 16, 24)));
    }



    [Test]
    public void GetTileAtPoint_EvenLineLeftHalfInside_ReturnsTile()
    {
        HexTileDescriptor td = new HexTileDescriptor
        {
            Height = 48,
            Width = 32
        };
        
        var tilePos = td.GetTileAtPoint(12, 27);
        Assert.That(tilePos, Is.EqualTo(new Position(0, 0)));
    }
    [Test]
    public void GetTileAtPoint_EvenLineRightHalfInside_ReturnsTile()
    {
        HexTileDescriptor td = new HexTileDescriptor
        {
            Height = 48,
            Width = 32
        };
        
        var tilePos = td.GetTileAtPoint(18, 27);
        Assert.That(tilePos, Is.EqualTo(new Position(0, 0)));
    }
    [Test]
    public void GetTileAtPoint_EvenLineLeftHalfOutside_ReturnsTile()
    {
        HexTileDescriptor td = new HexTileDescriptor
        {
            Height = 48,
            Width = 32
        };
        
        var tilePos = td.GetTileAtPoint(2, 20);
        Assert.That(tilePos, Is.EqualTo(new Position(-1, -1)));
    }
    [Test]
    public void GetTileAtPoint_EvenLineRightHalfOutside_ReturnsTile()
    {
        HexTileDescriptor td = new HexTileDescriptor
        {
            Height = 48,
            Width = 32
        };
        
        var tilePos = td.GetTileAtPoint(30, 20);
        Assert.That(tilePos, Is.EqualTo(new Position(-1, 0)));
    }
    [Test]
    public void GetTileAtPoint_EvenLineRightHalfTopRightInside_ReturnsTile()
    {
        HexTileDescriptor td = new HexTileDescriptor
        {
            Height = 48,
            Width = 32
        };
        
        var tilePos = td.GetTileAtPoint(25, 23);
        Assert.That(tilePos, Is.EqualTo(new Position(0, 0)));
    }
    [Test]
    public void GetTileAtPoint_EvenLineLeftHalfTopLeftInside_ReturnsTile()
    {
        HexTileDescriptor td = new HexTileDescriptor
        {
            Height = 48,
            Width = 32
        };
        
        var tilePos = td.GetTileAtPoint(7, 23);
        Assert.That(tilePos, Is.EqualTo(new Position(0, 0)));
    }


    [Test]
    public void GetTileAtPoint_OddLineLeftHalfInside_ReturnsTile()
    {
        HexTileDescriptor td = new HexTileDescriptor
        {
            Height = 48,
            Width = 32
        };
        
        var tilePos = td.GetTileAtPoint(28, 54);
        Assert.That(tilePos, Is.EqualTo(new Position(1, 0)));
    }
    [Test]
    public void GetTileAtPoint_OddLineRightHalfInside_ReturnsTile()
    {
        HexTileDescriptor td = new HexTileDescriptor
        {
            Height = 48,
            Width = 32
        };
        
        var tilePos = td.GetTileAtPoint(34, 51);
        Assert.That(tilePos, Is.EqualTo(new Position(1, 0)));
    }
    [Test]
    public void GetTileAtPoint_OddLineLeftHalfOutside_ReturnsTile()
    {
        HexTileDescriptor td = new HexTileDescriptor
        {
            Height = 48,
            Width = 32
        };
        
        var tilePos = td.GetTileAtPoint(18, 44);
        Assert.That(tilePos, Is.EqualTo(new Position(0, 0)));
    }
    [Test]
    public void GetTileAtPoint_OddLineRightHalfOutside_ReturnsTile()
    {
        HexTileDescriptor td = new HexTileDescriptor
        {
            Height = 48,
            Width = 32
        };
        
        var tilePos = td.GetTileAtPoint(46, 44);
        Assert.That(tilePos, Is.EqualTo(new Position(0, 1)));
    }


    [Test]
    public void GetTileAtPoint_EvenNegativeLineLeftHalfInside_ReturnsTile()
    {
        HexTileDescriptor td = new HexTileDescriptor
        {
            Height = 48,
            Width = 32
        };
        
        var tilePos = td.GetTileAtPoint(-20, -21);
        Assert.That(tilePos, Is.EqualTo(new Position(-2, -1)));
    }
    [Test]
    public void GetTileAtPoint_EvenNegativeLineRightHalfInside_ReturnsTile()
    {
        HexTileDescriptor td = new HexTileDescriptor
        {
            Height = 48,
            Width = 32
        };
        
        var tilePos = td.GetTileAtPoint(-14, -21);
        Assert.That(tilePos, Is.EqualTo(new Position(-2, -1)));
    }
    [Test]
    public void GetTileAtPoint_EvenNegativeLineLeftHalfOutside_ReturnsTile()
    {
        HexTileDescriptor td = new HexTileDescriptor
        {
            Height = 48,
            Width = 32
        };
        
        var tilePos = td.GetTileAtPoint(-30, -28);
        Assert.That(tilePos, Is.EqualTo(new Position(-3, -2)));
    }
    [Test]
    public void GetTileAtPoint_EvenNegativeLineRightHalfOutside_ReturnsTile()
    {
        HexTileDescriptor td = new HexTileDescriptor
        {
            Height = 48,
            Width = 32
        };
        
        var tilePos = td.GetTileAtPoint(-2, -28);
        Assert.That(tilePos, Is.EqualTo(new Position(-3, -1)));
    }


    [Test]
    public void GetTileAtPoint_OddNegativeLineLeftHalfInside_ReturnsTile()
    {
        HexTileDescriptor td = new HexTileDescriptor
        {
            Height = 48,
            Width = 32
        };
        
        var tilePos = td.GetTileAtPoint(-4, 6);
        Assert.That(tilePos, Is.EqualTo(new Position(-1, -1)));
    }
    [Test]
    public void GetTileAtPoint_OddNegativeLineRightHalfInside_ReturnsTile()
    {
        HexTileDescriptor td = new HexTileDescriptor
        {
            Height = 48,
            Width = 32
        };
        
        var tilePos = td.GetTileAtPoint(2, 3);
        Assert.That(tilePos, Is.EqualTo(new Position(-1, -1)));
    }
    [Test]
    public void GetTileAtPoint_OddNegativeLineLeftHalfOutside_ReturnsTile()
    {
        HexTileDescriptor td = new HexTileDescriptor
        {
            Height = 48,
            Width = 32
        };
        
        var tilePos = td.GetTileAtPoint(-14, -4);
        Assert.That(tilePos, Is.EqualTo(new Position(-2, -1)));
    }
    [Test]
    public void GetTileAtPoint_OddNegativeLineRightHalfOutside_ReturnsTile()
    {
        HexTileDescriptor td = new HexTileDescriptor
        {
            Height = 48,
            Width = 32
        };
        
        var tilePos = td.GetTileAtPoint(14, -4);
        Assert.That(tilePos, Is.EqualTo(new Position(-2, 0)));
    }

    [Test]
    public void GetTileAtPoint_RealSize_ReturnsCorrectTile()
    {
        HexTileDescriptor td = new HexTileDescriptor
        {
            Height = 384,
            Width = 256
        };
        
        var tilePos = td.GetTileAtPoint(195, 191);
        Assert.That(tilePos, Is.EqualTo(new Position(0, 0)));
    }
}
