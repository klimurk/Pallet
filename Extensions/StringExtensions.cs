namespace Pallet.Extensions
{


public static class StringExtensions
{
    public static void CheckStage(this string s)
    {
        string path = @".\Trace.txt";  // file path
        using (StreamWriter sw = new(path, true))
        { // If file exists, text will be appended ; otherwise a new file will be created
            sw.WriteLine(DateTime.Now + " --- " + s);
        }
    }

    public static string RemoveWhitespace(this string input)
    {
        return new string(input.ToCharArray()
            .Where(c => !char.IsWhiteSpace(c))
            .ToArray());
    }
}
}