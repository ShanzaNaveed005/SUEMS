using System;

namespace SUEMS.Core
{
    public static class UIHelper
    {
        // 🎨 Header
        public static void Header(string text)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n================================");
            Console.WriteLine($"   {text.ToUpper()}");
            Console.WriteLine("================================\n");
            Console.ResetColor();
        }

        // ✅ Success message
        public static void Success(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✔ " + msg);
            Console.ResetColor();
        }

        // ❌ Error message
        public static void Error(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("✖ " + msg);
            Console.ResetColor();
        }

        // ⚠️ Warning
        public static void Warning(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("⚠ " + msg);
            Console.ResetColor();
        }

        // 📌 Menu item
        public static void Menu(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
    }
}