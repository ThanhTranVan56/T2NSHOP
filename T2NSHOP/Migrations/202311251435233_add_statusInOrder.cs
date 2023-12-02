namespace T2NSHOP.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class add_statusInOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Order", "StatusOrder", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.tb_Order", "StatusOrder");
        }
    }
}
