using System.Configuration;
using System.Collections.Specialized;

class EnvReader
{
    public static void Load()
    {
        NameValueCollection sAll = ConfigurationManager.AppSettings;

        foreach (string? key in sAll.AllKeys)
        {
            Console.WriteLine($"{key} = {sAll.Get(key)}");
        }
    }
}
