namespace AW.SharedKernel.Interfaces
{
    public interface IFileWriter
    {
        bool FileExists(string fileName);
        void WriteFile(string fileName, byte[] bytes);
    }
}