using CsvHelper.Configuration.Attributes;

namespace TraktToLetterboxdCsv;

internal record TraktWatch
{
    [Name("imdb_id")]
    public required string ImdbId { get; set; }
    [Name("title")]
    public required string Title { get; set; }
    [Name("year")]
    public required int ReleaseYear { get; set; }
    [Name("watched_at")]
    public required DateTime WatchDate { get; set; }
}
