namespace APIWriter;

public static class Extensions
{
    public static Dictionary<TKey, TValue> Merge<TKey, TValue>(
        params Dictionary<TKey, TValue>[] dictionaries)
        where TKey : notnull
    {
        return dictionaries
            .SelectMany(d => d)
            .ToDictionary(p => p.Key, p => p.Value);
    }
    
    public static void AddRange<TKey, TValue>(
        this Dictionary<TKey, TValue> dict,
        params Dictionary<TKey, TValue>[] dictionaries)
        where TKey : notnull
    {
        dictionaries
            .Append(dict)
            .SelectMany(d => d)
            .ToList().ForEach(d => dict.TryAdd(d.Key, d.Value));
    }
}