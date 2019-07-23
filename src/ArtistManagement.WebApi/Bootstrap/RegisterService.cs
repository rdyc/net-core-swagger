using ArtistManagement.WebApi.Application.Services;
using ArtistManagement.WebApi.Domain;
using ArtistManagement.WebApi.Domain.Entities;
using ArtistManagement.WebApi.Domain.Repositories;
using ArtistManagement.WebApi.V1.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArtistManagement.WebApi.Bootstrap
{
    public static class RegisterService
    {
        public static IServiceCollection AddArtistManagement(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            AddApplicationService(services);
            AddAutoMapper(services);

            return services;
        }

        private static IServiceCollection AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ArtistDbContext>(options => 
                options
                    .UseSqlite(configuration.GetConnectionString("ArtistDbContext"))
                    .EnableSensitiveDataLogging(false)
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            );

            return services;
        }

        private static IServiceCollection AddApplicationService(IServiceCollection services)
        {
            services.AddTransient<IArtistService, ArtistService>();
            services.AddTransient<ITrackService, TrackService>();
            services.AddTransient<IAlbumService, AlbumService>();

            services.AddTransient<IArtistRepository, ArtistRepository>();
            services.AddTransient<IAlbumRepository, AlbumRepository>();

            return services;
        }

        private static IServiceCollection AddAutoMapper(IServiceCollection services)
        {
            var config = new MapperConfiguration(options => {
                options.AddProfile<ResponseMapperProfile<ArtistEntity, ArtistModel>>();
                options.AddProfile<ResponseMapperProfile<TrackEntity, TrackModel>>();
                options.AddProfile<ResponseMapperProfile<AlbumEntity, AlbumModel>>();

                options.AddProfile<CustomMapperProfile>();
            });

            // config.AssertConfigurationIsValid();
            
            services.AddSingleton<IMapper>(config.CreateMapper());

            return services;
        }
    }
}