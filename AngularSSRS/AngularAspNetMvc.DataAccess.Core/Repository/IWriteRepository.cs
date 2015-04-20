using System;
using System.Threading.Tasks;

namespace AngularAspNetMvc.DataAccess.Core.Repository
{
    public interface IWriteRepository : IDisposable
    {

        TItem Update<TItem>(TItem item, bool saveImmediately = true) 
            where TItem : class, new();


        TItem Delete<TItem>(TItem item, bool saveImmediately = true) 
            where TItem : class, new();


        TItem Insert<TItem>(TItem item, bool saveImmediately = true) 
            where TItem : class, new();

        TItem GetByPrimaryKey<TItem>(params object[] keyValues)
            where TItem : class, new();

        int Save();


        Task<int> SaveAsync();

    }
}
