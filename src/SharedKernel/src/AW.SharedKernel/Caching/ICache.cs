using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.SharedKernel.Caching
{
    public interface ICache<T>
    {
        Task Initialize();
        Task<List<T>> GetData();
        Task<List<T>> GetData(Func<T, bool> predicate);
    }
}