using SimpleForum.Data.Contracts.Repositories;
using System;
using System.Collections.Generic;
using SimpleForum.Core;
using SimpleForum.Core.Entities;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SimpleForum.Data.Repositories
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ForumDbContext context;
        private readonly DbSet<TEntity> dbSet;

        public RepositoryBase(ForumDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public virtual TEntity Get(int id)
        {
            return dbSet.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression)
        {
            return dbSet.Where(expression).ToList();
        }
    }
}
