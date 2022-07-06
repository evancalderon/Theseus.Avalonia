namespace APIWriter;

public record TypeDescription(
    string TypeName,
    bool Nullable,
    int ArrayDepth);

public record Description(
    Dictionary<string, TypeDescription> Properties);

public record Enumeration(
    Dictionary<string, string> Values);

public record MethodParameter(
    string Kind,
    TypeDescription Type);

public record Method(
    string Kind,
    string Endpoint,
    Dictionary<string, MethodParameter> Parameters);