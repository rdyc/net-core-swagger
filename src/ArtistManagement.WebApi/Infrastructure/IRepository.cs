using System;
using System.Linq;
using System.Linq.Expressions;

namespace ArtistManagement.WebApi.Infrastructure
{
    public interface IRepository<T> where T : Entity, IAggregateRoot
    {
        IQueryable<T> Get();

        IQueryable<T> Get(Expression<Func<T, object>> includes);

        T Get(string id);
        
        T Get(string id, Expression<Func<T, object>> includes);

        void Add(T entity);

        void Update(T entity);
        
        void Delete(T entity);
    }
}