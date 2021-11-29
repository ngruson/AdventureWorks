namespace AW.SharedKernel.Interfaces
{
    public interface IApplication
    {
        string Namespace { get; }
        string AppName { get; }
    }
}