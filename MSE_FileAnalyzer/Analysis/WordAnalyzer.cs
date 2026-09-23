using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSE_FileAnalyzer.Models;
using System.Text.RegularExpressions;

namespace MSE_FileAnalyzer.Analysis
{
    public class WordAnalyzer
    {
        public AnalysisResult Analyze(string content)
        {
            string[] words = ExtractWords(content);//kelimeleri ayıklama
            var frequencies = words
                .GroupBy(w => w) //aynı kelimeleri grupla ve say
                .Select(g => new WordFrequency { Word = g.Key, Count = g.Count()}) //çoktan aza sırala
                .OrderByDescending(wf => wf.Count)
                .ToList();
            var result = new AnalysisResult()
            {
                TotalUniqueWordCount = frequencies.Count,
                WordFrequencies = frequencies,
                PunctuationCounts = CountPunctuation(content)
            };
            return result;
        }
        private string[] ExtractWords(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return new string[0];
            }
            //harf olmayan her şeyi ayraç kabul ediyoruz
            string[] rawWords = Regex.Split(content.ToLower(), @"[^a-zçğıöşü]+"); //a-z ve türkçe karakterler hariç her şey burada sayıları da filtrelemiş olduk
            var filteredWords = rawWords
                .Where(w => !string.IsNullOrWhiteSpace(w)) //split sonrası boş stringler oluşabilir onları eliyoruz mesela iki noktalama yan yanaysa
                .Where(w => !TurkishStopWords.Conjunctions.Contains(w)) //bağlaçlar
                .ToArray();
            return filteredWords;
            
        }
        private Dictionary<char, int> CountPunctuation(string content)
        {
            var punctuationCounts = new Dictionary<char, int>();
            if (string.IsNullOrEmpty(content))
            {
                return punctuationCounts;
            }
            foreach (char c in content)
            {
                if(char.IsPunctuation(c))
                {
                    if (punctuationCounts.ContainsKey(c))
                    {
                        punctuationCounts[c]++;
                    }
                    else
                    {
                        punctuationCounts[c] = 1;
                    }
                }
            }
            return punctuationCounts;
        }
    }
}
