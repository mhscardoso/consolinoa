

using System.Text.Json;

namespace Inoa;

class ResultDTO
{
    required public string currency { get; set; }
    required public double earningsPerShare { get; set; }
    required public double fiftyTwoWeekHigh { get; set; }
    required public double fiftyTwoWeekLow { get; set; }
    required public string fiftyTwoWeekRange { get; set; }
    required public string logourl { get; set; }
    required public string longName { get; set; }
    required public double marketCap { get; set; }
    required public double priceEarnings { get; set; }
    required public double regularMarketChange { get; set; }
    required public double regularMarketChangePercent { get; set; }
    required public double regularMarketDayHigh { get; set; }
    required public double regularMarketDayLow { get; set; }
    required public string regularMarketDayRange { get; set; }
    required public double regularMarketOpen { get; set; }
    required public double regularMarketPreviousClose { get; set; }
    required public double regularMarketPrice { get; set; }
    required public string regularMarketTime { get; set; }
    required public double regularMarketVolume { get; set; }
    required public string shortName { get; set; }
    required public string symbol { get; set; }
}


class RequestDTO
{
    required public string requestedAt { get; set; }
    required public ResultDTO[] results { get; set; }
    required public string took { get; set; }
}


class RequestBuilder
{
    public static async Task<string> Request(string ticker)
    {
        string token = EnvReader.GetVariable("API_KEY");

        string url_header = EnvReader.GetVariable("URL");

        string url = $"{url_header}{ticker}?token={token}";

        using HttpClient client = new();
        string response = await client.GetStringAsync(url);

        return response;
    }


    public static double GetPrice(string tickerInfo)
    {
        RequestDTO request = JsonSerializer.Deserialize<RequestDTO>(tickerInfo) ??
            throw new Exception("Error converting String to JSON.");

        return request.results[0].regularMarketPrice;
    }

    public static RequestDTO TransformRequest(string tickerInfo)
    {
        RequestDTO request = JsonSerializer.Deserialize<RequestDTO>(tickerInfo) ??
            throw new Exception("Error converting String to JSON.");

        return request;
    }
}
