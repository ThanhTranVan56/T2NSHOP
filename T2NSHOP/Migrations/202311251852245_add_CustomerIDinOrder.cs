namespace T2NSHOP.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class add_CustomerIDinOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Order", "CustomerId", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.tb_Order", "CustomerId");
        }
    }
}
