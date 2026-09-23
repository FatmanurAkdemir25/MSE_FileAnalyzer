using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MSE_FileAnalyzer.Models
{
    public class WordFrequency //kaç kelime kaç kez. AnalysisResult genel sonucu WordFrequency her kelimenin kendi bilgisini tutuyor
    {
        public string Word { get; set; }
        public int Count { get; set; }
    }
}
