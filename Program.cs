using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using TraktToLetterboxdCsv;

// https://letterboxd.com/about/importing-data/

using var reader = new StreamReader("../../../trakt-movies.csv");
using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
var records = csv.GetRecords<TraktWatch>();
Console.WriteLine(records.GroupBy(x => x.ImdbId).Count());
