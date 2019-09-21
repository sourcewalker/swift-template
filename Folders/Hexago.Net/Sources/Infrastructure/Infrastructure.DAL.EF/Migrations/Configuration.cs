using Core.Model;
using System;
using System.Data.Entity.Migrations;
using System.Linq;

namespace $safeprojectname$.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DatabaseContext context)
        {
            var usId = default(Guid);
            var ukId = default(Guid);
            var ieId = default(Guid);
            var deId = default(Guid);
            var atId = default(Guid);

            #region Site seeding

            if (!context.Sites.Any(p => p.Name == "US"))
            {
                usId = Guid.NewGuid();

                context.Sites.AddOrUpdate(new Site()
                {
                    Id = usId,
                    Culture = "en-US",
                    Name = "US",
                    Domain = ".com",
                    CreatedDate = DateTimeOffset.UtcNow
                });
            }

            if (!context.Sites.Any(p => p.Name == "UK"))
            {
                ukId = Guid.NewGuid();
                
                context.Sites.AddOrUpdate(new Site()
                {
                    Id = ukId,
                    Culture = "en-GB",
                    Name = "UK",
                    Domain = ".co.uk",
                    CreatedDate = DateTimeOffset.UtcNow
                });
            }

            if (!context.Sites.Any(p => p.Name == "IE"))
            {
                ieId = Guid.NewGuid();

                context.Sites.AddOrUpdate(new Site()
                {
                    Id = ieId,
                    Culture = "en-IE",
                    Name = "IE",
                    Domain = ".ie",
                    CreatedDate = DateTimeOffset.UtcNow
                });
            }

            if (!context.Sites.Any(p => p.Name == "DE"))
            {
                deId = Guid.NewGuid();

                context.Sites.AddOrUpdate(new Site()
                {
                    Id = deId,
                    Culture = "de-DE",
                    Name = "DE",
                    Domain = ".de",
                    CreatedDate = DateTimeOffset.UtcNow
                });
            }

            if (!context.Sites.Any(p => p.Name == "AT"))
            {
                atId = Guid.NewGuid();

                context.Sites.AddOrUpdate(new Site()
                {
                    Id = atId,
                    Culture = "de-AT",
                    Name = "AT",
                    Domain = ".at",
                    CreatedDate = DateTimeOffset.UtcNow
                });
            }

            #endregion

            context.SaveChanges();
        }
    }
}
