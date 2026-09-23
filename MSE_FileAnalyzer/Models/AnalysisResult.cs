using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSE_FileAnalyzer.Models
{
    public class AnalysisResult
    {
        public int TotalUniqueWordCount { get; set; }
        public List<WordFrequency> WordFrequencies { get; set; }
        public Dictionary<char, int> PunctuationCounts { get; set; }
    }
}
