using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiyo_Core.Utilities.ErrorManagement
{
    class Logger
    {
        public static void Log(string msg)
        {
#if WINDOWS
            System.Diagnostics.Debug.WriteLine($"LOG:: {msg}");
#else
            Console.WriteLine($"LOG:: {msg}");
#endif
        }

        public static void LogWarning(string msg)
        {
#if WINDOWS
            System.Diagnostics.Debug.WriteLine($"WARN:: {msg}");
#else
            Console.WriteLine($"WARN:: {msg}");
#endif

        }

        public static void LogError(string msg)
        {
#if WINDOWS
            System.Diagnostics.Debug.WriteLine($"ERROR:: {msg}");
#else
            Console.WriteLine($"ERROR:: {msg}");
#endif

        }

        public static void LogException(string msg)
        {
#if WINDOWS
            System.Diagnostics.Debug.WriteLine($"EXCEPTION:: {msg}");
#else
            Console.WriteLine($"EXCEPTION:: {msg}");
#endif
        }
    }
}
