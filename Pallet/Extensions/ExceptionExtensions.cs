using System.IO;

namespace System;

public static class ExceptionExtensions
{
    public static string ExceptionToString(this Exception ex)
    {
        StringBuilder sb = new();

        sb.AppendLine("Date/Time: " + DateTime.UtcNow.ToString());
        sb.AppendLine("Exception Type: " + ex.GetType().FullName);
        sb.AppendLine("Message: " + ex.Message);
        sb.AppendLine("Source: " + ex.Source);
        foreach (var key in ex.Data.Keys)
        {
            sb.AppendLine(key.ToString() + ": " + ex.Data[key].ToString());
        }

        if (string.IsNullOrEmpty(ex.StackTrace))
        {
            sb.AppendLine("Environment Stack Trace: " + ex.StackTrace);
        }
        else
        {
            sb.AppendLine("Stack Trace: " + ex.StackTrace);
        }
        string path = @".\Error.txt";  // file path
        using (StreamWriter sw = new(path, true))
        { // If file exists, text will be appended ; otherwise a new file will be created
            sw.Write(sb);
        }
        if (ex.InnerException != null)
        {
            sb.AppendLine("Inner Exception: " + ex.InnerException.ExceptionToString());
        }

        return sb.ToString();
    }
}