using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using SIMS.Models;

namespace SIMS.Data.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class
    {
        private SIMSWebEntities dataContext;
        private readonly DbSet<T> dbSet;

        protected IDbFactory DbFactory { get; private set; }

        protected SIMSWebEntities DbContext => this.dataContext ?? (this.dataContext = this.DbFactory.Init());

        protected RepositoryBase(IDbFactory dbFactory)
        {
            this.DbFactory = dbFactory;
            this.dbSet = this.DbContext.Set<T>();
        }

        public virtual void Add(T entity) => this.dbSet.Add(entity);

        public virtual void Update(T entity)
        {
            this.dbSet.Attach(entity);
            this.dataContext.Entry<T>(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity) => this.dbSet.Remove(entity);

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            foreach (T entity in this.dbSet.Where<T>(where).AsEnumerable<T>())
                this.dbSet.Remove(entity);
        }

        public virtual T GetById(Decimal id) => this.dbSet.Find(new object[1]
        {
            (object) id
        });

        public virtual IEnumerable<T> GetAll() => (IEnumerable<T>)this.dbSet.ToList<T>();

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where) => (IEnumerable<T>)this.dbSet.Where<T>(where).ToList<T>();

        public T Get(Expression<Func<T, bool>> where) => this.dbSet.Where<T>(where).FirstOrDefault<T>();
    }
}
