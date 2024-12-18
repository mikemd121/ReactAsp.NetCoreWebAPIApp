using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ReactAsp.NetCoreWebAPIApp.Data.BaseOperation;
using ReactAsp.NetCoreWebAPIApp.Repository.BaseRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReactAsp.NetCoreWebAPIApp.Repository.BaseRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        CoreWebAppDbContext _context = null;

        public DbSet<T> table;


        public GenericRepository(CoreWebAppDbContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }

        public void Delete(object id)
        {
            T existing = table.Find(id);
            //This will mark the Entity State as Deleted
            table.Remove(existing);
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetByID(object id)
        {
            return table.Find(id);
        }

        public void Insert(T entity)
        {
            table.Add(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter)
        {
            return table.Where(filter).ToList();
        }
    }
}
