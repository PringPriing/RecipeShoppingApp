using RecipeShoppingApp.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeShoppingApp.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private RecipeShoppingAppContext dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public RecipeShoppingAppContext DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }
   

        public void Commit()
        {
            DbContext.Commit();
        }
    }
}
