namespace Inoa;

class Program
{
    static void Main(string[] args)
    {
        EnvReader.Load();

        // string result = await Request("BBAS3");

        // Console.WriteLine(result);
    }


    static async Task<string> Request(string ticker)
    {
        string token = Environment.GetEnvironmentVariable("API_KEY") ?? throw new Exception("Missing API_KEY");
        string url_header = Environment.GetEnvironmentVariable("URL") ?? throw new Exception("Missing URL");

        string url = $"{url_header}{ticker}?token={token}";

        using HttpClient client = new();
        string response = await client.GetStringAsync(url);

        return response;
    }
}
