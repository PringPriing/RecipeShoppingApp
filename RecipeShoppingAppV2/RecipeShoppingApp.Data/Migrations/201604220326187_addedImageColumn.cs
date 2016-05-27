namespace RecipeShoppingApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedImageColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipe", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Recipe", "Image");
        }
    }
}
