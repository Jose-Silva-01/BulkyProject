using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        // T - any generic model on which we want to interact with the DB
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);

        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false); // general syntax to be able to write a link expression in a method

        void Add(T entity);
        //void Update(T entity); Since update can be different for different classes, it's interesting to leave it out of the Interface
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
