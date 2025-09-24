namespace Inoa;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 3)
        {
            UsageInfo();

            return;
        }
        

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
            if (option == 5) break;

            switch (option)
            {
                case 1:
                    stocks.WatchStockList();
                    break;
                case 2:
                    Console.WriteLine("Option 2");
                    break;
                case 3:
                    stocks.AskAddItem();
                    break;
                default:
                    Console.WriteLine("Dont");
                    break;
            }
        }

        // stocks.AddStockItem("BBSA3", 23.5, 28.9);

        // Console.WriteLine($"Stocks String: {stocks.FormTicketStringList()}");

        // stocks.ChooseStockToWatch();

        // EnvReader.Load();

        // string result = await RequestBuilder.Request(firstTicker);

        // double price = RequestBuilder.GetPrice(result);
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
        Console.WriteLine("4) - Update Lowest or Highest band price.");
        Console.WriteLine("5) - Quit Program.");

        int? option = Reader.ReadInt("Your Option (1-5): ");

        if (option is null) return null;

        return option;
    }
}
