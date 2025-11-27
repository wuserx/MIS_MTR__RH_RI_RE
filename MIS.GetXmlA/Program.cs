
using System.Runtime.Intrinsics.X86;
using static System.Runtime.InteropServices.JavaScript.JSType;

try
{
    Console.OutputEncoding = System.Text.Encoding.UTF8; // 🟢 Включаем UTF-8

    Console.Title = string.Concat("MIS-MTR (RH_RI_RE) ", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
    
    Init.Get();

    App.RunAsync().Wait();    
}
catch (Exception ex)
{
    Console.BackgroundColor = ConsoleColor.DarkRed;
    Console.WriteLine(ex.Message);
    Console.ResetColor();
}

Console.ReadKey();
