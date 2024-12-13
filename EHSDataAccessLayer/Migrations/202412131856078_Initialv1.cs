namespace EHSDataAccessLayer.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initialv1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buyers",
                c => new
                {
                    BuyerId = c.Int(nullable: false, identity: true),
                    FirstName = c.String(nullable: false, maxLength: 25),
                    LastName = c.String(maxLength: 25),
                    DateOfBirth = c.DateTime(nullable: false),
                    PhoneNo = c.String(nullable: false, maxLength: 10),
                    EmailId = c.String(nullable: false, maxLength: 50),
                })
                .PrimaryKey(t => t.BuyerId);

            CreateTable(
                "dbo.Carts",
                c => new
                {
                    CartId = c.Int(nullable: false, identity: true),
                    BuyerId = c.Int(nullable: false),
                    PropertyId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.CartId)
                .ForeignKey("dbo.Buyers", t => t.BuyerId, cascadeDelete: true)
                .ForeignKey("dbo.Properties", t => t.PropertyId, cascadeDelete: true)
                .Index(t => t.BuyerId)
                .Index(t => t.PropertyId);

            CreateTable(
                "dbo.Properties",
                c => new
                {
                    PropertyId = c.Int(nullable: false, identity: true),
                    PropertyName = c.String(nullable: false, maxLength: 50),
                    PropertyType = c.String(nullable: false, maxLength: 15),
                    PropertyOption = c.String(nullable: false, maxLength: 10),
                    Description = c.String(maxLength: 250),
                    Address = c.String(nullable: false, maxLength: 250),
                    PriceRange = c.Decimal(nullable: false, precision: 18, scale: 2),
                    InitialDeposit = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Landmark = c.String(nullable: false, maxLength: 25),
                    IsActive = c.Boolean(nullable: false),
                    SellerId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.PropertyId)
                .ForeignKey("dbo.Sellers", t => t.SellerId, cascadeDelete: true)
                .Index(t => t.SellerId);

            CreateTable(
                "dbo.Sellers",
                c => new
                {
                    SellerId = c.Int(nullable: false, identity: true),
                    UserName = c.String(nullable: false, maxLength: 25),
                    FirstName = c.String(nullable: false, maxLength: 25),
                    LastName = c.String(maxLength: 25),
                    DateOfBirth = c.DateTime(nullable: false),
                    PhoneNo = c.String(nullable: false, maxLength: 10),
                    Address = c.String(nullable: false, maxLength: 250),
                    StateId = c.Int(nullable: false),
                    CityId = c.Int(nullable: false),
                    EmailId = c.String(nullable: false, maxLength: 50),
                })
                .PrimaryKey(t => t.SellerId)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: false) // Changed to prevent cascade delete
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: false) // Changed to prevent cascade delete
                .Index(t => t.StateId)
                .Index(t => t.CityId);

            CreateTable(
                "dbo.Cities",
                c => new
                {
                    CityId = c.Int(nullable: false, identity: true),
                    CityName = c.String(nullable: false, maxLength: 50),
                    StateId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: false) // Changed to prevent cascade delete
                .Index(t => t.StateId);

            CreateTable(
                "dbo.States",
                c => new
                {
                    StateId = c.Int(nullable: false, identity: true),
                    StateName = c.String(nullable: false, maxLength: 30),
                })
                .PrimaryKey(t => t.StateId);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    UserName = c.String(nullable: false, maxLength: 25),
                    Password = c.String(nullable: false, maxLength: 25),
                    UserType = c.String(nullable: false, maxLength: 15),
                })
                .PrimaryKey(t => t.UserName);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Sellers", "StateId", "dbo.States");
            DropForeignKey("dbo.Properties", "SellerId", "dbo.Sellers");
            DropForeignKey("dbo.Sellers", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Cities", "StateId", "dbo.States");
            DropForeignKey("dbo.Carts", "PropertyId", "dbo.Properties");
            DropForeignKey("dbo.Carts", "BuyerId", "dbo.Buyers");
            DropIndex("dbo.Cities", new[] { "StateId" });
            DropIndex("dbo.Sellers", new[] { "CityId" });
            DropIndex("dbo.Sellers", new[] { "StateId" });
            DropIndex("dbo.Properties", new[] { "SellerId" });
            DropIndex("dbo.Carts", new[] { "PropertyId" });
            DropIndex("dbo.Carts", new[] { "BuyerId" });
            DropTable("dbo.Users");
            DropTable("dbo.States");
            DropTable("dbo.Cities");
            DropTable("dbo.Sellers");
            DropTable("dbo.Properties");
            DropTable("dbo.Carts");
            DropTable("dbo.Buyers");
        }
    }
}
