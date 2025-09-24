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

        AddStockItem(ticket, (double) bandOne, (double) bandTwo);
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
        Console.WriteLine($"LowestBand:    {thisStock.LowestBand}");
        Console.WriteLine($"HighestBand:   {thisStock.HighestBand}");
        Console.WriteLine($"# ------------------ #");
    }

    public void ChooseStockToWatch()
    {
        WatchStockList();

        Console.WriteLine("Choose some ticket by id:");
        string? strNumber = Console.ReadLine();

        if (strNumber is null)
        {
            Console.WriteLine("Wrong Input");
            return;
        }

        int id;

        try
        {
            id = int.Parse(strNumber);
        }
        catch
        {
            Console.WriteLine("Not a valid number.");
            return;
        }

        if (id >= Stocks.Count)
        {
            Console.WriteLine($"There is no ticket with id {id}.");
            return;
        }

        WatchStockInfo(id);
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
}
