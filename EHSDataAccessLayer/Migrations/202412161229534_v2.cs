namespace EHSDataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "BuyerId", "dbo.Buyers");
            DropForeignKey("dbo.Carts", "PropertyId", "dbo.Properties");
            DropIndex("dbo.Carts", new[] { "BuyerId" });
            DropIndex("dbo.Carts", new[] { "PropertyId" });
            CreateTable(
                "dbo.PropertyCarts",
                c => new
                    {
                        Property_PropertyId = c.Int(nullable: false),
                        Cart_CartId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Property_PropertyId, t.Cart_CartId })
                .ForeignKey("dbo.Properties", t => t.Property_PropertyId, cascadeDelete: true)
                .ForeignKey("dbo.Carts", t => t.Cart_CartId, cascadeDelete: true)
                .Index(t => t.Property_PropertyId)
                .Index(t => t.Cart_CartId);
            
            AddColumn("dbo.Buyers", "UserName", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.Buyers", "Cart_CartId", c => c.Int());
            CreateIndex("dbo.Buyers", "Cart_CartId");
            AddForeignKey("dbo.Buyers", "Cart_CartId", "dbo.Carts", "CartId");
            DropColumn("dbo.Carts", "PropertyId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "PropertyId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Buyers", "Cart_CartId", "dbo.Carts");
            DropForeignKey("dbo.PropertyCarts", "Cart_CartId", "dbo.Carts");
            DropForeignKey("dbo.PropertyCarts", "Property_PropertyId", "dbo.Properties");
            DropIndex("dbo.PropertyCarts", new[] { "Cart_CartId" });
            DropIndex("dbo.PropertyCarts", new[] { "Property_PropertyId" });
            DropIndex("dbo.Buyers", new[] { "Cart_CartId" });
            DropColumn("dbo.Buyers", "Cart_CartId");
            DropColumn("dbo.Buyers", "UserName");
            DropTable("dbo.PropertyCarts");
            CreateIndex("dbo.Carts", "PropertyId");
            CreateIndex("dbo.Carts", "BuyerId");
            AddForeignKey("dbo.Carts", "PropertyId", "dbo.Properties", "PropertyId", cascadeDelete: true);
            AddForeignKey("dbo.Carts", "BuyerId", "dbo.Buyers", "BuyerId", cascadeDelete: true);
        }
    }
}
