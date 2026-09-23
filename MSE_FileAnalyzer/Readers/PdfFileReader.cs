using MSE_FileAnalyzer.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig;

namespace MSE_FileAnalyzer.Readers
{
    public class PdfFileReader : IFileReader
    {
        public bool CanRead(string filePath)
        {
            string extension = Path.GetExtension(filePath);
            return extension.Equals(".pdf", StringComparison.OrdinalIgnoreCase);
        }
        public string ReadContent(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("Dosya bulunamadı: " + filePath);
                }
                StringBuilder textBuilder = new StringBuilder();//StringBuilder kullandık çünkü birden fazla sayfa metni art arda ekleyeceğiz. string+= kullansaydık her eklemede yeni bir string nesnesi oluştururdu bu da performans kaybına yol açardı
                using(PdfDocument document = PdfDocument.Open(filePath)) //pdf PdfDocument.Open ile açılır
                {
                    foreach(var page in document.GetPages()) //her sayfayı dönüp o sayfadaki düz metni verir
                    {
                        textBuilder.AppendLine(page.Text);
                    }
                }
                return textBuilder.ToString();
            }
            catch(FileNotFoundException ex)
            {
                Logger.LogError("PDF dosyası bulunamadı: " + ex.Message);
                Console.WriteLine("Hata: Dosya bulunamadı " + ex.Message);
                throw;
            }
            catch(Exception ex)
            {
                Logger.LogError(".pdf dosyası okunurken hata: " + ex.Message);
                Console.WriteLine("Hata: .pdf dosyası okunurken bir sorun oluştu " +ex.Message);
                throw;
            }
        }
    }
}
