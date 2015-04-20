using System.Data.Entity;
using System.Threading.Tasks;

namespace AngularAspNetMvc.DataAccess.Core.Repository
{
    public abstract class WriteRepository<TContext> : IWriteRepository
        where TContext : DbContext, new()
    {
        private readonly TContext _context;

        protected TContext Context { get { return _context; } }

        protected WriteRepository()
        {
            _context = new TContext();
            _context.Configuration.ProxyCreationEnabled = false;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public TItem Update<TItem>(TItem item, bool saveImmediately = true) 
            where TItem : class, new()
        {
            return PerformAction(item, EntityState.Modified, saveImmediately);
        }

        public TItem Delete<TItem>(TItem item, bool saveImmediately = true) 
            where TItem : class, new()
        {
            return PerformAction(item, EntityState.Deleted, saveImmediately);
        }

        public TItem Insert<TItem>(TItem item, bool saveImmediately = true) 
            where TItem : class, new()
        {
            return PerformAction(item, EntityState.Added, saveImmediately);
        }

        public TItem GetByPrimaryKey<TItem>(params object[] keyValues) 
            where TItem : class, new()
        {
            return Context.Set<TItem>().Find(keyValues);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        protected virtual TItem PerformAction<TItem>(
            TItem item, 
            EntityState entityState, 
            bool saveImmediately = true) 
            where TItem : class, new()
        {
            _context.Entry(item).State = entityState;
            if (saveImmediately)
            {
                _context.SaveChanges();
            }
            return item;
        }
    }
}