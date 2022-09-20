using System.IO;

namespace System;

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
}