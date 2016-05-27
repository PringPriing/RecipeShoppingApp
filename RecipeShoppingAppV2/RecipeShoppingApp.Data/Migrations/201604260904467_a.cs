namespace RecipeShoppingApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Ingredient", new[] { "Recipe_ID" });
            CreateIndex("dbo.Ingredient", "recipe_ID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Ingredient", new[] { "recipe_ID" });
            CreateIndex("dbo.Ingredient", "Recipe_ID");
        }
    }
}
