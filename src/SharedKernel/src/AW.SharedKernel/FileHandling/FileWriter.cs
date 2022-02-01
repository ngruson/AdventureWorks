using AW.SharedKernel.Interfaces;
using System.IO;

namespace AW.SharedKernel.FileHandling
{
    public class FileWriter : IFileWriter
    {
        public bool FileExists(string fileName)
        {
            return File.Exists(fileName);
        }

        public void WriteFile(string fileName, byte[] bytes)
        {
            File.WriteAllBytes(fileName, bytes);
        }
    }
}