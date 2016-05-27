using RecipeShoppingApp.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeShoppingApp.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        RecipeShoppingAppContext Init();
    }
}
