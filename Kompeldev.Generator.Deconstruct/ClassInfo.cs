namespace Kompeldev.Generator.Deconstruct;

public record ClassInfo(string Namespace, string ClassName, (string Name, string Type)[] Properties);