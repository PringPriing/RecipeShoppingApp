using RecipeShoppingApp.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeShoppingApp.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        RecipeShoppingAppContext dbContext;
        public Configurations.RecipeShoppingAppContext Init()
        {
            return dbContext ?? (dbContext = new RecipeShoppingAppContext());
        }

        protected virtual void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
