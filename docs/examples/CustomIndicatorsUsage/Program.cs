using System.Collections.ObjectModel;
using System.Text.Json;
using Custom.Stock.Indicators;
using Skender.Stock.Indicators;

namespace CustomIndicatorsUsage;

// USE CUSTOM INDICATORS exactly the same as
// other indicators in the library

public static class Program
{
    public static void Main()
    {
        // fetch historical quotes from data provider
        IEnumerable<Quote> quotes = GetQuotesFromFeed();

        // calculate 10-period custom AtrWma
        IEnumerable<AtrWmaResult> results = quotes
            .GetAtrWma(10);

        // show results
        Console.WriteLine("ATR WMA Results ---------------------------");

        foreach (AtrWmaResult r in results.Take(30))
        {
            // only showing first 30 records for brevity
            Console.WriteLine($"ATR WMA on {r.Date:u} was ${r.AtrWma:N3}");
        }
    }

    private static Collection<Quote> GetQuotesFromFeed()
    {
        /************************************************************

         We're mocking a data provider here by simply importing a
         JSON file, a similar format of many public APIs.

         This approach will vary widely depending on where you are
         getting your quote history.

         See https://github.com/DaveSkender/Stock.Indicators/discussions/579
         for free or inexpensive market data providers and examples.

         The return type of IEnumerable<Quote> can also be List<Quote>
         or ICollection<Quote> or other IEnumerable compatible types.

         ************************************************************/

        string json = File.ReadAllText("quotes.data.json");

        Collection<Quote> quotes = JsonSerializer
            .Deserialize<IReadOnlyCollection<Quote>>(json)
            .ToSortedCollection();

        return quotes;
    }
}
