using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using TraktToLetterboxdCsv;

// Read Trakt CSV
using var reader = new StreamReader("trakt-movies.csv");
using var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
var traktWatches = csvReader.GetRecords<TraktWatch>().OrderBy(x => x.WatchDate).ToList();

// Process Trakt CSV
using var writer = new StreamWriter("letterboxd-logs.csv");
using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);

csvWriter.WriteHeader<LetterboxLog>();
await csvWriter.NextRecordAsync();

foreach (var movieGroup in traktWatches.GroupBy(x => x.ImdbId))
{
    var logsForMovie = movieGroup
        .OrderBy(x => x.WatchDate)
        .Select((x, idx) => Convert(x, idx != 0));
    foreach (var log in logsForMovie)
    {
        csvWriter.WriteRecord(log);
        await csvWriter.NextRecordAsync();
    }
}

static LetterboxLog Convert(TraktWatch watch, bool IsRewatch)
{
    var rating = default(int?);
    if (watch.WatchDate.Year == DateTime.Now.Year)
    {
        Console.Write($"\nRate {watch.Title} ({watch.ReleaseYear}) watched on {watch.WatchDate:F} from 1-10: ");
        var ratingStr = Console.ReadLine();
        rating = int.Parse(ratingStr ?? "");
        if (rating < 1 || rating > 10)
        {
            throw new Exception($"{rating} must be between 1-10");
        }
    }

    return new LetterboxLog 
    {
        ImdbId = watch.ImdbId,
        WatchedDate = watch.WatchDate.ToString("yyyy-MM-dd"),
        IsRewatch = IsRewatch,
        Rating = rating,
    };
}