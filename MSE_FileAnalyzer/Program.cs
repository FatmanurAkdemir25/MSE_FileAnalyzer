using MSE_FileAnalyzer.Analysis;
using MSE_FileAnalyzer.Logging;
using MSE_FileAnalyzer.Models;
using MSE_FileAnalyzer.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MSE_FileAnalyzer.Logging;

namespace MSE_FileAnalyzer
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Logger.LogInfo("Uygulama başlatıldı.");

            string filePath = SelectFile();
            if (string.IsNullOrEmpty(filePath))
            {
                Logger.LogInfo("Kullanıcı dosya seçmedi.");
                Console.WriteLine("Dosya seçilmedi. Program sonlandırılıyor.");
                return;
            }
            Logger.LogInfo("Seçilen dosya: " + filePath);
            IFileReader reader = GetReaderFor(filePath);
            if(reader == null)
            {
                Logger.LogError("Desteklenmeyen dosya türü: " + filePath);
                Console.WriteLine("Bu dosya türü desteklenmiyor.");
                return;
            }
            try
            {
                string content = reader.ReadContent(filePath);
                Logger.LogInfo("Dosya başarıyla okundu, analiz başlıyor.");
                WordAnalyzer analyzer = new WordAnalyzer();
                AnalysisResult result = analyzer.Analyze(content);
                Logger.LogInfo("Analiz tamamlandı. Toplam farklı kelime: " + result.TotalUniqueWordCount);
                PrintResult(result);
            }
            catch (Exception ex)
            {
                Logger.LogError("İşlem sırasında hata: " + ex.Message);
                Console.WriteLine("İşlem sırasında hata oluştu:" + ex.Message);
            }
            Console.ReadLine();
        }
        private static string SelectFile()
        {
            using(OpenFileDialog openFileDialog = new OpenFileDialog()) //kullanıcının sadece .txt/.docx görmesini sağlar. using ile diyalog kapanınca kaynak serbest bırakılıyor. kullanıcı iptal derse de null dönüyo
            {
                openFileDialog.Filter = "Desteklenen Dosyalar (*.txt;*.docx;*.pdf)|*.txt;*.docx;*.pdf";
                openFileDialog.Title = "Analiz edilecek dosyayı seçin";
                if(openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return openFileDialog.FileName;
                }
                return null;
            }
        }
        private static IFileReader GetReaderFor(string filePath)//GetReaderFor sayesinde mesela .pdf desteği eklemek istersek  sadece new PdfFileReader() eklememiz yeterli geliştirilebilirlik için gerekli
        {
            List<IFileReader> availableReaders = new List<IFileReader>
            {
                new TxtFileReader(),
                new DocxFileReader(),
                new PdfFileReader()
            };
            foreach(IFileReader reader in availableReaders)
            {
                if(reader.CanRead(filePath))
                {
                    return reader;
                }
            }
            return null;
        }
        private static void PrintResult(AnalysisResult result) //sonuçları ekrana düzenli bastırır
        {
            Console.WriteLine();
            Console.WriteLine("-------ANALİZ SONUÇLARI-------");
            Console.WriteLine("Toplam farklı kelime sayısı: " + result.TotalUniqueWordCount);
            Console.WriteLine();
            foreach (WordFrequency wf in result.WordFrequencies)
            {
                Console.WriteLine(wf.Word + " : " + wf.Count);
            }

            Console.WriteLine();
            Console.WriteLine("--- Noktalama İşaretleri ---");
            foreach (KeyValuePair<char, int> item in result.PunctuationCounts)
            {
                Console.WriteLine("'" + item.Key + "' : " + item.Value);
            }
        }
    }
}
