using ArtistManagement.WebApi.Domain.Entities;
using ArtistManagement.WebApi.Infrastructure;

namespace ArtistManagement.WebApi.Domain.Repositories
{
    public interface IAlbumRepository : IRepository<AlbumEntity>
    {
        void Add(AlbumTrackEntity entity);
        void Update(AlbumTrackEntity entity);
        void Delete(AlbumTrackEntity entity);
    }
}