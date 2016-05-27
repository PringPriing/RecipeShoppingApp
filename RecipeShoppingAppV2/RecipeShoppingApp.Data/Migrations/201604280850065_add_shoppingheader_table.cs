namespace RecipeShoppingApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_shoppingheader_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShoppingHeader",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Sun = c.String(),
                        Mon = c.String(),
                        Tue = c.String(),
                        Wed = c.String(),
                        Thu = c.String(),
                        Fri = c.String(),
                        Sat = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ShoppingHeader");
        }
    }
}
