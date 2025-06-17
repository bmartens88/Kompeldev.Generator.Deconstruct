using System.Collections;

namespace Kompeldev.Generator.Deconstruct;

public readonly struct EquatableArray<T>(T[] array) : IEquatable<EquatableArray<T>>, IReadOnlyCollection<T>
    where T : IEquatable<T>
{
    private readonly T[]? _array = array;

    public T this[int index]
    {
        get
        {
            if (_array is null) throw new IndexOutOfRangeException();
            return _array[index];
        }
    }

    public bool Equals(EquatableArray<T> other)
    {
        return AsSpan().SequenceEqual(other.AsSpan());
    }

    public override bool Equals(object? obj)
    {
        return obj is EquatableArray<T> array && Equals(array);
    }

    public override int GetHashCode()
    {
        if (_array is not { } array) return 0;

        return _array.Select(x => x.GetHashCode())
            .Aggregate((x, y) => x ^ y);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return ((IEnumerable<T>)(_array ?? [])).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<T>)(_array ?? [])).GetEnumerator();
    }

    public ReadOnlySpan<T> AsSpan()
    {
        return _array.AsSpan();
    }

    public int Count => _array?.Length ?? 0;

    public static bool operator ==(EquatableArray<T> left, EquatableArray<T> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(EquatableArray<T> left, EquatableArray<T> right)
    {
        return !left.Equals(right);
    }
}