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
}
