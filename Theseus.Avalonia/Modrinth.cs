using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace Theseus.Avalonia;

internal static class Base
{
    public static readonly Uri Url = new Uri("https://api.modrinth.com/");
}

public static class Modrinth
{
    private static readonly HttpClient web = new();

    public static async Task<dynamic?> SearchAsync(
        string? query = null,
        string[]? facets = null,
        string? index = null,
        int? offset = null,
        int? limit = null)
    {
        var parameters = HttpUtility.ParseQueryString("");

        if (query is not null)
        {
            parameters["query"] = query;
        }

        if (facets is { Length: > 0 })
        {
            parameters["facets"] = $@"[[""{string.Join(@"""],[""", facets)}""]]";
        }

        if (index is not null)
        {
            parameters["index"] = index;
        }

        if (offset is not null)
        {
            parameters["offset"] = offset.ToString();
        }

        if (limit is not null)
        {
            parameters["limit"] = limit.ToString();
        }

        var uri = new UriBuilder(Base.Url)
        {
            Path = "v2/search",
            Query = parameters.ToString()
        };

        await Console.Out.WriteLineAsync(uri.Uri.ToString());
        var result = await web.GetStringAsync(uri.Uri);
        await Console.Out.WriteLineAsync(result);
        return JsonConvert.DeserializeObject(result);
    }
}