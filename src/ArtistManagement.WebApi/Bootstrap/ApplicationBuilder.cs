using System;
using System.Collections.Generic;
using System.Linq;
using ArtistManagement.WebApi.Domain;
using ArtistManagement.WebApi.Domain.Entities;
using Bogus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace ArtistManagement.WebApi.Bootstrap
{
    public static class ApplicationBuilder
    {
        public static IApplicationBuilder UseDbMigration(this IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ArtistDbContext>();
                
                using (dbContext)
                {
                    // run db migration, this will creating a new db if not exist or updating the changes
                    dbContext.Database.Migrate();

                    // seeding data
                    SeedArtists(dbContext);
                    // SeedArtists(dbContext.Artists);

                    // save changes
                    dbContext.SaveChanges();
                }
            }

            return app;
        }

        public static IApplicationBuilder UseMySwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUI(c =>
                {
                    // build a swagger endpoint for each discovered API version
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"API {description.GroupName.ToUpperInvariant()} Docs");
                    }

                    c.DocumentTitle = "JOOXTify API";
                    c.RoutePrefix = string.Empty;
                    c.DefaultModelRendering(ModelRendering.Model);
                    c.DisplayRequestDuration();
                    c.DocExpansion(DocExpansion.None);
                    c.EnableDeepLinking();
                    c.EnableFilter();
                });

            return app;
        }

        private static void SeedArtists(ArtistDbContext context)
        {
            var faker = new Faker();

            var nationalities = new [] { "Argentina", "Australia", "Austria", "Brazil", "Canada", "Denmark", "England", "France", "Germany", "Indonesia", "Italy", "Malaysia", "Rusia", "United States", "Zimbabwe" };
            var genres = new []{ "Alternative", "Anime", "Blues", "Classical", "Country", "Dance", "Hip-hop", "Pop", "Rock", "Jazz", "R&B/Soul", "Reggae" };
            
            if (!context.Artists.Any())
            {
                for (int i = 0; i < 100; i++)
                {
                    // add artists
                    var artist = new ArtistEntity(
                        name: faker.Person.FullName,
                        nationality: faker.PickRandom(nationalities)
                    );

                    context.Entry(artist).State = EntityState.Added;

                    // add artist tracks
                    IList<string> trackIds = new List<string>();
                    for (int j = 0; j < faker.Random.Int(3, 10); j++)
                    {
                        var track = artist.AddTrack(
                            title: faker.Random.Words(3),
                            genre: faker.PickRandom(genres),
                            duration: TimeSpan.FromSeconds(faker.Random.Long(120, 380))
                        );

                        trackIds.Add(track.Id);
                        
                        context.Entry(track).State = EntityState.Added;
                    }

                    // add albums
                    var album = new AlbumEntity(
                        name: faker.Random.Words(2), 
                        release: faker.Date.Between(DateTime.Now.AddYears(-10).Date, DateTime.Now.Date)
                    );

                    context.Entry(album).State = EntityState.Added;

                    // add album tracks
                    foreach (var item in trackIds)
                    {
                        var albumTrack = album.AddTrack(trackId: item);

                        context.Entry(albumTrack).State = EntityState.Added;
                    }
                }
            }
        }

        private static void SeedArtists(DbSet<ArtistEntity> entity)
        {
            if (!entity.Any())
            {
                var artist = new Faker<ArtistEntity>()
                    .RuleFor(u => u.Id, f => Guid.NewGuid().ToString())
                    .RuleFor(u => u.Name, f => f.Person.FullName)
                    .RuleFor(u => u.Nationality, f => f.Person.Address.State);

                var data = artist.Generate(200);
                    
                entity.AddRange(data);
            }
        }
    }
}