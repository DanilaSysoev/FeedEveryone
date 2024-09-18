using FeedEveryone.Mono.Components.Service;
using FeedEveryone.Mono.Components.TileProcessing;
using Microsoft.Xna.Framework;

namespace FeedEveryone.Mono.Tests.Service;

public class MapDrawingAreaSelectorTests
{
    [Test]
    public void GetDrawingArea_CameraIsEqualTile_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            Vector2.Zero,
            1000,
            500,
            new WorldUnitSize { Height = 1000, Width = 500 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(0, 0, 1, 1)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualTileWithAdditional_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            Vector2.Zero,
            1000,
            500,
            new WorldUnitSize { Height = 1000, Width = 500 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(-1, -1, 3, 3)));
    }

    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTile_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            Vector2.Zero,
            800,
            750,
            new WorldUnitSize { Height = 400, Width = 250 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(0, 0, 3, 3)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTileWithAdditional_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            Vector2.Zero,
            800,
            750,
            new WorldUnitSize { Height = 400, Width = 250 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(-1, -1, 5, 5)));
    }


    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosXNegLessThanHalf_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(-100, 0),
            800,
            750,
            new WorldUnitSize { Height = 400, Width = 250 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(0, 0, 3, 3)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosXNegLessThanHalfWithAdditional_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(-100, 0),
            800,
            750,
            new WorldUnitSize { Height = 400, Width = 250 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(-1, -1, 5, 5)));
    }

    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosXNegHalf_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(-125, 0),
            800,
            750,
            new WorldUnitSize { Height = 400, Width = 250 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(0, 0, 3, 3)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosXNegHalfWithAdditional_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(-125, 0),
            800,
            750,
            new WorldUnitSize { Height = 400, Width = 250 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(-1, -1, 5, 5)));
    }

    
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosXNegMoreThenHalf_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(-225, 0),
            800,
            750,
            new WorldUnitSize { Height = 400, Width = 250 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(0, 0, 3, 3)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosXNegMoreThenHalfWithAdditional_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(-225, 0),
            800,
            750,
            new WorldUnitSize { Height = 400, Width = 250 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(-1, -1, 5, 5)));
    }
        
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosXNegUnit_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(-250, 0),
            800,
            750,
            new WorldUnitSize { Height = 400, Width = 250 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(-1, 0, 3, 3)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosXNegUnitWithAdditional_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(-250, 0),
            800,
            750,
            new WorldUnitSize { Height = 400, Width = 250 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(-2, -1, 5, 5)));
    }
        
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosXNegMoreThenUnit_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(-260, 0),
            800,
            750,
            new WorldUnitSize { Height = 400, Width = 250 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(-1, 0, 3, 3)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosXNegMoreThenUnitWithAdditional_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(-260, 0),
            800,
            750,
            new WorldUnitSize { Height = 400, Width = 250 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(-2, -1, 5, 5)));
    }



    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosXPosLessThanHalf_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(100, 0),
            800,
            750,
            new WorldUnitSize { Height = 400, Width = 250 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(1, 0, 3, 3)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosXPosLessThanHalfWithAdditional_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(100, 0),
            800,
            750,
            new WorldUnitSize { Height = 400, Width = 250 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(0, -1, 5, 5)));
    }

    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosXPosHalf_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(125, 0),
            800,
            750,
            new WorldUnitSize { Height = 400, Width = 250 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(1, 0, 3, 3)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosXPosHalfWithAdditional_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(125, 0),
            800,
            750,
            new WorldUnitSize { Height = 400, Width = 250 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(0, -1, 5, 5)));
    }

    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosXPosMoreThenHalf_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(225, 0),
            800,
            750,
            new WorldUnitSize { Height = 400, Width = 250 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(1, 0, 3, 3)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosXPosMoreThenHalfWithAdditional_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(225, 0),
            800,
            750,
            new WorldUnitSize { Height = 400, Width = 250 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(0, -1, 5, 5)));
    }
        
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosXPosUnit_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(250, 0),
            800,
            750,
            new WorldUnitSize { Height = 400, Width = 250 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(1, 0, 3, 3)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosXPosUnitWithAdditional_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(250, 0),
            800,
            750,
            new WorldUnitSize { Height = 400, Width = 250 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(0, -1, 5, 5)));
    }
        
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosXPosMoreThenUnit_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(260, 0),
            800,
            750,
            new WorldUnitSize { Height = 400, Width = 250 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(2, 0, 3, 3)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosXPosMoreThenUnitWithAdditional_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(260, 0),
            800,
            750,
            new WorldUnitSize { Height = 400, Width = 250 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(1, -1, 5, 5)));
    }




    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosYPosLessThanQuarterWidth_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, 50),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(0, 1, 3, 3)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosYPosLessThanQuarterWidthWithAdditional_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, 50),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(-1, 0, 5, 5)));
    }

    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosYPosQuarterWidth_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, 60),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(0, 1, 3, 3)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosYPosQuarterWidthWithAdditional_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, 60),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(-1, 0, 5, 5)));
    }

    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosYPosLessThanTwoQuarterWidth_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, 90),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(0, 1, 3, 3)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosYPosLessThanTwoQuarterWidthWithAdditional_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, 90),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(-1, 0, 5, 5)));
    }

    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosYPosTwoQuarterWidth_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, 120),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(0, 1, 3, 3)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosYPosTwoQuarterWidthWithAdditional_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, 120),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(-1, 0, 5, 5)));
    }

    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosYPosLessThanThreeQuarterWidth_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, 130),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(0, 1, 3, 3)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosYPosLessThanThreeQuarterWidthWithAdditional_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, 130),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(-1, 0, 5, 5)));
    }

    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosYPosThreeQuarterWidth_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, 180),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(0, 1, 3, 3)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosYPosThreeQuarterWidthWithAdditional_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, 180),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(-1, 0, 5, 5)));
    }

    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosYPosLessThanFourQuarterWidth_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, 200),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(0, 2, 3, 3)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosYPosLessThanFourQuarterWidthWithAdditional_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, 200),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(-1, 1, 5, 5)));
    }

    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosYPosFourQuarterWidth_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, 240),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(0, 2, 3, 3)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosYPosFourQuarterWidthWithAdditional_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, 240),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(-1, 1, 5, 5)));
    }

    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosYPosLessThanFourQuarterWidth2_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, 200),
            1000,
            720,
            new WorldUnitSize { Height = 500, Width = 240 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(0, 2, 3, 4)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosYPosLessThanFourQuarterWidthWithAdditional2_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, 200),
            1000,
            720,
            new WorldUnitSize { Height = 500, Width = 240 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(-1, 1, 5, 6)));
    }

    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosYPosFourQuarterWidth2_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, 240),
            1000,
            720,
            new WorldUnitSize { Height = 500, Width = 240 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(0, 2, 3, 4)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosYPosfourQuarterWidthWithAdditional2_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, 240),
            1000,
            720,
            new WorldUnitSize { Height = 500, Width = 240 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(-1, 1, 5, 6)));
    }



    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosYPosMoreThanFourQuarterWidth2_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, 260),
            1000,
            720,
            new WorldUnitSize { Height = 500, Width = 240 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(0, 2, 3, 4)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTilePosYPosMoreThanFourQuarterWidthWithAdditional2_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, 260),
            1000,
            720,
            new WorldUnitSize { Height = 500, Width = 240 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(-1, 1, 5, 6)));
    }





    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTileNegYPosLessThanQuarterWidth_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, -50),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(0, 0, 3, 3)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTileNegYPosLessThanQuarterWidthWithAdditional_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, -50),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(-1, -1, 5, 5)));
    }

    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTileNegYPosQuarterWidth_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, -60),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(0, 0, 3, 3)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTileNegYPosQuarterWidthWithAdditional_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, -60),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(-1, -1, 5, 5)));
    }

    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTileNegYPosLessThanTwoQuarterWidth_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, -90),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(0, 0, 3, 3)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTileNegYPosLessThanTwoQuarterWidthWithAdditional_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, -90),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(-1, -1, 5, 5)));
    }

    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTileNegYPosTwoQuarterWidth_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, -120),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(0, 0, 3, 3)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTileNegYPosTwoQuarterWidthWithAdditional_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, -120),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(-1, -1, 5, 5)));
    }

    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTileNegYPosLessThanThreeQuarterWidth_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, -130),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(0, 0, 3, 3)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTileNegYPosLessThanThreeQuarterWidthWithAdditional_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, -130),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(-1, -1, 5, 5)));
    }

    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTileNegYPosThreeQuarterWidth_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, -180),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(0, -1, 3, 3)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTileNegYPosThreeQuarterWidthWithAdditional_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, -180),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(-1, -2, 5, 5)));
    }

    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTileNegYPosLessThanFourQuarterWidth_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, -200),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(0, -1, 3, 3)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTileNegYPosLessThanFourQuarterWidthWithAdditional_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, -200),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(-1, -2, 5, 5)));
    }

    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTileNegYPosFourQuarterWidth_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, -240),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(0, -1, 3, 3)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTileNegYPosFourQuarterWidthWithAdditional_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, -240),
            800,
            720,
            new WorldUnitSize { Height = 400, Width = 240 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(-1, -2, 5, 5)));
    }

    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTileNegYPosLessThanFourQuarterWidth2_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, -200),
            1000,
            720,
            new WorldUnitSize { Height = 500, Width = 240 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(0, -1, 3, 4)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTileNegYPosLessThanFourQuarterWidthWithAdditional2_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, -200),
            1000,
            720,
            new WorldUnitSize { Height = 500, Width = 240 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(-1, -2, 5, 6)));
    }

    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTileNegYPosFourQuarterWidth2_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, -240),
            1000,
            720,
            new WorldUnitSize { Height = 500, Width = 240 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(0, -1, 3, 4)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTileNegYPosfourQuarterWidthWithAdditional2_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, -240),
            1000,
            720,
            new WorldUnitSize { Height = 500, Width = 240 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(-1, -2, 5, 6)));
    }



    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTileNegYPosMoreThanFourQuarterWidth2_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, -260),
            1000,
            720,
            new WorldUnitSize { Height = 500, Width = 240 },
            0
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(0, -1, 3, 4)));
    }
    [Test]
    public void GetDrawingArea_CameraIsEqualThreeTileNegYPosMoreThanFourQuarterWidthWithAdditional2_ReturnCorrectArea()
    {
        var selector = new MapDrawingAreaSelector(
            new Vector2(0, -260),
            1000,
            720,
            new WorldUnitSize { Height = 500, Width = 240 },
            2
        );
        Assert.That(selector.GetDrawingArea(), Is.EqualTo(new Rectangle(-1, -2, 5, 6)));
    }
}
