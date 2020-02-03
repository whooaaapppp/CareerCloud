using CareerCloud.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;


namespace CareerCloud.EntityFrameworkDataAccess
{
    public class EFGenericRepository<T> : IDataRepository<T> where T : class
    {
        private CareerCloudContext _context;
        
        public EFGenericRepository()
        {
            _context = new CareerCloudContext();
        }
        
        public void Add(params T[] items)
        {
            //array T and loop through the array and foreach of those since entry is one to one and set the enum entity state added ~4
            foreach(T item in items)
            {
                _context.Entry(item).State = EntityState.Added;
            }
            //telling EF savechanges task 
            _context.SaveChangesAsync();
        }

        

        public IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            //building an expression tree which is an IQueryable
            IQueryable<T> dbQuery = _context.Set<T>();
            foreach(Expression<Func<T,object>> property in navigationProperties)
            {
                dbQuery = dbQuery.Include<T, object>(property);
            }
            return dbQuery.ToList<T>();
        }

        

        public T GetSingle(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public void Remove(params T[] items)
        {
            foreach (T item in items)
            {
                _context.Entry(item).State = EntityState.Deleted;
            }
            //telling EF savechanges task 
            _context.SaveChangesAsync();
        }

        public void Update(params T[] items)
        {
            foreach (T item in items)
            {
                _context.Entry(item).State = EntityState.Modified;
            }
            //telling EF savechanges task 
            _context.SaveChangesAsync();
        }

        //future iterations, not yet implementated for now
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }
        public IList<T> GetList(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }
    }
}
