namespace APIWriter;

public record Property(
    string Name,
    string Type,
    bool Nullable,
    bool Array);

public record Descriptor(
    string Name,
    List<Property> Properties);

public record Enumerator(
    string Name,
    List<string> Values);

public record Method;
    