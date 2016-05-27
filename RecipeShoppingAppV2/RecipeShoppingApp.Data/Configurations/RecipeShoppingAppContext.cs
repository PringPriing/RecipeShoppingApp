using RecipeShoppingApp.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeShoppingApp.Data.Configurations
{
    public class RecipeShoppingAppContext : DbContext
    {
        public RecipeShoppingAppContext()
            :base("RecipeShoppingDB")
        {
            Database.SetInitializer<RecipeShoppingAppContext>(null);
        }

        public IDbSet<User> UserSet { get; set; }
        public IDbSet<Role> RoleSet { get; set; }
        public IDbSet<UserRole> UserRoleSet { get; set; }
        public IDbSet<Recipe> RecipeSet { get; set; }
        public IDbSet<Measurement> MeasurementSet { get; set; }
        public IDbSet<Ingredient> IngredientSet { get; set; }
        public IDbSet<ShoppingHeader> ShoppingHeaderSet { get; set; }


        public virtual void  Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new UserRoleConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new RecipeConfiguration());
            modelBuilder.Configurations.Add(new MeasurementConfiguration());
            modelBuilder.Configurations.Add(new IngredientsConfiguration());
            modelBuilder.Configurations.Add(new ShoppingHeaderConfiguration());

        }

    }
}
