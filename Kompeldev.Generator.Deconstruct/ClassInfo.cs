using Microsoft.CodeAnalysis;

namespace Kompeldev.Generator.Deconstruct;

/// <summary>
///     Model class which will contain all information required for the source generator.
/// </summary>
/// <param name="Accessibility">The <see cref="ISymbol.DeclaredAccessibility" /> of the type.</param>
/// <param name="Namespace">The <see cref="ISymbol.ContainingNamespace" /> of the type.</param>
/// <param name="ClassName">The <see cref="ISymbol.Name" /> of the type.</param>
/// <param name="Properties">
///     Collection of property information as tuples of (Name, Type), wrapped in an
///     <see cref="EquatableArray{T}" /> for efficient equality comparison.
/// </param>
public record ClassInfo(
    string Accessibility,
    string Namespace,
    string ClassName,
    EquatableArray<(string Name, string Type)> Properties);