using RecipeShoppingApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeShoppingApp.Data.Configurations
{
   public class RecipeConfiguration : EntityBaseConfiguration<Recipe>
    {
       public RecipeConfiguration()
       {
           Property(r => r.RecipeName).IsRequired().HasMaxLength(50);
           Property(r => r.Serving).IsRequired();
       }
    }
}
