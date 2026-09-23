using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MSE_FileAnalyzer.Logging
{
    public static class Logger //static yaptık çünkü logger'ın neseneye ihtiyacı yok direkt çağıracağız
    {
        private static readonly string LogFilePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "application.log"); // programın çalıştığı klasörü verir. log dosyası orda oluşur
        public static void LogInfo(string message)
        {
            WriteLog("INFO",message);
        }
        public static void LogError(string message)
        {
            WriteLog("ERROR", message);
        }

        private static void WriteLog(string level, string message)
        {
            try
            {
                string logLine = string.Format(
                    "[{0}] [{1}] {2}",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    level,
                    message);

                File.AppendAllText(LogFilePath, logLine + Environment.NewLine); //her çağrıldığında dosyanın sonuna ekler, üzerine yazmaz böylece geçmiş loglar korunur
            }
            catch
            {
                // Loglama başarısız olsa bile programın çökmesini istemiyoruz
            }
        }
    }
}
