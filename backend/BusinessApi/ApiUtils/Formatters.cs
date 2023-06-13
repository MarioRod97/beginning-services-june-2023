namespace ApiUtils;

public class Formatters
{
    public static string FormatName(string first, string last)
    {
        return $"{last},${first}";
    }
}