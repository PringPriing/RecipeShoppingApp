namespace RecipeShoppingApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMeasurementTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Measurement",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Measurement");
        }
    }
}
