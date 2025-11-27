
internal class MessageHelper
{
    public static void Print(string message, ConsoleColor color = ConsoleColor.Gray)
    {
        Console.ForegroundColor = color;
        Console.WriteLine($"\n{message}");
        Console.ResetColor();
    }

}