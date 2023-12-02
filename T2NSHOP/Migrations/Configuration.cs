namespace T2NSHOP.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<T2NSHOP.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(T2NSHOP.Models.ApplicationDbContext context)
        {

        }

    }
}
