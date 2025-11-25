
using System.Runtime.Intrinsics.X86;
using static System.Runtime.InteropServices.JavaScript.JSType;

try
{
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
