using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReactAsp.NetCoreWebAPIApp.Repository.BaseRepository.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetByID(Object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(object id);
        IEnumerable<T> Get(Expression<Func<T, bool>> filter); // New method in the interface

        void Save();
        
     
    }
}
