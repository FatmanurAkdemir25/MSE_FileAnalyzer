using MSE_FileAnalyzer.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Words.NET;

namespace MSE_FileAnalyzer.Readers
{
    public class DocxFileReader : IFileReader
    {
        public bool CanRead(string filePath)
        {
            string extension = Path.GetExtension(filePath);
            return extension.Equals(".docx", StringComparison.OrdinalIgnoreCase);
        }
        public string ReadContent(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("Dosya bulunamadı: " + filePath);
                }
                using(DocX document = DocX.Load(filePath))//using dosya işi bitince kaynakları otomatik serbest bırakır dosyayı kilitli bırakmamak için önemli
                {
                    return document.Text; //word dosyasındaki tüm düz metni tek string olarak verir
                }
            }
            catch(FileNotFoundException ex)
            {
                Logger.LogError("Dosya bulunamadı: " + ex.Message);
                Console.WriteLine("Hata: Dosya bulunamadı - " + ex.Message);
                throw;
            }
            catch(Exception ex)
            {
                Logger.LogError("Dosya bulunamadı: " + ex.Message);
                Console.WriteLine("Hata: .docx dosyasını okurken bir sorun oluştu" + ex.Message);
                throw;
            }
        }
    }
}
