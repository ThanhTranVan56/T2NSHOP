namespace T2NSHOP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class beginDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_AddressCustomer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProfileId = c.Int(nullable: false),
                        UserId = c.String(),
                        Name = c.String(),
                        Address = c.String(),
                        AddressDetail = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        IsDefault = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_ProfileCustomer", t => t.ProfileId, cascadeDelete: true)
                .Index(t => t.ProfileId);
            
            CreateTable(
                "dbo.tb_ProfileCustomer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Image = c.String(),
                        UserName = c.String(),
                        Name = c.String(),
                        Gender = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        TypePayment = c.Int(nullable: false),
                        TypePaymentVN = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tb_Adv",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titel = c.String(nullable: false, maxLength: 150),
                        Decription = c.String(maxLength: 500),
                        Image = c.String(maxLength: 500),
                        Type = c.Int(nullable: false),
                        Link = c.String(maxLength: 500),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifedrDate = c.DateTime(nullable: false),
                        ModifierBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tb_Categogy",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titel = c.String(nullable: false, maxLength: 150),
                        Alias = c.String(),
                        Decription = c.String(),
                        SeoTitel = c.String(maxLength: 150),
                        SeoDecription = c.String(maxLength: 250),
                        SeoKeywords = c.String(),
                        Possition = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifedrDate = c.DateTime(nullable: false),
                        ModifierBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tb_News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        Alias = c.String(),
                        Decription = c.String(),
                        Detail = c.String(),
                        Image = c.String(),
                        CategoryID = c.Int(nullable: false),
                        SeoTitel = c.String(),
                        SeoDecription = c.String(),
                        SeoKeywords = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifedrDate = c.DateTime(nullable: false),
                        ModifierBy = c.String(),
                        Categogy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_Categogy", t => t.Categogy_Id)
                .Index(t => t.Categogy_Id);
            
            CreateTable(
                "dbo.tb_Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        Alias = c.String(maxLength: 150),
                        Decription = c.String(),
                        Detail = c.String(),
                        Image = c.String(maxLength: 250),
                        ViewCount = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                        SeoTitel = c.String(),
                        SeoDecription = c.String(maxLength: 500),
                        SeoKeywords = c.String(maxLength: 200),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifedrDate = c.DateTime(nullable: false),
                        ModifierBy = c.String(),
                        Categogy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_Categogy", t => t.Categogy_Id)
                .Index(t => t.Categogy_Id);
            
            CreateTable(
                "dbo.tb_Contact",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Email = c.String(maxLength: 150),
                        Website = c.String(),
                        Message = c.String(maxLength: 4000),
                        IsRead = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifedrDate = c.DateTime(nullable: false),
                        ModifierBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tb_OrderDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_Order", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.tb_Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.tb_Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        CustomerName = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Email = c.String(),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        TypePayment = c.Int(nullable: false),
                        StatusPayment = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifedrDate = c.DateTime(nullable: false),
                        ModifierBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tb_Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 250),
                        Alias = c.String(maxLength: 250),
                        ProductCode = c.String(maxLength: 50),
                        Decription = c.String(),
                        Detail = c.String(),
                        Image = c.String(maxLength: 250),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OriginalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PriceSale = c.Decimal(precision: 18, scale: 2),
                        ViewCount = c.Int(nullable: false),
                        IsHome = c.Boolean(nullable: false),
                        IsSale = c.Boolean(nullable: false),
                        IsFeature = c.Boolean(nullable: false),
                        IsHot = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        ProductCategoryID = c.Int(nullable: false),
                        SeoTitel = c.String(maxLength: 250),
                        SeoDecription = c.String(maxLength: 500),
                        SeoKeywords = c.String(maxLength: 250),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifedrDate = c.DateTime(nullable: false),
                        ModifierBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_ProductCategory", t => t.ProductCategoryID, cascadeDelete: true)
                .Index(t => t.ProductCategoryID);
            
            CreateTable(
                "dbo.tb_ProductAttri",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Color = c.String(),
                        Alias = c.String(),
                        ProductId = c.Int(nullable: false),
                        Size = c.String(),
                        Quantity = c.Int(nullable: false),
                        PriceOverValue = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.tb_ProductCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titel = c.String(nullable: false, maxLength: 150),
                        Alias = c.String(nullable: false, maxLength: 150),
                        Decription = c.String(maxLength: 500),
                        Icon = c.String(maxLength: 250),
                        SeoTitel = c.String(maxLength: 250),
                        SeoDecription = c.String(maxLength: 500),
                        SeoKeywords = c.String(maxLength: 250),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifedrDate = c.DateTime(nullable: false),
                        ModifierBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tb_ProductImage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Color = c.String(),
                        Image = c.String(),
                        IsDefault = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.tb_ShoppingCart",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        IdUser = c.String(),
                        ProductName = c.String(),
                        Alias = c.String(),
                        CategoryName = c.String(),
                        ProductImg = c.String(),
                        Color = c.String(),
                        Size = c.String(),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsActive = c.Boolean(nullable: false),
                        IsPay = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tb_Subscribe",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tb_SystemSetting",
                c => new
                    {
                        SettingKey = c.String(nullable: false, maxLength: 50),
                        SettingValue = c.String(maxLength: 4000),
                        SettingDescription = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.SettingKey);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Fullname = c.String(),
                        Phone = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.tb_ProductImage", "ProductId", "dbo.tb_Product");
            DropForeignKey("dbo.tb_Product", "ProductCategoryID", "dbo.tb_ProductCategory");
            DropForeignKey("dbo.tb_ProductAttri", "ProductId", "dbo.tb_Product");
            DropForeignKey("dbo.tb_OrderDetail", "ProductId", "dbo.tb_Product");
            DropForeignKey("dbo.tb_OrderDetail", "OrderId", "dbo.tb_Order");
            DropForeignKey("dbo.tb_Posts", "Categogy_Id", "dbo.tb_Categogy");
            DropForeignKey("dbo.tb_News", "Categogy_Id", "dbo.tb_Categogy");
            DropForeignKey("dbo.tb_AddressCustomer", "ProfileId", "dbo.tb_ProfileCustomer");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.tb_ProductImage", new[] { "ProductId" });
            DropIndex("dbo.tb_ProductAttri", new[] { "ProductId" });
            DropIndex("dbo.tb_Product", new[] { "ProductCategoryID" });
            DropIndex("dbo.tb_OrderDetail", new[] { "ProductId" });
            DropIndex("dbo.tb_OrderDetail", new[] { "OrderId" });
            DropIndex("dbo.tb_Posts", new[] { "Categogy_Id" });
            DropIndex("dbo.tb_News", new[] { "Categogy_Id" });
            DropIndex("dbo.tb_AddressCustomer", new[] { "ProfileId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.tb_SystemSetting");
            DropTable("dbo.tb_Subscribe");
            DropTable("dbo.tb_ShoppingCart");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.tb_ProductImage");
            DropTable("dbo.tb_ProductCategory");
            DropTable("dbo.tb_ProductAttri");
            DropTable("dbo.tb_Product");
            DropTable("dbo.tb_Order");
            DropTable("dbo.tb_OrderDetail");
            DropTable("dbo.tb_Contact");
            DropTable("dbo.tb_Posts");
            DropTable("dbo.tb_News");
            DropTable("dbo.tb_Categogy");
            DropTable("dbo.tb_Adv");
            DropTable("dbo.tb_ProfileCustomer");
            DropTable("dbo.tb_AddressCustomer");
        }
    }
}
