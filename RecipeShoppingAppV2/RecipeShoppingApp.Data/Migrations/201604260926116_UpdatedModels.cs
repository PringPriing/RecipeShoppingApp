namespace RecipeShoppingApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ingredient", "measurement_ID", "dbo.Measurement");
            DropForeignKey("dbo.Ingredient", "recipe_ID", "dbo.Recipe");
            DropIndex("dbo.Ingredient", new[] { "measurement_ID" });
            DropIndex("dbo.Ingredient", new[] { "recipe_ID" });
            RenameColumn(table: "dbo.Ingredient", name: "measurement_ID", newName: "MeasurementID");
            RenameColumn(table: "dbo.Ingredient", name: "recipe_ID", newName: "RecipeID");
            AlterColumn("dbo.Ingredient", "MeasurementID", c => c.Int(nullable: false));
            AlterColumn("dbo.Ingredient", "RecipeID", c => c.Int(nullable: false));
            CreateIndex("dbo.Ingredient", "MeasurementID");
            CreateIndex("dbo.Ingredient", "RecipeID");
            AddForeignKey("dbo.Ingredient", "MeasurementID", "dbo.Measurement", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Ingredient", "RecipeID", "dbo.Recipe", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ingredient", "RecipeID", "dbo.Recipe");
            DropForeignKey("dbo.Ingredient", "MeasurementID", "dbo.Measurement");
            DropIndex("dbo.Ingredient", new[] { "RecipeID" });
            DropIndex("dbo.Ingredient", new[] { "MeasurementID" });
            AlterColumn("dbo.Ingredient", "RecipeID", c => c.Int());
            AlterColumn("dbo.Ingredient", "MeasurementID", c => c.Int());
            RenameColumn(table: "dbo.Ingredient", name: "RecipeID", newName: "recipe_ID");
            RenameColumn(table: "dbo.Ingredient", name: "MeasurementID", newName: "measurement_ID");
            CreateIndex("dbo.Ingredient", "recipe_ID");
            CreateIndex("dbo.Ingredient", "measurement_ID");
            AddForeignKey("dbo.Ingredient", "recipe_ID", "dbo.Recipe", "ID");
            AddForeignKey("dbo.Ingredient", "measurement_ID", "dbo.Measurement", "ID");
        }
    }
}
