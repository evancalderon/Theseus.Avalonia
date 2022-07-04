namespace APIWriter;

public record TypeDescription(
    string Type,
    bool Nullable,
    int ArrayDepth);

public record Description(
    Dictionary<string, TypeDescription> Properties);

public record Enumeration(
    List<string> Values);

public record Method(
    string method,
    Dictionary<string, TypeDescription> Parameters);
    