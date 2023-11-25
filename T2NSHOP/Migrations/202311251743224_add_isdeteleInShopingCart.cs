namespace T2NSHOP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_isdeteleInShopingCart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_ShoppingCart", "IsDelete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_ShoppingCart", "IsDelete");
        }
    }
}
