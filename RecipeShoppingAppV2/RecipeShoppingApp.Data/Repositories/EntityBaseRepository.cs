using RecipeShoppingApp.Data.Configurations;
using RecipeShoppingApp.Data.Infrastructure;
using RecipeShoppingApp.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RecipeShoppingApp.Data.Repositories
{
    public class EntityBaseRepository<T>: IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private RecipeShoppingAppContext dataContext;
        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected RecipeShoppingAppContext DbContext
        {
            get{ return dataContext ?? (dataContext = DbFactory.Init());}
        }

        public EntityBaseRepository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
        }

        public virtual IQueryable<T> GetAll()
        {
            return DbContext.Set<T>();
        }

        public virtual IQueryable<T> All
        {
            get
            {
                return GetAll();
            }
        }

        public virtual IQueryable<T> AllIncluding(params Expression<Func<T,object>>[] includeproperties)
        {
            IQueryable<T> query = DbContext.Set<T>();
            foreach(var includeProperty in includeproperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public T GetSingle(int id)
        {
            return GetAll().FirstOrDefault(x => x.ID == id);
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T,bool>> predicate)
        {
            return DbContext.Set<T>().Where(predicate);
        }

        public virtual void Add(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry<T>(entity);
            DbContext.Set<T>().Add(entity);
        }

        public virtual void Edit(T entity)
        {
            DbEntityEntry dbEntity = DbContext.Entry<T>(entity);
            dbEntity.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted; ;
        }


    }
}
