namespace Inoa;

class Program
{
    static async Task Main(string[] args)
    {
        string ticker = "BBAS3";

        EnvReader.Load();

        string result = await RequestBuilder.Request(ticker);

        double price = RequestBuilder.GetPrice(result);

        MailSender.SendMail(
            $"{ticker}: BUY",
            $"The price of {ticker} is R$ {price}.{Environment.NewLine}Your lowest band is: R$ 25.03"
        );
    }
}
