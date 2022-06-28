using System.Net;
using System.Text.RegularExpressions;
using Microsoft.OpenApi.Readers;

WebClient web = new();
var apispec = web.DownloadString("https://raw.githubusercontent.com/modrinth/docs/master/static/openapi.yaml");
apispec = Regex.Replace(apispec, @"(?<=openapi: )'3\.1\.0'", "'3.0'");
apispec = Regex.Replace(apispec, @"(?<!"")https?:.*?(?=[,\]])", @"""$0""");
File.WriteAllText("modified.txt", apispec);

var doc = new OpenApiStringReader(new OpenApiReaderSettings()).Read(apispec, out var diag);

foreach (var err in diag.Errors)
{
    Console.WriteLine(err);
}
foreach (var warn in diag.Warnings)
{
    Console.WriteLine(warn);
}
