namespace Inoa;

public class Stock
{
    public Stock(string stockName, double lowestBand, double highestBand) =>
        (StockName, LowestBand, HighestBand) = (stockName, lowestBand, highestBand);
    public required string StockName { get; init; }
    public string? LastTimeGet { get; set; }
    public double? Price { get; set; }
    required public double LowestBand { get; set; }
    required public double HighestBand { get; set; }
}


public class StockDB
{
    public StockDB(string stockName, double lowestBand, double highestBand)
    {
        Stock newStock = new(stockName, lowestBand, highestBand)
        {
            StockName = stockName,
            LowestBand = lowestBand,
            HighestBand = highestBand
        };

        Stocks = [newStock];
    }

    public List<Stock> Stocks { get; init; }

    public int FindIndexByStockName(string stockName)
    {
        int stockIndex = Stocks.FindIndex(s => s.StockName == stockName);

        return stockIndex;
    }

    public void UpdateStockItem(int index, string lastTimeGet, double price)
    {
        Stocks[index].LastTimeGet = lastTimeGet;
        Stocks[index].Price = price;
    }


    public void UpdateBands(int index, double bandOne, double bandTwo)
    {
        double minBand = Math.Min(bandOne, bandTwo);
        double maxBand = Math.Max(bandOne, bandTwo);

        Stocks[index].LowestBand = minBand;
        Stocks[index].HighestBand = maxBand;
    }

    public void ChooseStockToUpdateBand()
    {
        WatchStockList();

        int? id = Reader.ReadInt("Choose some ticket by id:");
        if (id is null) return;

        if (id < 0 || id >= Stocks.Count)
        {
            Console.WriteLine($"There is no ticket with id {id}.");
            return;
        }

        double? bandOne = Reader.ReadDouble("Choose first band: ");
        if (bandOne is null) return;

        double? bandTwo = Reader.ReadDouble("Choose second band: ");
        if (bandTwo is null) return;

        UpdateBands((int)id, (double)bandOne, (double)bandTwo);

        Console.WriteLine($"You modify the bands of {Stocks[(int)id].StockName}");
    }

    public void AddStockItem(string stockName, double numberBand1, double numberBand2)
    {
        double lowestBand = Math.Min(numberBand1, numberBand2);
        double highestBand = Math.Min(numberBand1, numberBand2);

        if (FindIndexByStockName(stockName) != -1)
        {
            return;
        }

        Stock newStock = new(stockName, lowestBand, highestBand)
        {
            StockName = stockName,
            LastTimeGet = null,
            Price = null,
            LowestBand = lowestBand,
            HighestBand = highestBand
        };

        Stocks.Add(newStock);
    }


    public void AskAddItem()
    {
        string? ticket = Reader.ReadString("Ticket Name: ");
        if (ticket is null) return;

        ticket = ticket.ToUpper();

        double? bandOne = Reader.ReadDouble("First Band Parameter: ");
        double? bandTwo = Reader.ReadDouble("Second Band Parameter: ");

        if (bandOne is null || bandTwo is null) return;

        AddStockItem(ticket, (double)bandOne, (double)bandTwo);
    }


    public void WatchStockList()
    {
        Console.WriteLine($"{Environment.NewLine}Stocks List Below:{Environment.NewLine}");
        Console.WriteLine("id | Ticket");
        Console.WriteLine("-----------");

        for (int i = 0; i < Stocks.Count; i++)
        {
            Console.WriteLine($" {i} | {Stocks[i].StockName}");
        }
        Console.WriteLine($"-----------{Environment.NewLine}");
    }

    public void WatchStockInfo(int index)
    {
        Stock thisStock = Stocks[index];

        Console.WriteLine($"# ------------------ #");
        Console.WriteLine($"Ticket:        {thisStock.StockName}");
        Console.WriteLine($"Price:         {thisStock.Price}");
        Console.WriteLine($"Last Request:  {thisStock.LastTimeGet}");
        Console.WriteLine($"LowestBand:    {thisStock.LowestBand}");
        Console.WriteLine($"HighestBand:   {thisStock.HighestBand}");
        Console.WriteLine($"# ------------------ #");
    }

