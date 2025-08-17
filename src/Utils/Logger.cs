using System;

namespace TABFRET.Utils
{
    public static class Logger
    {
        public static void Log(string message)
        {
            // Simple console logging, can be extended to file or UI
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
        }
    }
}
