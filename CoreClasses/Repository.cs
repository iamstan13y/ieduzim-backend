using IEduZimAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IEduZimAPI.CoreClasses
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        public IEduContext context { get; }
        public Repository(IEduContext context) => this.context = context;
        public Repository() : this(new IEduContext()) { }

        public virtual IEnumerable<T> Get() => context.Set<T>();

        public virtual T Get<TKey>(TKey id) => context.Set<T>().Find(id);

        public virtual T Add(T item, string user)
        {
            context.Set<T>().Add(item);
            var props = item.GetType().GetProperties();
            foreach (var prop in props)
            {
                if (prop.Name.Equals("DateCreated"))
                    prop.SetValue(item, DateTime.Now);
                if (prop.Name.Equals("CreatedBy"))
                    prop.SetValue(item, user);
                if (prop.Name.Equals("LastDateModified"))
                    prop.SetValue(item, DateTime.Now);
                if (prop.Name.Equals("ModifiedBy"))
                    prop.SetValue(item, user);
            }
            Save();
            return item;
        }

        public virtual void Delete<TKey>(TKey id)
        {
            var dbItem = context.Set<T>().Find(id);
            context.Set<T>().Remove(dbItem);
            Save();
        }

        public virtual T Update<TKey>(TKey id, T item, string user = "")
        {
            var dbItem = context.Set<T>().Find(id);
            if (dbItem == null) throw new Exception("Could not find requested item");
            var props = item.GetType().GetProperties();
            foreach (var prop in props)
            {
                if (prop.Name.Equals("Id")) continue;
                object propValue = prop.GetValue(item);
                prop.SetValue(dbItem, propValue);
                if (prop.Name.Equals("LastDateModified"))
                    prop.SetValue(item, DateTime.Now);
                if (prop.Name.Equals("ModifiedBy"))
                    prop.SetValue(item, user);
            }
            Save();
            return dbItem;
        }

        public virtual Paginator<T> GetPaged(PageRequest request)
        {
            if (request == null) request = new PageRequest() { PageNumber = 1, PageSize = 10 };
            var req = context.Set<T>().AsQueryable<T>();
            if (request.SortParam != null)
                req = Sort(req, request);
            var total = req.CountAsync().Result;
            req = req.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);
            return new Paginator<T>(request, total, req);
        }

        public IQueryable<T> Sort(IQueryable<T> req, PageRequest request)
        {
            var param = Expression.Parameter(typeof(T), "item");
            var sortExpression = Expression.Lambda<Func<T, object>>
                    (Expression.Convert(Expression.Property(param, request.SortParam), typeof(object)), param);
            if (request.Desc)
                req = req.OrderByDescending(sortExpression);
            else req = req.OrderBy(sortExpression);
            return req;
        }

        private void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new Exception(e.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}