using System.Linq.Expressions;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {

        T GetById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        void Update(T entity, params Expression<Func<T, object>>[] propertiesToUpdate);
        void UpdateRange(IEnumerable<T> entities, params Expression<Func<T, object>>[] propertiesToUpdate);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Save();

    }
}