    public void ChooseStockToWatch()
    {
        WatchStockList();

        int? id = Reader.ReadInt("Choose some ticket by id:");
        if (id is null) return;

        if (id < 0 || id >= Stocks.Count)
        {
            Console.WriteLine($"There is no ticket with id {id}.");
            return;
        }

        WatchStockInfo((int)id);
    }


    public string FormTicketStringList()
    {
        string result = "";

        foreach (Stock s in Stocks)
        {
            result += $"{s.StockName},";
        }

        result = result[..^1];

        return result;
    }


    public async Task<List<Stock>> RequestForAll()
    {
        string ticketString = FormTicketStringList();

        string resultString = await RequestBuilder.Request(ticketString);

        RequestDTO request = RequestBuilder.TransformRequest(resultString);
        string requestedAt = request.requestedAt;

        List<Stock> stocksOuterBand = [];

        foreach (ResultDTO result in request.results)
        {
            int ticketIndex = FindIndexByStockName(result.symbol);
            UpdateStockItem(ticketIndex, requestedAt, result.regularMarketPrice);

            if (
                result.regularMarketChange < Stocks[ticketIndex].LowestBand ||
                result.regularMarketPrice > Stocks[ticketIndex].HighestBand
            )
            {
                stocksOuterBand.Add(Stocks[ticketIndex]);
            }
        }

        Console.WriteLine("Request Done. You can check your Tickets.");

        return stocksOuterBand;
    }



    // Make many requests for price.
    public async Task<List<Stock>> RequestAll()
    {
        List<Stock> stocksOuterBand = [];

        foreach (Stock s in Stocks)
        {
            string resultString = await RequestBuilder.Request(s.StockName.ToUpper());
            RequestDTO request = RequestBuilder.TransformRequest(resultString);
            string requestedAt = request.requestedAt;

            ResultDTO result = request.results.First();

            int ticketIndex = FindIndexByStockName(result.symbol);
            UpdateStockItem(ticketIndex, requestedAt, result.regularMarketPrice);

            if (
                result.regularMarketChange < Stocks[ticketIndex].LowestBand ||
                result.regularMarketPrice > Stocks[ticketIndex].HighestBand
            )
            {
                stocksOuterBand.Add(Stocks[ticketIndex]);
            }
        }

        return stocksOuterBand;
    }


    public async Task<List<Stock>> RequestForOne()
    {
        WatchStockList();

        int? id = Reader.ReadInt("Choose some ticket by id:");
        if (id is null) return [];

        if (id < 0 || id >= Stocks.Count)
        {
            Console.WriteLine($"There is no ticket with id {id}.");
            return [];
        }

        string ticketString = Stocks[(int)id].StockName;

        string resultString = await RequestBuilder.Request(ticketString);

        RequestDTO request = RequestBuilder.TransformRequest(resultString);
        string requestedAt = request.requestedAt;

        List<Stock> stocksOuterBand = [];

        foreach (ResultDTO result in request.results)
        {
            int ticketIndex = FindIndexByStockName(result.symbol);
            UpdateStockItem(ticketIndex, requestedAt, result.regularMarketPrice);

            if (
                result.regularMarketChange < Stocks[ticketIndex].LowestBand ||
                result.regularMarketPrice > Stocks[ticketIndex].HighestBand
            )
            {
                stocksOuterBand.Add(Stocks[ticketIndex]);
            }
        }

        Console.WriteLine("Request Done. You can check your Tickets.");

        return stocksOuterBand;
    }

    public static string MountMessage(List<Stock> stocks)
    {
        string message = "";

        foreach (Stock s in stocks)
        {
            if (s.Price < s.LowestBand)
            {
                message += $"{s.StockName}: BUY ; Price: {s.Price}; Lowest: {s.LowestBand}{Environment.NewLine}";
            }
            else
            {
                message += $"{s.StockName}: SELL; Price: {s.Price}; Highest: {s.HighestBand}{Environment.NewLine}";
            }
        }

        return message;
    }


    public async Task UpdateStatusForUser()
    {
        List<Stock> stocksOuterBand = await RequestAll();
        string message = MountMessage(stocksOuterBand);

        MailSender.SendMail("Inoa: Stock List Review", message);
    }
}
