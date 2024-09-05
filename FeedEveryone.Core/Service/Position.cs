namespace FeedEveryone.Core.Service;

public class Position : IComparable<Position>
{
    public int Line { get; private set; }
    public int Column { get; private set; }

    public Position(int line, int column)
    {
        Line = line;
        Column = column;
    }

    public int CompareTo(Position? other)
    {
        if (other is null)
            return 1;

        if (Line != other.Line)
            return Line.CompareTo(other.Line);

        return Column.CompareTo(other.Column);
    }

    public override bool Equals(object? obj)
    {
        return (obj is Position pos) &&
               pos.Line == Line &&
               pos.Column == Column;
    }

    public override int GetHashCode()
    {
        return System.HashCode.Combine(Line, Column);
    }

    public override string? ToString()
    {
        return $"[{Line}, {Column}]";
    }

    public static bool operator <(Position left, Position right)
    {
        return left.CompareTo(right) < 0;
    }
    public static bool operator <=(Position left, Position right)
    {
        return left.CompareTo(right) <= 0;
    }
    public static bool operator >(Position left, Position right)
    {
        return left.CompareTo(right) > 0;
    }
    public static bool operator >=(Position left, Position right)
    {
        return left.CompareTo(right) >= 0;
    }
    public static bool operator ==(Position left, Position right)
    {
        return left.CompareTo(right) == 0;
    }
    public static bool operator !=(Position left, Position right)
    {
        return left.CompareTo(right) != 0;
    }
}