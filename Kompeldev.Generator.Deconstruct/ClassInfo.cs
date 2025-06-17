namespace Kompeldev.Generator.Deconstruct;

public record ClassInfo(
    string Accessibility,
    string Namespace,
    string ClassName,
    EquatableArray<(string Name, string Type)> Properties);