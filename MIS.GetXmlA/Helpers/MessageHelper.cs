
internal class MessageHelper
{
    public static void Print(string message, ConsoleColor? color = null)
    {
        if (Console.IsOutputRedirected)
        {
            Console.WriteLine(message);
            return;
        }

        try
        {
            var currentTop = Console.CursorTop;
            var currentLeft = Console.CursorLeft;

            // Если мы на строке 0 — сдвигаемся на строку 1
            if (currentTop == 0)
            {
                Console.SetCursorPosition(0, 1);
            }

            if (color.HasValue)
            {
                Console.ForegroundColor = color.Value;
                Console.WriteLine(message);
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine(message);
            }

            // Всё ещё не возвращаемся на строку 0 — пусть прогресс-бар остаётся отдельно
        }
        catch
        {
            // Игнорируем ошибки вывода
            Console.WriteLine(message); // fallback
        }
    }

}