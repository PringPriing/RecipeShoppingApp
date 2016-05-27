namespace RecipeShoppingApp.Data.Migrations
{
    using RecipeShoppingApp.Data.Configurations;
    using RecipeShoppingApp.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RecipeShoppingAppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RecipeShoppingAppContext context)
        {

            //Create Roles
            context.RoleSet.AddOrUpdate(r => r.Name, GenerateRoles());

            //Create superUser
            context.UserSet.AddOrUpdate(u => u.Email, new User[]{
                new User()
                {
                    Email = "superuser@superuser.com",
                    UserName ="superuser",
                    HashedPassword ="wbXg1fiu0x5QRPerxOyzF4LOcyMiF3Myo/4/Y4VmtpQ=",
                    Salt = "G5vMoEsCjBmeTOG4covCEg==",
                    IsLocked = false,
                    DateCreated = DateTime.Now
                }
            });
            //Create Role
            context.UserRoleSet.AddOrUpdate(new UserRole[]{
                new UserRole()
                {
                    RoleId = 1,
                    UserId =1
                }
            });

            //Create Measurements
            context.MeasurementSet.AddOrUpdate(new Measurement[]
            {
                new Measurement(){Name ="kilogram"},
                new Measurement(){Name ="ounce"},
                new Measurement(){Name ="grams"},
                new Measurement(){Name ="tbsp"},
                new Measurement(){Name ="slice"},
                new Measurement(){Name ="piece"},
                new Measurement(){Name ="diced"},
                new Measurement(){Name ="cup"},
                new Measurement(){Name ="can"}
            });
            //Create Recipes
            context.RecipeSet.AddOrUpdate(new Recipe[]
            {
                new Recipe(){RecipeName="Chicken Adobo",Serving=1,Description="Main course",DateCreated=DateTime.Now,Image="download (5).jpg",Rating=2},
                new Recipe(){RecipeName="Pork BBQ",Serving=12,Description="Main course",DateCreated=DateTime.Now,Image="images (1).jpg",Rating=2},
                new Recipe(){RecipeName="Pork steak",Serving=1,Description="Main course",DateCreated=DateTime.Now,Image="images (2).jpg",Rating=2},
                new Recipe(){RecipeName="Beef steak",Serving=1,Description="Main course",DateCreated=DateTime.Now,Image="4.jpg",Rating=4},
                new Recipe(){RecipeName="broccoli-spinach soup with avocado toasts",Serving=1,Description="Main course",DateCreated=DateTime.Now,Image="broccoli-spinach-soup-with-avocado-toasts-ss.jpg",Rating=2},
                new Recipe(){RecipeName="cauliflower steaks",Serving=1,Description="Main course",DateCreated=DateTime.Now,Image="cauliflower-steaks-with-roasted-pepper-and-tomato-salad-ss.jpg",Rating=3},
                new Recipe(){RecipeName="brownies",Serving=1,Description="Dessert",DateCreated=DateTime.Now,Image="carousel2.jpg",Rating=2},
                new Recipe(){RecipeName="Fish Fillet",Serving=3,Description="Dessert",DateCreated=DateTime.Now,Image="1.jpg",Rating=1},
                new Recipe(){RecipeName="Meat Nirvana",Serving=4,Description="Dessert",DateCreated=DateTime.Now,Image="397070.jpg",Rating=5},
                new Recipe(){RecipeName="Ellen's Chicken Cacciatore",Serving=1,Description="Dessert",DateCreated=DateTime.Now,Image="822122.jpg",Rating=5},

            });

            context.SaveChanges();

            //Create Ingredients
            context.IngredientSet.AddOrUpdate(new Ingredient[]
            {
                //chicken adobo 
                new Ingredient(){Name="Chicken",MeasurementID=1,RecipeID=1,Serving=1},
                new Ingredient(){Name="soy sauce",MeasurementID=4,RecipeID=1,Serving=2},
                new Ingredient(){Name="Garlic",MeasurementID=7,RecipeID=1,Serving=1},
                //Pork BBQ
                new Ingredient(){Name="Pork",MeasurementID=1,RecipeID=2,Serving=1},
                new Ingredient(){Name="soy sauce",MeasurementID=4,RecipeID=2,Serving=2},
                new Ingredient(){Name="Garlic",MeasurementID=7,RecipeID=2,Serving=1},
                //Pork Steak
                new Ingredient(){Name="Pork",MeasurementID=1,RecipeID=3,Serving=1},
                new Ingredient(){Name="soy sauce",MeasurementID=4,RecipeID=3,Serving=2},
                new Ingredient(){Name="Garlic",MeasurementID=7,RecipeID=3,Serving=1},
                new Ingredient(){Name="Onions",MeasurementID=7,RecipeID=3,Serving=1},
            });
        }


        private Entities.Role[] GenerateRoles()
        {
            Role[] roles = new Role[]{
                new Role()
                {
                    Name ="Admin"
                },
                new Role()
                {
                    Name="Basic"
                }
            };

            return roles;
        }
    }
}
