using System.Configuration;
using System.Collections.Specialized;

namespace Inoa;

class EnvReader
{
    public static void Load()
    {
        NameValueCollection sAll = ConfigurationManager.AppSettings;

        foreach (string? key in sAll.AllKeys)
        {
            if (key is null) continue;
            Environment.SetEnvironmentVariable(key, sAll.Get(key));
        }
    }


    public static string GetVariable(string key)
    {
        string value = Environment.GetEnvironmentVariable(key) ??
            throw new Exception($"Missing {key}");

        return value;
    }
}
