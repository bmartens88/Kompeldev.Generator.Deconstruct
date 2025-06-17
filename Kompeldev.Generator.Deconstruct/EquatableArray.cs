using System.Collections;

namespace Kompeldev.Generator.Deconstruct;

/// <summary>
///     Class which wraps an array and implements <see cref="IEquatable{T}" /> and <see cref="IReadOnlyCollection{T}" />.
/// </summary>
/// <param name="array">Array of <typeparamref name="T" /> to wrap.</param>
/// <typeparam name="T">The type of the element in the wrapped array.</typeparam>
public readonly struct EquatableArray<T>(T[] array) : IEquatable<EquatableArray<T>>, IReadOnlyCollection<T>
    where T : IEquatable<T>
{
    private readonly T[]? _array = array;

    /// <summary>
    ///     Indexer to access elements in the wrapped array.
    /// </summary>
    /// <param name="index">The index for which to get the array element.</param>
    /// <exception cref="IndexOutOfRangeException">
    ///     Thrown when the array is null or the index is out of bounds of the array.
    /// </exception>
    public T this[int index]
    {
        get
        {
            if (_array is null) throw new IndexOutOfRangeException();
            return _array[index];
        }
    }

    /// <inheritdoc />
    public bool Equals(EquatableArray<T> other)
    {
        return AsSpan().SequenceEqual(other.AsSpan());
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is EquatableArray<T> array && Equals(array);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        if (_array is not { } array) return 0;

        return _array.Select(x => x.GetHashCode())
            .Aggregate((x, y) => x ^ y);
    }

    /// <inheritdoc />
    public IEnumerator<T> GetEnumerator()
    {
        return ((IEnumerable<T>)(_array ?? [])).GetEnumerator();
    }

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<T>)(_array ?? [])).GetEnumerator();
    }

    /// <summary>
    ///     Returns the underlying array as a <see cref="ReadOnlySpan{T}" />.
    /// </summary>
    /// <returns>
    ///     A read-only span of the elements in the wrapped array.
    /// </returns>
    private ReadOnlySpan<T> AsSpan()
    {
        return _array.AsSpan();
    }

    /// <inheritdoc />
    public int Count => _array?.Length ?? 0;

    /// <summary>
    ///     Determines whether two <see cref="EquatableArray{T}" /> instances are equal.
    /// </summary>
    /// <param name="left">The first array to compare.</param>
    /// <param name="right">The second array to compare.</param>
    /// <returns>
    ///     <c>true</c> if the arrays contain the same elements in the same order; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    ///     Equality is determined by comparing each element in the arrays using their <see cref="IEquatable{T}.Equals(T)" />
    ///     implementation.
    /// </remarks>
    public static bool operator ==(EquatableArray<T> left, EquatableArray<T> right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///     Determines whether two <see cref="EquatableArray{T}" /> instances are not equal.
    /// </summary>
    /// <param name="left">The first array to compare.</param>
    /// <param name="right">The second array to compare.</param>
    /// <returns>
    ///     <c>true</c> if the arrays do not contain the same elements in the same order; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    ///     Equality is determined by comparing each element in the arrays using their <see cref="IEquatable{T}.Equals(T)" />
    ///     implementation.
    /// </remarks>
    public static bool operator !=(EquatableArray<T> left, EquatableArray<T> right)
    {
        return !left.Equals(right);
    }
}