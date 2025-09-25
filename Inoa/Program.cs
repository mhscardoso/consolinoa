using System.Threading.Tasks;

namespace Inoa;

class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length != 3)
        {
            UsageInfo();

            return;
        }

        EnvReader.Load();        

        string firstTicker = args[0];

        double[] firstPrices = [
            double.Parse(args[1]),
            double.Parse(args[2]),
        ];

        double max_limit = Math.Max(firstPrices[0], firstPrices[1]);
        double min_limit = Math.Min(firstPrices[0], firstPrices[1]);

        StockDB stocks = new(firstTicker, min_limit, max_limit);

        while (true)
        {
            int? option = Menu();

            if (option is null) continue;
            if (option == 6) break;

            switch (option)
            {
                case 1:
                    stocks.WatchStockList();
                    break;
                case 2:
                    await stocks.UpdateStatusForUser();
                    break;
                case 3:
                    stocks.AskAddItem();
                    break;
                case 4:
                    stocks.ChooseStockToWatch();
                    break;
                case 5:
                    stocks.ChooseStockToUpdateBand();
                    break;
                default:
                    Console.WriteLine($"There is no option with id {option}");
                    break;
            }
        }
    }

    static void UsageInfo()
    {
        Console.WriteLine("Usage:");
        Console.WriteLine("    Inoa.exe <TICKER> <PRICE_OPTION> <PRICE_OPTION>");
        Console.WriteLine("Example:");
        Console.WriteLine("    Inoa.exe BBAS3 22.32 25.90");
    }


    static int? Menu()
    {
        Console.WriteLine("-------------------------------");
        Console.WriteLine("1) - Verify current ticket list.");
        Console.WriteLine("2) - Request current price now.");
        Console.WriteLine("3) - Add new ticket to list.");
        Console.WriteLine("4) - Verify ticket data.");
        Console.WriteLine("5) - Update Lowest or Highest band price.");
        Console.WriteLine("6) - Quit Program.");

        int? option = Reader.ReadInt("Your Option (1-6): ");

        if (option is null) return null;

        return option;
    }
}
