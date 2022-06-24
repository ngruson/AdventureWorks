namespace AW.UI.Web.Internal.Core.Interfaces
{
    public interface ICache<T>
    {
        Task Initialize();
        Task<List<T>> GetData();
        Task<List<T>> GetData(Func<T, bool> predicate);
    }
}