namespace Inoa;

public class Reader
{
    public static string? ReadString(string message)
    {
        Console.WriteLine(message);
        string? value = Console.ReadLine();

        if (value is null)
        {
            Console.WriteLine("There must be some data.");
            return null;
        }

        return value;
    }


    public static double? ReadDouble(string message)
    {
        string? strValue = ReadString(message);
        if (strValue == null) return null;

        double result;

        try
        {
            result = double.Parse(strValue);
        }
        catch
        {
            Console.WriteLine("Cannot convert to Double.");
            return null;
        }

        return result;
    }


    public static int? ReadInt(string message)
    {
        string? strValue = ReadString(message);
        if (strValue == null) return null;

        int result;

        try
        {
            result = int.Parse(strValue);
        }
        catch
        {
            Console.WriteLine("Cannot convert to Double.");
            return null;
        }

        return result;
    }
}
