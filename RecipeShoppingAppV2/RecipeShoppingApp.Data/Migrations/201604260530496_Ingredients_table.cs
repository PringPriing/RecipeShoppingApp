namespace RecipeShoppingApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ingredients_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ingredient",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Serving = c.Int(nullable: false),
                        measurement_ID = c.Int(),
                        Recipe_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Measurement", t => t.measurement_ID)
                .ForeignKey("dbo.Recipe", t => t.Recipe_ID)
                .Index(t => t.measurement_ID)
                .Index(t => t.Recipe_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ingredient", "Recipe_ID", "dbo.Recipe");
            DropForeignKey("dbo.Ingredient", "measurement_ID", "dbo.Measurement");
            DropIndex("dbo.Ingredient", new[] { "Recipe_ID" });
            DropIndex("dbo.Ingredient", new[] { "measurement_ID" });
            DropTable("dbo.Ingredient");
        }
    }
}
