using Aiyo_Core.Utilities.Constants;

namespace Aiyo_Core.Utilities.ErrorManagement
{
    public static class Logger
    {
        private static StreamWriter fileWriter;
        private static string CurrTime => DateTime.Now.ToLocalTime().ToString("HH:mm:ss");

        static void StaticClass_Dtor(object sender, EventArgs e)
        {
            // clean it up
            fileWriter.Close();
        }

        public static void Log(string msg)
        {
            WriteLine($"[LOG  ]::{CurrTime}::{msg}");
        }

        public static void LogWarning(string msg)
        {
            WriteLine($"[WARN ]::{CurrTime}::{msg}");
        }

        public static void LogError(string msg)
        {
            WriteLine($"[ERROR]::{CurrTime}::{msg}");
        }

        public static void LogException(string msg)
        {
            WriteLine($"[EXCEP]::{CurrTime}::{msg}");
        }

        private static void WriteLine(string msg)
        {
            System.Diagnostics.Debug.WriteLine(msg);
            WriteToFile(msg);
        }

        private static void WriteToFile(string msg)
        {
            if(fileWriter== null)
            {
                //create dir if not exists
                if(!Directory.Exists($"{AiyoCoreConstants.LOG_FILE_DIR}"))
                    Directory.CreateDirectory($"{AiyoCoreConstants.LOG_FILE_DIR}");

                var path = string.Format(AiyoCoreConstants.LOG_FILE_NAME, DateTime.Now.ToLocalTime().ToString("dd-MM-yyyy_hh-mm-ss"));
                fileWriter = File.AppendText($"{AiyoCoreConstants.LOG_FILE_DIR}\\{path}");
            }

            fileWriter.WriteLine(msg);
            fileWriter.Flush();
        }
    }
}
