using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity;
using TrelloMVC.Models;

namespace TrelloMVC.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "TrelloMVC.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            /*var passwordHash = new PasswordHasher();
              context.Users.AddOrUpdate(
                  new ApplicationUser { UserName = "JPLC",  PasswordHash = passwordHash.HashPassword("jplc1989!") },
                  new ApplicationUser { UserName = "Caetano",    PasswordHash = passwordHash.HashPassword("jplc1989!") },
                  new ApplicationUser { UserName = "Pedro",  PasswordHash = passwordHash.HashPassword("jplc1989!") },
                  new ApplicationUser { UserName = "Maria",  PasswordHash = passwordHash.HashPassword("jplc1989!") }
                );*/
            //
        }
    }
}
