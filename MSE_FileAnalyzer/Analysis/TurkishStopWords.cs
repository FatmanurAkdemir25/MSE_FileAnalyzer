using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSE_FileAnalyzer.Analysis
{
    public static class TurkishStopWords
    {
        public static readonly HashSet<string> Conjunctions = new HashSet<string> //HashSet kullandık çünkü tek tek kontrol yapacağız ve bu konuda List ten daha hızlı
        {
            "ve", "ile", "ama", "fakat", "ancak", "veya", "ya", "da", "de",
            "çünkü", "hem", "ne", "ki", "de", "ise", "yada", "lakin", "oysa"
        };
    }
}
