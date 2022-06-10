namespace AW.SharedKernel.Interfaces
{
    public interface IFileHandler
    {
        bool FileExists(string fileName);
        void WriteFile(string fileName, byte[] bytes);
    }
}