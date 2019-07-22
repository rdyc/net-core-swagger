using System;
using System.Linq;
using ArtistManagement.WebApi.Domain;
using ArtistManagement.WebApi.Domain.Entities;
using Bogus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ArtistManagement.WebApi.Application
{
    public static class ArtistBuilder
    {
        public static IApplicationBuilder UseArtistMigration(this IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ArtistDbContext>();
                
                using (dbContext)
                {
                    // run db migration, this will creating a new db if not exist or updating the changes
                    dbContext.Database.Migrate();

                    // seeding data
                    SeedArtists(dbContext.Artists);

                    // save changes
                    dbContext.SaveChanges();
                }
            }

            return app;
        }

        private static void SeedArtists(DbSet<ArtistEntity> entity)
        {
            if (!entity.Any())
            {
                var faker = new Faker<ArtistEntity>()
                    .RuleFor(u => u.Id, f => Guid.NewGuid().ToString())
                    .RuleFor(u => u.Name, f => f.Person.FullName)
                    .RuleFor(u => u.Nationality, f => f.Person.Address.State);

                var data = faker.Generate(200);
                    
                entity.AddRange(data);
            }
        }
    }
}