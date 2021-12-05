namespace _1911205_Lab09_NguyenHuuDucThanh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFood : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Food",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Unit = c.String(),
                        FoodCategoryId = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.FoodCategoryId, cascadeDelete: true)
                .Index(t => t.FoodCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Food", "FoodCategoryId", "dbo.Category");
            DropIndex("dbo.Food", new[] { "FoodCategoryId" });
            DropTable("dbo.Food");
            DropTable("dbo.Category");
        }
    }
}
