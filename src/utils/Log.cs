using System;

public static class Log
{
    public static void Error(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Error: " + message);
        Console.ResetColor();
    }

    public static void Warning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Warning: " + message);
        Console.ResetColor();
    }

    public static void Message(string message)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Message: " + message);
        Console.ResetColor();
    }
}