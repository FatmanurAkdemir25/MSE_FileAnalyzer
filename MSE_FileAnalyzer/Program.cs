using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSE_FileAnalyzer.Readers;
using System.Windows.Forms;
using MSE_FileAnalyzer.Analysis;
using MSE_FileAnalyzer.Models;

namespace MSE_FileAnalyzer
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string filePath = SelectFile();
            if (string.IsNullOrEmpty(filePath))
            {
                Console.WriteLine("Dosya seçilmedi. Program sonlandırılıyor.");
                return;
            }
            IFileReader reader = GetReaderFor(filePath);
            if(reader == null)
            {
                Console.WriteLine("Bu dosya türü desteklenmiyor.");
                return;
            }
            try
            {
                string content = reader.ReadContent(filePath);
                WordAnalyzer analyzer = new WordAnalyzer();
                AnalysisResult result = analyzer.Analyze(content);
                PrintResult(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("İşlem sırasında hata oluştu:" + ex.Message);
            }
            Console.ReadLine();
        }
        private static string SelectFile()
        {
            using(OpenFileDialog openFileDialog = new OpenFileDialog()) //kullanıcının sadece .txt/.docx görmesini sağlar. using ile diyalog kapanınca kaynak serbest bırakılıyor. kullanıcı iptal derse de null dönüyo
            {
                openFileDialog.Filter = "Desteklenen Dosyalar (*.txt;*.docx)|*.txt;*.docx";
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
                new DocxFileReader()
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
