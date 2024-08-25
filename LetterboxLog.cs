using CsvHelper.Configuration.Attributes;

namespace TraktToLetterboxdCsv;

/// <summary>
/// https://letterboxd.com/about/importing-data/
/// </summary>
public record LetterboxLog
{
    [Name("imdbID")]
    public required string ImdbId { get; init; }
    [Name("WatchedDate")]
    public required string WatchedDate { get; init; }
    [Name("Rewatch")]
    public required bool IsRewatch { get; set; }
    [Name("Rating10")]
    public int? Rating { get; set; }
}