namespace T2NSHOP.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using T2NSHOP.Models;

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
