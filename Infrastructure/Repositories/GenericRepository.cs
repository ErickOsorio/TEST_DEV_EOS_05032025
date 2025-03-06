using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        protected readonly ApplicationDbContext context;

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            context.Set<T>().AddRange(entities);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return context.Set<T>().Where(expression);
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            context.Set<T>().UpdateRange(entities);
        }

        public void Update(T entity, params Expression<Func<T, object>>[] propertiesToUpdate)
        {
            var entry = context.Entry(entity);
            entry.State = EntityState.Detached;

            foreach (var property in propertiesToUpdate)
            {
                entry.Property(property).IsModified = true;
            }
        }

        public void UpdateRange(IEnumerable<T> entities, params Expression<Func<T, object>>[] propertiesToUpdate)
        {
            foreach (var entity in entities)
            {
                var entry = context.Entry(entity);
                entry.State = EntityState.Detached;

                foreach (var property in propertiesToUpdate)
                {
                    entry.Property(property).IsModified = true;
                }
            }
        }

        public void Remove(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            context.Set<T>().RemoveRange(entities);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
