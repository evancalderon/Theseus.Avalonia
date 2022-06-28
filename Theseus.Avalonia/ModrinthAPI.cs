using System.ComponentModel.DataAnnotations;
using Microsoft.CodeAnalysis;

namespace Theseus.Avalonia;

public class ModrinthAPI
{
}

public enum Support
{
    required,
    optional,
    unsupported
}

public enum ProjectType
{
    mod,
    modpack
}

public enum Status
{
    approved,
    rejected,
    draft,
    unlisted,
    archived,
    processing,
    unknown
}

public enum DependencyType
{
    required,
    optional,
    incompatible,
    embedded
}

public enum VersionType
{
    release,
    beta,
    alpha
}

public record DonationURL(
    string id,
    string platform,
    string url);

public record License(
    string id,
    string name);

public record GalleryImage(
    string url,
    bool featured,
    string? title,
    string? description,
    string created);

public record Project(
    string slug, string title, string description,
    string[] categories,
    Support client_side, Support server_side,
    string body,
    string? issues_url, string? source_url, string? wiki_url, string? discord_url,
    string[]? donation_urls,
    ProjectType project_type,
    int downloads,
    string? icon_url,
    string id, string team,
    string? moderator_message,
    string published, string updated,
    int followers,
    Status status,
    License license,
    string[] versions,
    GalleryImage[]? gallery);

public record SearchResult(
    string slug, string title, string description,
    string[] categories,
    Support client_side, Support server_side,
    ProjectType project_type,
    int downloads,
    string? icon_url,
    string project_id,
    string author,
    string[] versions,
    int follows,
    string date_created,
    string date_modified,
    string latest_version,
    string license,
    string[] gallery);

public record Dependency(
    string? version_id,
    string? project_id,
    DependencyType dependency_type);

public record Hashes(
    string sha512,
    string sha1);

public record File(
    Hashes hashes,
    string url,
    string filename,
    bool primary,
    int size);

public record Version(
    string name,
    string version_number,
    string? changelog,
    Dependency[]? dependencies,
    string[] game_versions,
    VersionType version_type,
    string[] loaders,
    bool featured,
    string id,
    string project_id,
    string author_id,
    string date_published,
    int downloads,
    File[] files);
    