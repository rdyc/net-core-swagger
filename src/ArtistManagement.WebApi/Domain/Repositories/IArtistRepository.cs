using ArtistManagement.WebApi.Domain.Entities;
using ArtistManagement.WebApi.Infrastructure;

namespace ArtistManagement.WebApi.Domain.Repositories
{
    public interface IArtistRepository : IRepository<ArtistEntity>
    {
        void Add(TrackEntity entity);
        void Update(TrackEntity entity);
        void Delete(TrackEntity entity);
    }
}