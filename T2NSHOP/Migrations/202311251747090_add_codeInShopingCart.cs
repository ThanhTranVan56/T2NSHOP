namespace T2NSHOP.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class add_codeInShopingCart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_ShoppingCart", "Code", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.tb_ShoppingCart", "Code");
        }
    }
}
