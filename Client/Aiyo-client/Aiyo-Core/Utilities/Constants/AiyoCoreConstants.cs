using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiyo_Core.Utilities.Constants
{
    public static class AiyoCoreConstants
    {
        public static readonly string APP_DIAG_BASE_DIR = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        public static string DIAGNOSTICS_DIR {
            get
            { 
                var dirName = "AiyoDiagnostics";
                var path = $"{APP_DIAG_BASE_DIR}\\{dirName}";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                return path;
            }
        }

        public static readonly string LOG_FILE_DIR = $"{DIAGNOSTICS_DIR}\\Logs";
        public static readonly string LOG_FILE_NAME = "Aiyo_Usage_{0}.log";

    }
}
