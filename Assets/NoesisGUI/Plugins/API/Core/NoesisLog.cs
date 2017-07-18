using System;
using System.Runtime.InteropServices;

namespace Noesis
{
    public class Log
    {
        /// Logs an info message in NoesisConsole
        public static void Info(string text)
        {
            Noesis_LogInfo(text);
        }

        /// Logs a warning message in NoesisConsole
        public static void Warning(string text)
        {
            Noesis_LogWarning(text);
        }

        [DllImport(Library.Name)]
        static extern void Noesis_LogInfo([MarshalAs(UnmanagedType.LPWStr)]string message);

        [DllImport(Library.Name)]
        static extern void Noesis_LogWarning([MarshalAs(UnmanagedType.LPWStr)]string message);
    }
}
