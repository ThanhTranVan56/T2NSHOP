namespace T2NSHOP.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class addReviewProduct : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_Review",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    ProductId = c.Int(nullable: false),
                    UserName = c.String(),
                    FullName = c.String(),
                    Email = c.String(),
                    Content = c.String(),
                    Rate = c.Int(nullable: false),
                    Createdate = c.DateTime(nullable: false),
                    Avata = c.String(),
                    IsActive = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.id);

        }

        public override void Down()
        {
            DropTable("dbo.tb_Review");
        }
    }
}
