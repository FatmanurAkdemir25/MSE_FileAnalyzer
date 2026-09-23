using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSE_FileAnalyzer.Readers
{
    public class TxtFileReader : IFileReader
    {
        public bool CanRead(string filePath)
        {
            string extension = Path.GetExtension(filePath); //dosya yolundan uzantıyı çıkarır
            return extension.Equals(".txt", StringComparison.OrdinalIgnoreCase); //büyük küçük harf duyarlılığını yok sayar
        }
        public string ReadContent(string filePath)
        {
            try
            {
                if (!File.Exists(filePath)) //ilk önce dosyanın var olup olmadığını kontrol ediyoruz
                {
                    throw new FileNotFoundException("Dosya bulunamadı: " + filePath);
                }
                string content = File.ReadAllText(filePath);
                return content;
            }
            catch(FileNotFoundException ex) 
            {
                Console.WriteLine("Hata: Dosya bulunamadı - " + ex.Message);
                throw;//throw ile hatayı tekrar fırlatıyoruz ki üst katman yani program.cs haberdar olsun ve akışı durdurabilsin
            }
            catch(UnauthorizedAccessException ex)
            {
                Console.WriteLine("Hata: Dosyaya erişim izniniz yok - " + ex.Message);
                throw;
            }
            catch(IOException ex)
            {
                Console.WriteLine("Hata: Dosya okunurken bir G/Ç hatası oluştu -" + ex.Message);
                throw;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Beklenmeyen bir hata oluştu - " + ex.Message);
                throw;
            }
        }
    }
}
