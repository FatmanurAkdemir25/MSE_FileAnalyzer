using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSE_FileAnalyzer.Readers
{
    public interface IFileReader
    {
        bool CanRead(string filePath); //bu dosyayı ben okuyabilir miyim
        string ReadContent(string filePath); //dosyanın içeriğini okuyup tek bir string olarak döner
    }
}
