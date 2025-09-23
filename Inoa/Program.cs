namespace Inoa;

class Program
{
    static async Task Main(string[] args)
    {
        string ticker = "BBAS3";

        EnvReader.Load();

        string result = await RequestBuilder.Request(ticker);

        double price = RequestBuilder.GetPrice(result);

        Console.WriteLine($"{ticker}: {price}");
    }
}
