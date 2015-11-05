   using System;

    static partial class MyLogClass
    {
       static void WriteBrightLine (string message)
       {
           var color = Console.ForegroundColor;
           Console.ForegroundColor = ConsoleColor.White;
           try
           {
               Console.WriteLine (message ?? "");
           }
           finally
           {
               Console.ForegroundColor = color;
           }
       }

       static void WriteNormalLine (string message)
       {
           var color = Console.ForegroundColor;
           Console.ForegroundColor = ConsoleColor.Gray;
           try
           {
               Console.WriteLine (message ?? "");
           }
           finally
           {
               Console.ForegroundColor = color;
           }
       }

       static void WriteUrlLine (string message)
       {
           var color = Console.ForegroundColor;
           Console.ForegroundColor = ConsoleColor.Cyan;
           try
           {
               Console.WriteLine (message ?? "");
           }
           finally
           {
               Console.ForegroundColor = color;
           }
       }

       
}
