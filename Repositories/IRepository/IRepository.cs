using System.Linq.Expressions;

namespace CHARIBOY_ARTS.Repositories.IRepository
{
    public interface IRepository<T> where T: class
    {
        IEnumerable<T> GetAll(string? IncludeProperties = null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        // void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
