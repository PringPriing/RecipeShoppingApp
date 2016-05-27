namespace RecipeShoppingApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedRating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipe", "Rating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Recipe", "Rating");
        }
    }
}
