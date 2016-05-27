using RecipeShoppingApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeShoppingApp.Data.Configurations
{
    class RoleConfiguration : EntityBaseConfiguration<Role>
    {
        public RoleConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(50);
        }
    }
}
