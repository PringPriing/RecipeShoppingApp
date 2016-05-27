using RecipeShoppingApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeShoppingApp.Data.Configurations
{
    public class IngredientsConfiguration : EntityBaseConfiguration<Ingredient>
    {
        public IngredientsConfiguration()
        {
            Property(i => i.Name).IsRequired().HasMaxLength(30);
            Property(i => i.Serving).IsRequired();
        }
    }
}
