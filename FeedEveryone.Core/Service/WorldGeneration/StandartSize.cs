
namespace FeedEveryone.Core.Service.WorldGeneration;

public struct StandartSize : IComparable<StandartSize>
{
    public int Value { get; private set; }

    public StandartSize(int size)
    {
        int pow = 2;
        while (pow + 1 < size)
            pow <<= 1;
        Value = pow + 1;
    }

    public static implicit operator int(StandartSize s) => s.Value;
    public static implicit operator StandartSize(int v) => new StandartSize(v);

    public int CompareTo(StandartSize other)
    {
        return Value.CompareTo(other.Value);
    }
    public override string ToString()
    {
        return Value.ToString();
    }
    public override bool Equals(object? obj)
    {
        if (obj is StandartSize size)
            return Value == size.Value;
        return false;
    }
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static bool operator ==(StandartSize left, StandartSize right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(StandartSize left, StandartSize right)
    {
        return !(left == right);
    }

    public static bool operator <(StandartSize left, StandartSize right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator <=(StandartSize left, StandartSize right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >(StandartSize left, StandartSize right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator >=(StandartSize left, StandartSize right)
    {
        return left.CompareTo(right) >= 0;
    }

    public static int operator /(StandartSize left, StandartSize right)
    {
        return left.Value / right.Value;
    }
    public static int operator %(StandartSize left, StandartSize right)
    {
        return left.Value % right.Value;
    }
    public static int operator /(StandartSize left, int right)
    {
        return left.Value / right;
    }
    public static int operator %(StandartSize left, int right)
    {
        return left.Value % right;
    }
    public static int operator /(int left, StandartSize right)
    {
        return left / right.Value;
    }
    public static int operator %(int left, StandartSize right)
    {
        return left % right.Value;
    }
}
