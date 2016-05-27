using RecipeShoppingApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeShoppingApp.Data.Configurations
{
    public class ShoppingHeaderConfiguration : EntityBaseConfiguration<ShoppingHeader>
    {
        public ShoppingHeaderConfiguration()
        {
            Property(x => x.Date).IsRequired();
        }
    }
}
